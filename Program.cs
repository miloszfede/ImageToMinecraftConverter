using JpgToMinecraftConverter.Interfaces;
using JpgToMinecraftConverter.Models;
using JpgToMinecraftConverter.Services;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

class Program
{
    static void Main()
    {
        string blocksDirectory = "static/blocks/";
        string inputImagePath = "/Users/miloszfede/Projects/JpgToMinecraftConverter/luffyo.png";
        int blockWidth = 256;
        int blockHeight = 256;
        int tileSize = 16;

        IImageProcessor imageProcessor = new ImageProcessor();
        IBlockMatcher blockMatcher = new BlockMatcher();
        IBlockTextureLoader textureLoader = new BlockTextureLoader();
        ISchematicBuilder schematicBuilder = new SchematicBuilder();

        Image<Rgba32> image = imageProcessor.LoadAndResize(inputImagePath, blockWidth, blockHeight);
        Rgba32[] pixels = imageProcessor.ExtractPixels(image);

        var blockColors = blockMatcher.LoadBlockColors(blocksDirectory);
        var blockGrid = blockMatcher.MatchBlocks(pixels, image.Width, image.Height, blockColors);

        var blocksToPngs = textureLoader.LoadTextures(blockGrid, blocksDirectory, tileSize);
        imageProcessor.CreateOutputImage(blockGrid, blocksToPngs, tileSize, "output.png");

        var schematic = schematicBuilder.Build(blockGrid, "static/blocks.json");
    }
}

