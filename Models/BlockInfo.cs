namespace JpgToMinecraftConverter
{

    class BlockInfo
    {
        public required string name { get; set; }
        public required string texture_image { get; set; }
        public required string game_id { get; set; }
        public required string game_id_13 { get; set; }
        public required int block_id { get; set; }
        public required int data_id { get; set; }
        public required bool luminance { get; set; }
        public required bool transparency { get; set; }
        public required bool falling { get; set; }
        public required bool redstone { get; set; }
        public required bool survival { get; set; }
        public required int version { get; set; }

    }
}
