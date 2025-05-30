using JpgToMinecraftConverter.Models;

namespace JpgToMinecraftConverter.Services
{
    public static class ConfigurationParser
    {
        public static ConversionConfig ParseArguments(string[] args)
        {
            if (args.Length == 0)
            {
                ShowUsage();
                Environment.Exit(0);
            }

            var config = new ConversionConfig();
            
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLower())
                {
                    case "--help":
                        ShowUsage();
                        Environment.Exit(0);
                        break;
                    case "-v":
                    case "--verbose":
                        config.Verbose = true;
                        break;
                    case "-w":
                    case "--width":
                        if (i + 1 < args.Length && int.TryParse(args[i + 1], out int width))
                        {
                            config.BlockWidth = width;
                            i++;
                        }
                        break;
                    case "-h":
                    case "--height":
                        if (i + 1 < args.Length && int.TryParse(args[i + 1], out int height))
                        {
                            config.BlockHeight = height;
                            i++;
                        }
                        break;
                    case "-t":
                    case "--tile-size":
                        if (i + 1 < args.Length && int.TryParse(args[i + 1], out int tileSize))
                        {
                            config.TileSize = tileSize;
                            i++;
                        }
                        break;
                    case "-o":
                    case "--output":
                        if (i + 1 < args.Length)
                        {
                            config.OutputImagePath = args[i + 1];
                            i++;
                        }
                        break;
                    case "--no-schematic":
                        config.GenerateSchematic = false;
                        break;
                    default:
                        if (!args[i].StartsWith("-") && string.IsNullOrEmpty(config.InputImagePath))
                        {
                            config.InputImagePath = args[i];
                        }
                        break;
                }
            }

            return config;
        }

        private static void ShowUsage()
        {
            Console.WriteLine("JpgToMinecraftConverter - Convert images to Minecraft block art");
            Console.WriteLine();
            Console.WriteLine("Usage:");
            Console.WriteLine("  JpgToMinecraftConverter <input_image> [options]");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("  -w, --width <value>      Output width in blocks (default: 256)");
            Console.WriteLine("  -h, --height <value>     Output height in blocks (default: 256)");
            Console.WriteLine("  -t, --tile-size <value>  Tile size in pixels (default: 16)");
            Console.WriteLine("  -o, --output <path>      Output image path (default: output.png)");
            Console.WriteLine("  --no-schematic           Don't generate schematic file");
            Console.WriteLine("  -v, --verbose            Enable verbose output");
            Console.WriteLine("  --help                   Show this help message");
            Console.WriteLine();
            Console.WriteLine("Examples:");
            Console.WriteLine("  JpgToMinecraftConverter image.png");
            Console.WriteLine("  JpgToMinecraftConverter image.png -w 128 -h 128 -o my_output.png");
            Console.WriteLine("  JpgToMinecraftConverter image.png --verbose --no-schematic");
        }
    }
}
