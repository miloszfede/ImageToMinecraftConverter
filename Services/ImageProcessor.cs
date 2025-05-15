namespace JpgToMinecraftConverter.Services
{
    using JpgToMinecraftConverter.Interfaces;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.PixelFormats;
    using SixLabors.ImageSharp.Processing;
    using System.Collections.Generic;

    class ImageProcessor : IImageProcessor
    {
        public Image<Rgba32> LoadAndResize(string path, int width, int height)
        {
            var image = Image.Load<Rgba32>(path);
            image.Mutate(x => x.Resize(width, height));
            return image;
        }

        public Rgba32[] ExtractPixels(Image<Rgba32> image)
        {
            var pixels = new Rgba32[image.Width * image.Height];
            image.CopyPixelDataTo(pixels);
            return pixels;
        }

        public void CreateOutputImage(string[,] blockGrid, Dictionary<string, Image<Rgba32>> textures, int tileSize, string outputPath)
        {
            int width = blockGrid.GetLength(1);
            int height = blockGrid.GetLength(0);
            using var outputImage = new Image<Rgba32>(width * tileSize, height * tileSize);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    string block = blockGrid[i, j];
                    if (textures.ContainsKey(block))
                    {
                        var tex = textures[block];
                        outputImage.Mutate(ctx => ctx.DrawImage(tex, new Point(j * tileSize, i * tileSize), 1f));
                    }
                }
            }
            outputImage.Save(outputPath);
        }
    }
}

