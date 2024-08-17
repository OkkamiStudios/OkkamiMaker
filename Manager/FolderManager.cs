using System;
using System.IO;

namespace OkkamiMaker.Manager
{
    public class FolderManager
    {
        private static readonly string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static readonly string suiteFolder = "OKsuite";
        private static readonly string appName = "OKMMaker";
        private static readonly string suitePath = Path.Combine(documentsPath, suiteFolder);
        private static readonly string appPath = Path.Combine(suitePath, appName);

        // Método para asegurarse de que las carpetas están creadas
        public static void EnsureProjectFolders()
        {
            try
            {
                CreateDirectoryIfNotExists(suitePath, "suite");
                CreateDirectoryIfNotExists(appPath, "aplicación");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Acceso denegado al crear carpetas: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error de entrada/salida al crear carpetas: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado al crear carpetas: {ex.Message}");
            }
        }

        // Método para obtener la ruta del proyecto y asegurar que las carpetas existen
        public static string GetProjectsPath()
        {
            EnsureProjectFolders();
            return appPath;
        }

        // Método auxiliar para crear directorios si no existen
        private static void CreateDirectoryIfNotExists(string path, string folderType)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Console.WriteLine($"Carpeta de {folderType} creada en: {path}");
            }
            else
            {
                Console.WriteLine($"La carpeta de {folderType} ya existe en: {path}");
            }
        }
    }
}
