# JpgToMinecraftConverter

## Overview
JpgToMinecraftConverter is a tool that transforms a standard image into a Minecraft representation using in-game blocks. The current version generates a PNG file that visually replicates the input image using Minecraft blocks. Future updates will include the ability to generate `.schematic` files compatible with the WorldEdit plugin, enabling users to place the image directly into their Minecraft worlds.

## Features
- **Image to Minecraft Block Conversion**: Converts an input image into a grid of Minecraft blocks.
- **PNG Output**: Generates a PNG file that visually represents the input image using Minecraft blocks.
- **Future Feature**: `.schematic` file generation for WorldEdit plugin compatibility.

## How It Works
1. **Input Image**: Provide an image file (e.g., `luffyo.png`).
2. **Block Matching**: The tool matches the colors of the image pixels to the closest Minecraft block textures.
3. **Output**: Generates a PNG file (`output.png`) that visually represents the input image using Minecraft blocks.
4. **Upcoming**: `.schematic` file generation for direct use in Minecraft.

## Getting Started

### Prerequisites
- .NET 9.0 SDK
- A collection of Minecraft block textures (provided in the `static/blocks/` directory).

### Installation
1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd JpgToMinecraftConverter
   ```
2. Build the project:
   ```bash
   dotnet build
   ```

### Usage
1. Place your input image in the project directory (e.g., `luffyo.png`).
2. Run the program:
   ```bash
   dotnet run
   ```
3. The output PNG file will be generated in the project directory as `output.png`.

### Future Usage (Planned)
- After `.schematic` file support is added, the tool will also generate a `.schematic` file alongside the PNG output. This file can be imported into Minecraft using the WorldEdit plugin.

## Project Structure
- `Program.cs`: Main entry point of the application.
- `Interfaces/`: Contains interfaces for modular components.
- `Models/`: Data models used in the application.
- `Services/`: Core services for image processing, block matching, and schematic building.
- `static/blocks/`: Directory containing Minecraft block textures.

## Contributing
Contributions are welcome! Feel free to open issues or submit pull requests to improve the project.

## Acknowledgments
- [SixLabors.ImageSharp](https://github.com/SixLabors/ImageSharp) for image processing.
- Minecraft and its community for inspiration and block textures.
