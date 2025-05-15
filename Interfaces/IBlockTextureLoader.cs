namespace JpgToMinecraftConverter.Interfaces
{
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.PixelFormats;
    using System.Collections.Generic;

    interface IBlockTextureLoader
    {
        Dictionary<string, Image<Rgba32>> LoadTextures(string[,] blockGrid, string directory, int tileSize);
    }
}
