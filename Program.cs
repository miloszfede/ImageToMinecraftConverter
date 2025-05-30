using JpgToMinecraftConverter.Interfaces;
using JpgToMinecraftConverter.Models;
using JpgToMinecraftConverter.Services;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            var config = ConfigurationParser.ParseArguments(args);
            config.Validate();

            if (config.Verbose)
            {
                Console.WriteLine($"Input: {config.InputImagePath}");
                Console.WriteLine($"Output: {config.OutputImagePath}");
                Console.WriteLine($"Dimensions: {config.BlockWidth}x{config.BlockHeight}");
                Console.WriteLine($"Tile size: {config.TileSize}px");
                Console.WriteLine();
            }

            Console.WriteLine("Loading and processing image...");
            IImageProcessor imageProcessor = new ImageProcessor();
            IBlockMatcher blockMatcher = new BlockMatcher();
            IBlockTextureLoader textureLoader = new BlockTextureLoader();
            ISchematicBuilder schematicBuilder = new SchematicBuilder();

            using var image = imageProcessor.LoadAndResize(config.InputImagePath, config.BlockWidth, config.BlockHeight);
            Rgba32[] pixels = imageProcessor.ExtractPixels(image);

            if (config.Verbose) Console.WriteLine("Loading block colors...");
            var blockColors = blockMatcher.LoadBlockColors(config.BlocksDirectory);
            if (config.Verbose) Console.WriteLine($"Loaded {blockColors.Count} block types");

            if (config.Verbose) Console.WriteLine("Matching pixels to blocks...");
            var blockGrid = blockMatcher.MatchBlocks(pixels, image.Width, image.Height, blockColors);

            if (config.Verbose) Console.WriteLine("Loading block textures...");
            var blocksToPngs = textureLoader.LoadTextures(blockGrid, config.BlocksDirectory, config.TileSize);
            
            if (config.Verbose) Console.WriteLine("Creating output image...");
            imageProcessor.CreateOutputImage(blockGrid, blocksToPngs, config.TileSize, config.OutputImagePath);

            if (config.GenerateSchematic)
            {
                if (config.Verbose) Console.WriteLine("Building schematic...");
                var schematic = schematicBuilder.Build(blockGrid, config.BlocksJsonPath);
            }
            
            Console.WriteLine("Conversion completed successfully!");
            Console.WriteLine($"Output image saved as: {config.OutputImagePath}");
            Console.WriteLine($"Image dimensions: {image.Width}x{image.Height} pixels");
            
            // Dispose of textures to free memory
            foreach (var texture in blocksToPngs.Values)
            {
                texture?.Dispose();
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Invalid arguments: {ex.Message}");
            Environment.Exit(1);
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"File not found: {ex.Message}");
            Environment.Exit(1);
        }
        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine($"Directory not found: {ex.Message}");
            Environment.Exit(1);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Operation failed: {ex.Message}");
            Environment.Exit(1);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error during conversion: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            Environment.Exit(1);
        }
    }
}

