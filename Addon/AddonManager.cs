

namespace OkkamiMaker.Addon
{
    public class Addon
    {
        string AddonPath { get; set; }
        public Addon(string addonPath) 
        {
            AddonPath = addonPath;
        }

        public void AddItem(string name, string item)
        {
            // Asegurarse de que el directorio exista
            string directoryPath = Path.GetDirectoryName(AddonPath + $"\\bh\\items\\{name}.item.json");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Escribir el contenido en el archivo
            File.WriteAllText(AddonPath + $"\\bh\\items\\{name}.item.json", item);
        }

        public void AddBlock(string name, string block)
        {
            // Asegurarse de que el directorio exista
            string directoryPath = Path.GetDirectoryName(AddonPath + $"\\bh\\blocks\\{name}.block.json");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Escribir el contenido en el archivo
            File.WriteAllText(AddonPath + $"\\bh\\blocks\\{name}.block.json", block);
        }

    }
}
