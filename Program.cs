using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;


class Program
{
    private static void Main()
    {
        string blocksDirectory = "/Users/miloszfede/Projects/JpgToMinecraftConverter/static/blocks/";
        
        string[] pngFiles = Directory.GetFiles(blocksDirectory, "*.png");
                
        Dictionary<string, Rgba32> blockColors = new Dictionary<string, Rgba32>();
        
        foreach (string pngFilePath in pngFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(pngFilePath);
            
            using Image<Rgba32> image = Image.Load<Rgba32>(pngFilePath);
            Rgba32[] pixelArray = new Rgba32[image.Width * image.Height];
            image.CopyPixelDataTo(pixelArray);

            Dictionary<Rgba32, int> colorCount = new Dictionary<Rgba32, int>();

            foreach (Rgba32 pixel in pixelArray)
            {
                if (colorCount.ContainsKey(pixel))
                {
                    colorCount[pixel]++;
                }
                else
                {
                    colorCount[pixel] = 1;
                }
            }
            
            Rgba32 mostCommonColor = colorCount.OrderByDescending(kvp => kvp.Value).First().Key;
            blockColors[fileName] = mostCommonColor;
            
        }
        int blockWidth = 256;
        int blockHeight = 256;
        int tileSize = 16;


        using Image<Rgba32> imageToLoad = Image.Load<Rgba32>("/Users/miloszfede/Projects/JpgToMinecraftConverter/IMG_4670.png");
        imageToLoad.Mutate(x => x.Resize(blockWidth,blockHeight)); 
        Rgba32[] pixelArrayToLoad = new Rgba32[imageToLoad.Width * imageToLoad.Height];
        imageToLoad.CopyPixelDataTo(pixelArrayToLoad);

        string[,] blockGrid = new string[imageToLoad.Height, imageToLoad.Width];
        
        float distance_squared(Rgba32 p1, Rgba32 p2)
        {
            float rDiff = (p1.R - p2.R) * (p1.R - p2.R);
            float gDiff = (p1.G - p2.G) * (p1.G - p2.G);
            float bDiff = (p1.B - p2.B) * (p1.B - p2.B);
            return rDiff + gDiff + bDiff;
        }

        for (int i = 0; i < pixelArrayToLoad.Length; i++)
        {
            Rgba32 pixel = pixelArrayToLoad[i];
            float minDistance = float.MaxValue;
            string closestBlock = string.Empty;

            foreach (var kvp in blockColors)
            {
                float distance = distance_squared(pixel, kvp.Value);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestBlock = kvp.Key;
                }
            }
            int x = i % imageToLoad.Width;
            int y = i / imageToLoad.Width;
            blockGrid[y, x] = closestBlock;
        }

        Dictionary<string, Image<Rgba32>> blocksToPngs = new Dictionary<string, Image<Rgba32>>();

        foreach (string blockName in blockGrid)
        {
            if (!blocksToPngs.ContainsKey(blockName))
            {
                string path = Path.Combine(blocksDirectory, blockName + ".png");
                Image<Rgba32> blockImage = Image.Load<Rgba32>(path);
                blockImage.Mutate(x => x.Resize(tileSize, tileSize));
                blocksToPngs[blockName] = blockImage;
            }
            System.Console.WriteLine(KeyValuePair.Create(blockName, blocksToPngs[blockName]));
        }

        using(Image<Rgba32> minecraftPixelImage = new(blockWidth * tileSize, blockHeight * tileSize))
        {
            for (int i = 0; i <blockGrid.GetLength(0); i++)
        {
            for (int j = 0; j <blockGrid.GetLength(1);j++)
            {
                string blockName = blockGrid[i,j];
                if (blocksToPngs.ContainsKey(blockName))
                {
                    Image<Rgba32> blockImage = blocksToPngs[blockName];
                    var destX = j * tileSize;
                    var destY = i * tileSize; 
                    minecraftPixelImage.Mutate(ctx => ctx.DrawImage(blockImage, new Point(destX, destY), 1f));
                }
            }
        }
        minecraftPixelImage.Save("output.png");
        }
    
    }
}