using JpgToMinecraftConverter.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace JpgToMinecraftConverter.Services
{
    public class BlockTextureLoader : IBlockTextureLoader
    {
        public Dictionary<string, Image<Rgba32>> LoadTextures(string[,] blockGrid, string directory, int tileSize)
        {
            var result = new Dictionary<string, Image<Rgba32>>();
            var uniqueBlocks = new HashSet<string>();

            foreach (string name in blockGrid)
            {
                if (!string.IsNullOrEmpty(name))
                {
                    uniqueBlocks.Add(name);
                }
            }

            foreach (string name in uniqueBlocks)
            {
                string path = Path.Combine(directory, name + ".png");
                if (File.Exists(path))
                {
                    using var img = Image.Load<Rgba32>(path);
                    var resized = img.Clone(x => x.Resize(tileSize, tileSize));
                    result[name] = resized;
                }
            }
            return result;
        }
    }
}
