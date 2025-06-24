using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using NetTopologySuite.Geometries;
using NetTopologySuite.Features;
using NetTopologySuite.IO;
using AIRAC_RELEASE_PREP.HELPERS;
using AIRAC_RELEASE_PREP.MODELS;

namespace AIRAC_RELEASE_PREP.PARSERS;

public static class GeoJsonProcessor
{
    private static readonly string VideoMapsPath = Path.Join(
        Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
        "FE-BUDDY_Output", "UPLOAD_TO_vNAS", "VIDEO_MAPS");

    public static void ProcessGeoJsonFiles()
    {
        Console.WriteLine("\nProcessing .geojson files...");

        string crcPath = Path.Join(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "FE-BUDDY_Output", "CRC");

        if (!Directory.Exists(crcPath))
            throw new DirectoryNotFoundException($"CRC folder not found at {crcPath}.");

        if (!Directory.Exists(VideoMapsPath))
            throw new DirectoryNotFoundException($"VIDEO_MAPS folder not found at {VideoMapsPath}.");

        var fileSettings = FileSettings.GetSampleFileSettings();

        foreach (var setting in fileSettings)
        {
            string originalFilePath = Path.Join(crcPath, setting.OriginalFileName);
            string updatedFilePath = Path.Join(VideoMapsPath, setting.UpdatedFileName);

            if (!File.Exists(originalFilePath))
            {
                Console.WriteLine($"\n\tFile not found: {setting.OriginalFileName}\n");
                continue;
            }

            File.Copy(originalFilePath, updatedFilePath, overwrite: true);
            PrependDefaultFeature(updatedFilePath, setting.PrependedDefaultFeature);
        }

        ClipGeoJsonFiles(VideoMapsPath, PreferencesManager.Preferences.BoundingBox);
    }

    private static void PrependDefaultFeature(string filePath, string defaultFeatureJson)
    {
        try
        {
            var reader = new GeoJsonReader();
            var writer = new GeoJsonWriter();

            string json = File.ReadAllText(filePath);
            var featureCollection = reader.Read<FeatureCollection>(json);

            var defaultFeatureCollection = reader.Read<FeatureCollection>
            ($"{{\"type\":\"FeatureCollection\",\"features\":[{defaultFeatureJson}]}}");

            if (defaultFeatureCollection.Count > 0)
            {
                featureCollection.Insert(0, defaultFeatureCollection[0]);
            }

            string updatedJson = writer.Write(featureCollection);
            File.WriteAllText(filePath, updatedJson);

            Console.WriteLine($"\t{Path.GetFileNameWithoutExtension(filePath)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\tError prepending feature to {filePath}: {ex.Message}");
        }
    }

    private static void ClipGeoJsonFiles(string directory, BoundingBox boundingBox)
    {
        Console.WriteLine("\nClipping .geojson files...");

        var envelope = new Envelope(
            boundingBox.SouthwestCorner.Longitude,
            boundingBox.NortheastCorner.Longitude,
            boundingBox.SouthwestCorner.Latitude,
            boundingBox.NortheastCorner.Latitude);

        var polygon = GeometryFactory.Default.CreatePolygon(new[]
        {
            new Coordinate(envelope.MinX, envelope.MinY),
            new Coordinate(envelope.MinX, envelope.MaxY),
            new Coordinate(envelope.MaxX, envelope.MaxY),
            new Coordinate(envelope.MaxX, envelope.MinY),
            new Coordinate(envelope.MinX, envelope.MinY)
        });

        var reader = new GeoJsonReader();
        var writer = new GeoJsonWriter();

        foreach (var file in Directory.GetFiles(directory, "*.geojson"))
        {
            try
            {
                string json = File.ReadAllText(file);
                var featureCollection = reader.Read<FeatureCollection>(json);

                var clippedFeatures = new List<IFeature>();
                var featuresToPrepend = new List<IFeature>();

                foreach (var feature in featureCollection)
                {
                    if (feature.Geometry.IsEmpty) continue;

                    if (feature.Geometry.GeometryType == "Point")
                    {
                        if (feature.Attributes.Exists("isSymbolDefaults") ||
                            feature.Attributes.Exists("isTextDefaults") ||
                            feature.Attributes.Exists("isLineDefaults"))
                        {
                            featuresToPrepend.Add(feature);
                            continue;
                        }

                        if (polygon.Contains(GeometryFactory.Default.CreatePoint(feature.Geometry.Coordinate)))
                        {
                            clippedFeatures.Add(feature);
                            continue;
                        }
                    }

                    var clippedGeometry = feature.Geometry.Intersection(polygon);
                    if (!clippedGeometry.IsEmpty)
                    {
                        feature.Geometry = clippedGeometry;
                        clippedFeatures.Add(feature);
                    }
                }

                featureCollection.Clear();
                foreach (var feature in featuresToPrepend)
                    featureCollection.Add(feature);

                foreach (var feature in clippedFeatures)
                    featureCollection.Add(feature);

                string fileName = Path.GetFileName(file);
                string tempFilePath = Path.Join(directory, fileName + "_clipped.geojson");
                string updatedFilePath = Path.Join(directory, fileName);

                File.WriteAllText(tempFilePath, writer.Write(featureCollection));
                File.Delete(file);
                File.Move(tempFilePath, updatedFilePath);

                Console.WriteLine($"\t{Path.GetFileNameWithoutExtension(updatedFilePath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\tError processing {file}: {ex.Message}");
            }
        }
    }
}
