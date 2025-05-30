using JpgToMinecraftConverter.Models;

namespace JpgToMinecraftConverter.Interfaces
{
    public interface ISchematicBuilder
    {
        Schematic Build(string[,] blockGrid, string blockJsonPath);
    }
}
