using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json;

public class Block
{
    public string Identifier { get; set; }
    public string Category { get; set; }
    public string Group { get; set; }

    // Diccionario que almacena los componentes del bloque
    public Dictionary<string, object> Components { get; private set; } = new Dictionary<string, object>();

    public Block(string identifier, string category, string group)
    {
        Identifier = identifier;
        Category = category;
    }

    // Método para serializar el bloque a JSON
    public string ToJson()
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
                        category = Category,
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

    // Método para agregar "destructible_by_explosion" si es necesario
    public void DestructibleByExplosion(float explosionResistance)
    {
        Components["minecraft:destructible_by_explosion"] = new { explosion_resistance = explosionResistance };
    }

    // Método para agregar otros componentes si son necesarios
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

    public void Geometry(string geometryID)
    {
        Components["minecraft:geometry"] = geometryID;
    }

    // Método para agregar la caja de selección
    public void SelectionBox(bool hasSelectionBox)
    {
        Components["minecraft:selection_box"] = hasSelectionBox;
    }

    
}
