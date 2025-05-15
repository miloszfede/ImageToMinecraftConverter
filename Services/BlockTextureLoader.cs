namespace JpgToMinecraftConverter.Services
{
    using JpgToMinecraftConverter.Interfaces;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.PixelFormats;
    using SixLabors.ImageSharp.Processing;
    using System.Collections.Generic;

    class BlockTextureLoader : IBlockTextureLoader
    {
        public Dictionary<string, Image<Rgba32>> LoadTextures(string[,] blockGrid, string directory, int tileSize)
        {
            var result = new Dictionary<string, Image<Rgba32>>();
            foreach (string name in blockGrid)
            {
                if (!result.ContainsKey(name))
                {
                    string path = Path.Combine(directory, name + ".png");
                    using var img = Image.Load<Rgba32>(path);
                    img.Mutate(x => x.Resize(tileSize, tileSize));
                    result[name] = img.Clone();
                }
            }
            return result;
        }
    }
}
