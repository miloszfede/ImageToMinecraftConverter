namespace JpgToMinecraftConverter.Models
{
    public class ConversionConfig
    {
        public string InputImagePath { get; set; } = string.Empty;
        public string BlocksDirectory { get; set; } = "static/blocks/";
        public string BlocksJsonPath { get; set; } = "static/blocks.json";
        public string OutputImagePath { get; set; } = "output.png";
        public int BlockWidth { get; set; } = 256;
        public int BlockHeight { get; set; } = 256;
        public int TileSize { get; set; } = 16;
        public bool GenerateSchematic { get; set; } = true;
        public bool Verbose { get; set; } = false;

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(InputImagePath))
                throw new ArgumentException("Input image path is required");

            if (!File.Exists(InputImagePath))
                throw new FileNotFoundException($"Input image file not found: {InputImagePath}");

            if (!Directory.Exists(BlocksDirectory))
                throw new DirectoryNotFoundException($"Blocks directory not found: {BlocksDirectory}");

            if (GenerateSchematic && !File.Exists(BlocksJsonPath))
                throw new FileNotFoundException($"Blocks JSON file not found: {BlocksJsonPath}");

            if (BlockWidth <= 0 || BlockHeight <= 0)
                throw new ArgumentException("Block dimensions must be positive");

            if (TileSize <= 0)
                throw new ArgumentException("Tile size must be positive");
        }
    }
}
