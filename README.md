# AIRAC Release Prep

The **AIRAC Release Prep** program is a .NET console application designed to streamline the preparation and processing of FE-BUDDY_Output GeoJSON files for AIRAC releases. It supports folder validation, GeoJSON transformation, and the inclusion of default features in the output files.

## [DOWNLOAD](https://github.com/KCSanders7070/vZOB-AIRAC-RELEASE-PREP/releases/latest/download/vZOB-AIRAC-RELEASE-PREP.exe)

## Features

1. **FE-BUDDY_Output Folder Detection**
   - Automatically detects the `FE-BUDDY_Output` folder on the user's desktop.
   - Prompts the user to use the detected folder or input a custom path.

2. **GeoJSON Mapping**
   - Uses a `GeoJSONMapping.json` file to map input file names to desired output file names and default features.

3. **Alias File Management**
   - Supports local alias files or downloading from a URL with optional token authentication.
   - Saves alias file preferences for future runs.

4. **File Processing**
   - Parses `.geojson` files in the specified folder.
   - Prepends default features to the files based on the mappings.
   - Outputs transformed files to an `output` folder.

5. **Preferences Management**
   - Saves and loads user preferences for alias files in `%localappdata%/AIRAC_RELEASE_PREP_prefs`.

---

## Project Structure

```
AIRAC RELEASE PREP/
│   AIRAC RELEASE PREP.csproj
│   AIRAC RELEASE PREP.sln
│   Program.cs
│
├───GENERAL RESOURCES
│       GeoJSONMapping.json
│       PreferencesManager.cs
│
├───GENERATORS
│       DefaultGenerator.cs
│
├───MODELS
│       FEBDefaults.cs
│
├───PARSERS
│       FileParser.cs
│
├───UI
│       UserInterface.cs
│
└───output
        [Generated files are saved here.]
```

---

## How to Use

### Prerequisites
- .NET 8.0 SDK or higher.
- A `GeoJSONMapping.json` file in the `GENERAL RESOURCES` folder with proper mappings.

### Running the Program
1. Build the solution using Visual Studio or the .NET CLI.
2. Run the program.

### Workflow
1. **FE-BUDDY_Output Folder**:
   - If the folder is found on the desktop, the program will prompt you to use it.
   - If not, you'll be asked to provide the folder path.

2. **Alias File**:
   - Choose to use a local alias file or download one from a URL.
   - Provide the token if required for downloading.

3. **GeoJSON Processing**:
   - The program processes all `.geojson` files in the specified folder.
   - Applies the transformations and saves the outputs in the `output` folder.

4. **Output**:
   - Transformed files are saved in the `output` directory under the program's root folder.

---

## Configuration

### GeoJSONMapping.json
The `GeoJSONMapping.json` file defines the mappings between input files, output files, and default features.

Example:
```json
[
    {
        "FeBuddyOutputFileName": "APT_symbols.geojson",
        "ChangedFileName": "FEB_APT_symbols.geojson",
        "PrependedDefaultFeature": "{\"type\":\"Feature\",\"geometry\":{\"type\":\"Point\",\"coordinates\":[90.0,180.0]},\"properties\":{\"isSymbolDefaults\":true,\"bcg\":18,\"filters\":[18],\"style\":\"airport\",\"size\":1}}"
    }
]
```

### Preferences File
Preferences are saved in `%localappdata%/AIRAC_RELEASE_PREP_prefs/preferences.json`. Example:
```json
{
    "AliasFilePath": "C:\\path\\to\\alias.txt",
    "AliasFileURL": "https://example.com/alias",
    "AliasFileToken": "your-token-here"
}
```

---

## Key Files

### `Program.cs`
- Orchestrates the overall flow of the application.

### `FileParser.cs`
- Handles directory and file operations.
- Parses GeoJSON files.

### `GeoJSONMapping.cs`
- Loads and validates GeoJSON mappings from `GeoJSONMapping.json`.

### `DefaultGenerator.cs`
- Applies default features and transforms GeoJSON files.

### `UserInterface.cs`
- Manages user interactions and prompts.
- Downloads alias files if needed.

### `PreferencesManager.cs`
- Manages saving and loading user preferences.

---

## Troubleshooting

1. **Mapping Not Found**:
   - Ensure the `GeoJSONMapping.json` file contains the correct mappings for all input files.

2. **Alias File Issues**:
   - Verify the file path, URL, and token in the preferences file.

3. **Output Directory Missing**:
   - The program automatically creates the `output` folder if it doesn't exist.

---

## Future Enhancements
- Add support for logging to a file.
- Implement unit tests for key modules.
- Add a GUI for better user interaction.

---

## Author
- Developed as part of the AIRAC Release Prep project.

---

## License
- [MIT License](LICENSE)
