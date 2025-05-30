using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace JpgToMinecraftConverter.Interfaces
{
    public interface IBlockTextureLoader
    {
        Dictionary<string, Image<Rgba32>> LoadTextures(string[,] blockGrid, string directory, int tileSize);
    }

    public interface ITextureCache : IDisposable
    {
        Dictionary<string, Image<Rgba32>> Textures { get; }
    }
}
