using JpgToMinecraftConverter.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace JpgToMinecraftConverter.Services
{
    public class BlockMatcher : IBlockMatcher
    {
        private readonly Dictionary<string, Rgba32> _blockColorCache = new();

        private float DistanceSquared(Rgba32 a, Rgba32 b)
        {
            float r = (a.R - b.R) * (a.R - b.R);
            float g = (a.G - b.G) * (a.G - b.G);
            float bDiff = (a.B - b.B) * (a.B - b.B);
            return r + g + bDiff;
        }

        public Dictionary<string, Rgba32> LoadBlockColors(string blocksDirectory)
        {
            if (_blockColorCache.Count > 0)
                return _blockColorCache;

            if (!Directory.Exists(blocksDirectory))
                throw new DirectoryNotFoundException($"Blocks directory not found: {blocksDirectory}");

            var files = Directory.GetFiles(blocksDirectory, "*.png");
            
            if (files.Length == 0)
                throw new InvalidOperationException($"No PNG files found in directory: {blocksDirectory}");

            Console.WriteLine($"Loading {files.Length} block textures...");

            foreach (string file in files)
            {
                try
                {
                    string name = Path.GetFileNameWithoutExtension(file);
                    using var img = Image.Load<Rgba32>(file);
                    
                    Rgba32[] pixels = new Rgba32[img.Width * img.Height];
                    img.CopyPixelDataTo(pixels);

                    var colorCount = new Dictionary<Rgba32, int>();
                    foreach (var px in pixels)
                    {
                        colorCount[px] = colorCount.GetValueOrDefault(px, 0) + 1;
                    }

                    if (colorCount.Count > 0)
                    {
                        var mostCommon = colorCount.OrderByDescending(c => c.Value).First().Key;
                        _blockColorCache[name] = mostCommon;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Warning: Failed to load block texture '{file}': {ex.Message}");
                }
            }

            Console.WriteLine($"Loaded {_blockColorCache.Count} block colors.");
            return _blockColorCache;
        }

        public string[,] MatchBlocks(Rgba32[] pixels, int width, int height, Dictionary<string, Rgba32> blockColors)
        {
            var grid = new string[height, width];
            var blockList = blockColors.ToList(); 
            Console.WriteLine($"Matching {pixels.Length} pixels to blocks...");
            
            for (int i = 0; i < pixels.Length; i++)
            {
                if (i % 10000 == 0 && i > 0)
                {
                    var progress = (int)((i / (double)pixels.Length) * 100);
                    Console.Write($"\rProgress: {progress}%");
                }

                var pixel = pixels[i];
                string closestBlock = "";
                float minDist = float.MaxValue;

                foreach (var block in blockList)
                {
                    float dist = DistanceSquared(pixel, block.Value);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        closestBlock = block.Key;
                    }
                }

                int x = i % width;
                int y = i / width;
                grid[y, x] = closestBlock;
            }

            Console.WriteLine("\rProgress: 100% - Complete!");
            return grid;
        }
    }
}

