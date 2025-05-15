namespace JpgToMinecraftConverter.Services
{
    using JpgToMinecraftConverter.Interfaces;
    using JpgToMinecraftConverter.Models;
    using System.Text.Json;

    class SchematicBuilder : ISchematicBuilder
    {
        public Schematic Build(string[,] blockGrid, string blockJsonPath)
        {
            var map = new Dictionary<string, (byte, byte)>();
            var blocks = JsonSerializer.Deserialize<List<BlockInfo>>(File.ReadAllText(blockJsonPath));
            foreach (var b in blocks)
            {
                string name = Path.GetFileNameWithoutExtension(b.texture_image);
                map[name] = ((byte)b.block_id, (byte)b.data_id);
            }

            int height = blockGrid.GetLength(0);
            int width = blockGrid.GetLength(1);

            byte[] blockIds = new byte[width * height];
            byte[] dataIds = new byte[width * height];

            for (int z = 0; z < height; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = z * width + x;
                    string block = blockGrid[z, x];
                    if (map.ContainsKey(block))
                    {
                        (byte id, byte data) = map[block];
                        blockIds[index] = id;
                        dataIds[index] = data;
                    }
                }
            }

            return new Schematic((short)width, (short)height, blockIds, dataIds);
        }
    }
}

