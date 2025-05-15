using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace JpgToMinecraftConverter.Interfaces
{
    interface IImageProcessor
    {
        Image<Rgba32> LoadAndResize(string path, int width, int height);
        Rgba32[] ExtractPixels(Image<Rgba32> image);
        void CreateOutputImage(string[,] blockGrid, Dictionary<string, Image<Rgba32>> textures, int tileSize, string outputPath);
    }
}

