namespace JpgToMinecraftConverter
{

    class Schematic
    {
        public required short Width { get; set; }
        public required short Height { get; set; }
        public required short Length { get; set; }
        public required string Materials { get; set; }
        public required byte[] Blocks { get; set; }
        public required byte[] Data { get; set; }
    }
}
