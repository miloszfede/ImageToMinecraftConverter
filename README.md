# JPG to Minecraft Converter

A high-performance C# application that converts images into Minecraft block art using advanced color matching algorithms.

## Features

- 🎨 **Optimized Color Matching**: Uses most-common-color algorithm for superior visual results
- 🖼️ **High Quality Output**: Generates pixel-perfect recreations using Minecraft block textures
- 📦 **Schematic Export**: Creates Minecraft schematic files for easy importing
- 🛠️ **Professional CLI**: Extensive command-line options for customization
- ⚡ **Performance Optimized**: Block color caching and efficient algorithms
- 🔧 **Memory Efficient**: Proper resource management to handle large images

## Requirements

- .NET 9.0 or later
- ImageSharp library (automatically installed via NuGet)
- fNbt library for schematic generation (automatically installed via NuGet)

## Installation

1. Clone the repository:
```bash
git clone <repository-url>
cd JpgToMinecraftConverter
```

2. Build the project:
```bash
dotnet build
```

3. Run the application:
```bash
dotnet run -- <input_image> [options]
```

## Usage

### Basic Usage
```bash
dotnet run -- image.png
```

### Advanced Usage
```bash
dotnet run -- image.png -w 128 --height 128 -o my_output.png --verbose
```

### Command Line Options

| Option | Description | Default |
|--------|-------------|---------|
| `-w, --width <value>` | Output width in blocks | 256 |
| `--height <value>` | Output height in blocks | 256 |
| `-t, --tile-size <value>` | Tile size in pixels | 16 |
| `-o, --output <path>` | Output image path | output.png |
| `--no-schematic` | Don't generate schematic file | false |
| `-v, --verbose` | Enable verbose output | false |
| `-h, --help` | Show help message | - |

## Examples

Convert an image to 128x128 blocks:
```bash
dotnet run -- photo.jpg -w 128 --height 128
```

Generate only the image without schematic:
```bash
dotnet run -- artwork.png --no-schematic
```

Verbose output with custom output path:
```bash
dotnet run -- landscape.jpg -v -o minecraft_landscape.png
```

## Output Files

- **Output Image**: A reconstructed image using Minecraft block textures
- **Schematic File**: A .schematic file that can be imported into Minecraft

## Supported Block Types

The application includes support for 100+ Minecraft blocks including:
- Basic blocks (stone, dirt, wood)
- Colored concrete and concrete powder
- Ores and gems
- Coral blocks
- And many more!

## Performance

- **Parallel Processing**: Utilizes all CPU cores for optimal performance
- **Memory Management**: Proper disposal of resources prevents memory leaks
- **Progress Reporting**: Real-time progress updates for large images

## Architecture

The application follows clean architecture principles:

- **Interfaces**: Abstract contracts for all major components
- **Services**: Implementation of core functionality
- **Models**: Data structures and configuration
- **Dependency Injection**: Loose coupling between components

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## License

This project is licensed under the MIT License.

## Technical Details

### Color Matching Algorithm
Uses weighted RGB distance calculation for perceptually accurate color matching:
- Red: 30% weight
- Green: 59% weight  
- Blue: 11% weight

### Supported Image Formats
- PNG
- JPEG
- BMP
- GIF
- TIFF
- And other formats supported by ImageSharp

### Block Texture Resolution
- Default: 16x16 pixels (Minecraft standard)
- Configurable via `--tile-size` option

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
