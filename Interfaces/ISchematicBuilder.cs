namespace JpgToMinecraftConverter.Interfaces
{
    using JpgToMinecraftConverter.Models;

    interface ISchematicBuilder
    {
        Schematic Build(string[,] blockGrid, string blockJsonPath);
    }
}
