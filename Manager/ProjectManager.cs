using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace OkkamiMaker.Manager
{
    public class ProjectManager
    {
        // Método para buscar proyectos por nombre
        public static List<string> SearchProjectsByName(string searchName)
        {
            List<string> matchingProjects = new List<string>();
            string projectsPath = FolderManager.GetProjectsPath();

            if (Directory.Exists(projectsPath))
            {
                var projectDirectories = Directory.GetDirectories(projectsPath);

                foreach (var projectDir in projectDirectories)
                {
                    string projectFilePath = Path.Combine(projectDir, "ProjectOKM.json");

                    if (File.Exists(projectFilePath))
                    {
                        try
                        {
                            string jsonContent = File.ReadAllText(projectFilePath);
                            var projectData = JsonConvert.DeserializeObject<ProjectData>(jsonContent);

                            // Comparar nombre
                            if (projectData.Metadata.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase))
                            {
                                matchingProjects.Add(projectDir);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error al leer el archivo JSON en {projectDir}: {ex.Message}");
                        }
                    }
                }
            }

            return matchingProjects;
        }

        // Método para buscar proyectos por UUID
        public static List<string> SearchProjectsByUuid(string uuid)
        {
            List<string> matchingProjects = new List<string>();
            string projectsPath = FolderManager.GetProjectsPath();

            if (Directory.Exists(projectsPath))
            {
                var projectDirectories = Directory.GetDirectories(projectsPath);

                foreach (var projectDir in projectDirectories)
                {
                    string projectFilePath = Path.Combine(projectDir, "ProjectOKM.json");

                    if (File.Exists(projectFilePath))
                    {
                        try
                        {
                            string jsonContent = File.ReadAllText(projectFilePath);
                            var projectData = JsonConvert.DeserializeObject<ProjectData>(jsonContent);

                            // Comparar UUID (metadata, resource y data)
                            if (projectData.Metadata.Uuid.Equals(uuid, StringComparison.OrdinalIgnoreCase) ||
                                (projectData.Content.Resource?.Uuid?.Equals(uuid, StringComparison.OrdinalIgnoreCase) ?? false) ||
                                (projectData.Content.Data?.Uuid?.Equals(uuid, StringComparison.OrdinalIgnoreCase) ?? false))
                            {
                                matchingProjects.Add(projectDir);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error al leer el archivo JSON en {projectDir}: {ex.Message}");
                        }
                    }
                }
            }

            return matchingProjects;
        }

        // Método para obtener una lista de todos los proyectos con sus metadatos
        public static List<ProjectMetadata> GetAllProjects()
        {
            List<ProjectMetadata> allProjects = new List<ProjectMetadata>();
            string projectsPath = FolderManager.GetProjectsPath();

            if (Directory.Exists(projectsPath))
            {
                var projectDirectories = Directory.GetDirectories(projectsPath);

                foreach (var projectDir in projectDirectories)
                {
                    string projectFilePath = Path.Combine(projectDir, "ProjectOKM.json");

                    if (File.Exists(projectFilePath))
                    {
                        try
                        {
                            string jsonContent = File.ReadAllText(projectFilePath);
                            var projectData = JsonConvert.DeserializeObject<ProjectData>(jsonContent);

                            // Agregar el proyecto a la lista
                            allProjects.Add(new ProjectMetadata
                            {
                                Name = projectData.Metadata.Name,
                                Uuid = projectData.Metadata.Uuid,
                                Directory = projectDir,
                                ResourceExists = projectData.Content.Resource?.Exist ?? false,
                                DataExists = projectData.Content.Data?.Exist ?? false
                            });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error al leer el archivo JSON en {projectDir}: {ex.Message}");
                        }
                    }
                }
            }

            return allProjects;
        }

        // Método para crear un nuevo proyecto
        public static void CreateProject(string projectName)
        {
            string projectsPath = FolderManager.GetProjectsPath();
            string projectDir = Path.Combine(projectsPath, projectName);

            if (Directory.Exists(projectDir))
            {
                Console.WriteLine("El proyecto ya existe.");
                return;
            }

            // Crear la carpeta del proyecto
            Directory.CreateDirectory(projectDir);
            Console.WriteLine($"Carpeta del proyecto creada en: {projectDir}");

            // Preguntar al usuario sobre resource pack y behavior pack
            bool useResourcePack = AskUser("¿Quieres usar resource pack? (s/n)");
            bool useBehaviorPack = AskUser("¿Quieres usar behavior pack? (s/n)");

            // Crear el archivo ProjectOKM.json
            var projectData = new ProjectData
            {
                Metadata = new Metadata
                {
                    Name = projectName,
                    Uuid = Guid.NewGuid().ToString() // Generar un UUID para el proyecto
                },
                Content = new Content
                {
                    Resource = new Resource
                    {
                        Exist = useResourcePack,
                        Uuid = useResourcePack ? Guid.NewGuid().ToString() : null
                    },
                    Data = new Data
                    {
                        Exist = useBehaviorPack,
                        Uuid = useBehaviorPack ? Guid.NewGuid().ToString() : null
                    }
                }
            };

            string jsonContent = JsonConvert.SerializeObject(projectData, Formatting.Indented);
            string projectFilePath = Path.Combine(projectDir, "ProjectOKM.json");

            File.WriteAllText(projectFilePath, jsonContent);
            Console.WriteLine($"Archivo ProjectOKM.json creado en: {projectFilePath}");

            // Crear carpetas rp y bh si corresponde
            if (useResourcePack)
            {
                Directory.CreateDirectory(Path.Combine(projectDir, "rp"));
                Console.WriteLine("Carpeta rp creada.");
            }

            if (useBehaviorPack)
            {
                Directory.CreateDirectory(Path.Combine(projectDir, "bh"));
                Console.WriteLine("Carpeta bh creada.");
            }
        }

        // Método para preguntar al usuario
        private static bool AskUser(string question)
        {
            Console.WriteLine(question);
            string response = Console.ReadLine()?.Trim().ToLower();
            return response == "s" || response == "si";
        }

        // Método para obtener la ruta de un proyecto por nombre o UUID
        public static string GetProjectPath(string searchValue)
        {
            string projectsPath = FolderManager.GetProjectsPath();

            if (Directory.Exists(projectsPath))
            {
                var projectDirectories = Directory.GetDirectories(projectsPath);

                foreach (var projectDir in projectDirectories)
                {
                    string projectFilePath = Path.Combine(projectDir, "ProjectOKM.json");

                    if (File.Exists(projectFilePath))
                    {
                        try
                        {
                            string jsonContent = File.ReadAllText(projectFilePath);
                            var projectData = JsonConvert.DeserializeObject<ProjectData>(jsonContent);

                            // Comparar nombre o UUID
                            if (projectData.Metadata.Name.Equals(searchValue, StringComparison.OrdinalIgnoreCase) ||
                                projectData.Metadata.Uuid.Equals(searchValue, StringComparison.OrdinalIgnoreCase) ||
                                (projectData.Content.Resource?.Uuid?.Equals(searchValue, StringComparison.OrdinalIgnoreCase) ?? false) ||
                                (projectData.Content.Data?.Uuid?.Equals(searchValue, StringComparison.OrdinalIgnoreCase) ?? false))
                            {
                                return projectDir;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error al leer el archivo JSON en {projectDir}: {ex.Message}");
                        }
                    }
                }
            }

            return null; // O puedes lanzar una excepción si prefieres
        }
    }

    // Clases para deserializar el archivo JSON
    public class ProjectData
    {
        public Metadata Metadata { get; set; }
        public Content Content { get; set; }
    }

    public class Metadata
    {
        public string Name { get; set; }
        public string Uuid { get; set; }
    }

    public class Content
    {
        public Resource Resource { get; set; }
        public Data Data { get; set; }
    }

    public class Resource
    {
        public bool Exist { get; set; }
        public string Uuid { get; set; }
    }

    public class Data
    {
        public bool Exist { get; set; }
        public string Uuid { get; set; }
    }

    // Clase para devolver los metadatos del proyecto
    public class ProjectMetadata
    {
        public string Name { get; set; }
        public string Uuid { get; set; }
        public string Directory { get; set; }
        public bool ResourceExists { get; set; }
        public bool DataExists { get; set; }
    }
}
