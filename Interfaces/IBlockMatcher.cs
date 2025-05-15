using SixLabors.ImageSharp.PixelFormats;

namespace JpgToMinecraftConverter.Interfaces
{
    interface IBlockMatcher
    {
        Dictionary<string, Rgba32> LoadBlockColors(string blocksDirectory);
        string[,] MatchBlocks(Rgba32[] pixels, int width, int height, Dictionary<string, Rgba32> blockColors);
    }
}

