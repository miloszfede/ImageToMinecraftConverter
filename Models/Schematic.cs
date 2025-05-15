namespace JpgToMinecraftConverter.Models
{
    class Schematic
    {
        public short Width { get; set; }
        public short Height = 1;
        public short Length { get; set; }
        public string Materials = "Alpha";
        public byte[] Blocks { get; set; }
        public byte[] Data { get; set; }

        public Schematic(short width, short length, byte[] blocks, byte[] data)
        {
            Width = width;
            Length = length;
            Blocks = blocks;
            Data = data;
        }
    }
}
