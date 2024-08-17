
using System.Text.Json;

namespace OkkamiMaker.Minecraft
{
    public class Block
    {
        public string Identifier { get; set; }
        public string Category { get; set; }
        public string Group { get; set; }

        // Diccionario que almacena los componentes del bloque
        public Dictionary<string, object> Components { get; private set; } = new Dictionary<string, object>();

        public Block(string identifier)
        {
            Identifier = identifier;
        }

        // Método para serializar el bloque a JSON
        public string Compile()
        {
            var blockObject = new
            {
                format_version = "1.20.20",
                minecraft_block = new
                {
                    description = new
                    {
                        identifier = Identifier,
                        menu_category = new
                        {
                            category = Category
                        }
                    },
                    components = Components
                }
            };

            return JsonSerializer.Serialize(blockObject, new JsonSerializerOptions { WriteIndented = true });
        }

        // Método para agregar el componente "map_color" si es necesario
        public void MapColor(string color)
        {
            if (!string.IsNullOrEmpty(color))
            {
                Components["minecraft:map_color"] = color;
            }
        }

        // Método para agregar "destructible_by_explosion" con resistencia personalizada
        public void ExplosionResistance(float resistance)
        {
            Components["minecraft:destructible_by_explosion"] = new { explosion_resistance = resistance };
        }

        // Método para agregar "collision_box"
        public void CollisionBox(bool hasCollision, float[] origin = null, float[] size = null)
        {
            if (hasCollision)
            {
                Components["minecraft:collision_box"] = new
                {
                    origin = origin ?? new float[] { -8.0f, 0.0f, -8.0f },
                    size = size ?? new float[] { 16.0f, 16.0f, 16.0f }
                };
            }
            else
            {
                Components["minecraft:collision_box"] = false;
            }
        }

        // Método para agregar geometría personalizada
        public void Geometry(string geometryID)
        {
            Components["minecraft:geometry"] = geometryID;
        }

        // Método para agregar la caja de selección
        public void SelectionBox(bool hasSelectionBox)
        {
            Components["minecraft:selection_box"] = hasSelectionBox;
        }

        // Método para agregar fricción
        public void Friction(float friction)
        {
            Components["minecraft:friction"] = friction;
        }

        // Método para agregar resistencia al minado
        public void MiningResistance(float secondsToDestroy)
        {
            Components["minecraft:destructible_by_mining"] = new { seconds_to_destroy = secondsToDestroy };
        }

        // Método para agregar emisión de luz
        public void LightEmission(int lightEmission)
        {
            Components["minecraft:light_emission"] = lightEmission;
        }

        // Método para definir el nombre visible del bloque
        public void DisplayName(string displayName)
        {
            Components["minecraft:display_name"] = displayName;
        }

        public void CraftingTable(string TableName, List<string> CraftingTags)
        {
            Components["minecraft:crafting_table"] = new { table_name = TableName, crafting_tags = CraftingTags };
        }
    }
}
