using JpgToMinecraftConverter.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace JpgToMinecraftConverter.Services
{
    class BlockMatcher : IBlockMatcher
    {
        private float DistanceSquared(Rgba32 a, Rgba32 b)
        {
            float r = (a.R - b.R) * (a.R - b.R);
            float g = (a.G - b.G) * (a.G - b.G);
            float bDiff = (a.B - b.B) * (a.B - b.B);
            return r + g + bDiff;
        }

        public Dictionary<string, Rgba32> LoadBlockColors(string blocksDirectory)
        {
            var blockColors = new Dictionary<string, Rgba32>();
            foreach (string file in Directory.GetFiles(blocksDirectory, "*.png"))
            {
                string name = Path.GetFileNameWithoutExtension(file);
                using var img = Image.Load<Rgba32>(file);
                Rgba32[] pixels = new Rgba32[img.Width * img.Height];
                img.CopyPixelDataTo(pixels);

                var colorCount = new Dictionary<Rgba32, int>();
                foreach (var px in pixels)
                {
                    if (!colorCount.ContainsKey(px)) colorCount[px] = 0;
                    colorCount[px]++;
                }

                var mostCommon = colorCount.OrderByDescending(c => c.Value).First().Key;
                blockColors[name] = mostCommon;
            }

            return blockColors;
        }

        public string[,] MatchBlocks(Rgba32[] pixels, int width, int height, Dictionary<string, Rgba32> blockColors)
        {
            var grid = new string[height, width];
            for (int i = 0; i < pixels.Length; i++)
            {
                var pixel = pixels[i];
                string closestBlock = "";
                float minDist = float.MaxValue;

                foreach (var block in blockColors)
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

            return grid;
        }
    }
}

