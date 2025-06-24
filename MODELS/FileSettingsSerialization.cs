using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AIRAC_RELEASE_PREP.MODELS
{
    public class FileSettings
    {
        [JsonPropertyName("FeBuddyOutputFileName")]
        public string OriginalFileName { get; set; }

        [JsonPropertyName("ChangedFileName")]
        public string UpdatedFileName { get; set; }

        [JsonPropertyName("PrependedDefaultFeature")]
        public string PrependedDefaultFeature { get; set; }

        public static List<FileSettings> GetSampleFileSettings()
        {
            return new List<FileSettings>
            {
                new FileSettings
                {
                    OriginalFileName = "APT_symbols.geojson",
                    UpdatedFileName = "FEB_APT_symbols.geojson",
                    PrependedDefaultFeature = JsonSerializer.Serialize(new
                    {
                        type = "Feature",
                        geometry = new
                        {
                            type = "Point",
                            coordinates = new[] { 90.0, 180.0 }
                        },
                        properties = new
                        {
                            isSymbolDefaults = true,
                            bcg = 18,
                            filters = new[] { 18 },
                            style = "airport",
                            size = 1
                        }
                    })
                },
                new FileSettings
                {
                    OriginalFileName = "APT_text.geojson",
                    UpdatedFileName = "FEB_APT_text.geojson",
                    PrependedDefaultFeature = JsonSerializer.Serialize(new
                    {
                        type = "Feature",
                        geometry = new
                        {
                            type = "Point",
                            coordinates = new[] { 90.0, 180.0 }
                        },
                        properties = new
                        {
                            isTextDefaults = true,
                            bcg = 18,
                            filters = new[] { 18 },
                            size = 1,
                            underline = false,
                            opaque = false,
                            xOffset = 12,
                            yOffset = 0
                        }
                    })
                },
                new FileSettings
                {
                    OriginalFileName = "ARTCC BOUNDARIES-HIGH_lines.geojson",
                    UpdatedFileName = "FEB_ARTCC BOUNDARIES-HIGH_lines.geojson",
                    PrependedDefaultFeature = JsonSerializer.Serialize(new
                    {
                        type = "Feature",
                        geometry = new
                        {
                            type = "Point",
                            coordinates = new[] { 90.0, 180.0 }
                        },
                        properties = new
                        {
                            isLineDefaults = true,
                            bcg = 1,
                            filters = new[] { 1 },
                            style = "Solid",
                            thickness = 3
                        }
                    })
                },
                new FileSettings
                {
                    OriginalFileName = "ARTCC BOUNDARIES-LOW_lines.geojson",
                    UpdatedFileName = "FEB_ARTCC BOUNDARIES-LOW_lines.geojson",
                    PrependedDefaultFeature = JsonSerializer.Serialize(new
                    {
                        type = "Feature",
                        geometry = new
                        {
                            type = "Point",
                            coordinates = new[] { 90.0, 180.0 }
                        },
                        properties = new
                        {
                            isLineDefaults = true,
                            bcg = 11,
                            filters = new[] { 11 },
                            style = "Solid",
                            thickness = 3
                        }
                    })
                },
                new FileSettings
                {
                    OriginalFileName = "AWY-HIGH_lines(DME Cutoff).geojson",
                    UpdatedFileName = "FEB_AWY-HIGH_lines(DME Cutoff).geojson",
                    PrependedDefaultFeature = JsonSerializer.Serialize(new
                    {
                        type = "Feature",
                        geometry = new
                        {
                            type = "Point",
                            coordinates = new[] { 90.0, 180.0 }
                        },
                        properties = new
                        {
                            isLineDefaults = true,
                            bcg = 3,
                            filters = new[] { 3 },
                            style = "Solid",
                            thickness = 1
                        }
                    })
                },
                new FileSettings
                {
                    OriginalFileName = "AWY-HIGH_symbols.geojson",
                    UpdatedFileName = "FEB_AWY-HIGH_symbols.geojson",
                    PrependedDefaultFeature = JsonSerializer.Serialize(new
                    {
                        type = "Feature",
                        geometry = new
                        {
                            type = "Point",
                            coordinates = new[] { 90.0, 180.0 }
                        },
                        properties = new
                        {
                            isSymbolDefaults = true,
                            bcg = 3,
                            filters = new[] { 3 },
                            style = "airwayIntersections",
                            size = 1
                        }
                    })
                },
                new FileSettings
                {
                    OriginalFileName = "AWY-HIGH_text.geojson",
                    UpdatedFileName = "FEB_AWY-HIGH_text.geojson",
                    PrependedDefaultFeature = JsonSerializer.Serialize(new
                    {
                        type = "Feature",
                        geometry = new
                        {
                            type = "Point",
                            coordinates = new[] { 90.0, 180.0 }
                        },
                        properties = new
                        {
                            isTextDefaults = true,
                            bcg = 3,
                            filters = new[] { 3 },
                            size = 1,
                            underline = false,
                            opaque = false,
                            xOffset = 12,
                            yOffset = 0
                        }
                    })
                },
                new FileSettings
                {
                    OriginalFileName = "AWY-LOW_lines(DME Cutoff).geojson",
                    UpdatedFileName = "FEB_AWY-LOW_lines(DME Cutoff).geojson",
                    PrependedDefaultFeature = JsonSerializer.Serialize(new
                    {
                        type = "Feature",
                        geometry = new
                        {
                            type = "Point",
                            coordinates = new[] { 90.0, 180.0 }
                        },
                        properties = new
                        {
                            isLineDefaults = true,
                            bcg = 13,
                            filters = new[] { 13 },
                            style = "Solid",
                            thickness = 1
                        }
                    })
                },
                new FileSettings
                {
                    OriginalFileName = "AWY-LOW_symbols.geojson",
                    UpdatedFileName = "FEB_AWY-LOW_symbols.geojson",
                    PrependedDefaultFeature = JsonSerializer.Serialize(new
                    {
                        type = "Feature",
                        geometry = new
                        {
                            type = "Point",
                            coordinates = new[] { 90.0, 180.0 }
                        },
                        properties = new
                        {
                            isSymbolDefaults = true,
                            bcg = 13,
                            filters = new[] { 13 },
                            style = "airwayIntersections",
                            size = 1
                        }
                    })
                },
                new FileSettings
                {
                    OriginalFileName = "AWY-LOW_text.geojson",
                    UpdatedFileName = "FEB_AWY-LOW_text.geojson",
                    PrependedDefaultFeature = JsonSerializer.Serialize(new
                    {
                        type = "Feature",
                        geometry = new
                        {
                            type = "Point",
                            coordinates = new[] { 90.0, 180.0 }
                        },
                        properties = new
                        {
                            isTextDefaults = true,
                            bcg = 13,
                            filters = new[] { 13 },
                            size = 1,
                            underline = false,
                            opaque = false,
                            xOffset = 12,
                            yOffset = 0
                        }
                    })
                },
                new FileSettings
                {
                    OriginalFileName = "FIX_symbols.geojson",
                    UpdatedFileName = "FEB_FIX_symbols.geojson",
                    PrependedDefaultFeature = JsonSerializer.Serialize(new
                    {
                        type = "Feature",
                        geometry = new
                        {
                            type = "Point",
                            coordinates = new[] { 90.0, 180.0 }
                        },
                        properties = new
                        {
                            isSymbolDefaults = true,
                            bcg = 8,
                            filters = new[] { 8 },
                            style = "otherWaypoints",
                            size = 1
                        }
                    })
                },
                new FileSettings
                {
                    OriginalFileName = "FIX_text.geojson",
                    UpdatedFileName = "FEB_FIX_text.geojson",
                    PrependedDefaultFeature = JsonSerializer.Serialize(new
                    {
                        type = "Feature",
                        geometry = new
                        {
                            type = "Point",
                            coordinates = new[] { 90.0, 180.0 }
                        },
                        properties = new
                        {
                            isTextDefaults = true,
                            bcg = 8,
                            filters = new[] { 8 },
                            size = 1,
                            underline = false,
                            opaque = false,
                            xOffset = 6,
                            yOffset = 0
                        }
                    })
                },
                new FileSettings
                {
                    OriginalFileName = "VOR_symbols.geojson",
                    UpdatedFileName = "FEB_VOR_symbols.geojson",
                    PrependedDefaultFeature = JsonSerializer.Serialize(new
                    {
                        type = "Feature",
                        geometry = new
                        {
                            type = "Point",
                            coordinates = new[] { 90.0, 180.0 }
                        },
                        properties = new
                        {
                            isSymbolDefaults = true,
                            bcg = 12,
                            filters = new[] { 12 },
                            style = "vor",
                            size = 1
                        }
                    })
                },
                new FileSettings
                {
                    OriginalFileName = "VOR_text.geojson",
                    UpdatedFileName = "FEB_VOR_text.geojson",
                    PrependedDefaultFeature = JsonSerializer.Serialize(new
                    {
                        type = "Feature",
                        geometry = new
                        {
                            type = "Point",
                            coordinates = new[] { 90.0, 180.0 }
                        },
                        properties = new
                        {
                            isTextDefaults = true,
                            bcg = 12,
                            filters = new[] { 12 },
                            size = 1,
                            underline = false,
                            opaque = false,
                            xOffset = 12,
                            yOffset = 0
                        }
                    })
                },
                new FileSettings
                {
                    OriginalFileName = "WX STATIONS_text.geojson",
                    UpdatedFileName = "FEB_WX STATIONS_text.geojson",
                    PrependedDefaultFeature = JsonSerializer.Serialize(new
                    {
                        type = "Feature",
                        geometry = new
                        {
                            type = "Point",
                            coordinates = new[] { 90.0, 180.0 }
                        },
                        properties = new
                        {
                            isTextDefaults = true,
                            bcg = 9,
                            filters = new[] { 9 },
                            size = 1,
                            underline = false,
                            opaque = false,
                            xOffset = 12,
                            yOffset = 0
                        }
                    })
                }
            };
        }
    }
}
