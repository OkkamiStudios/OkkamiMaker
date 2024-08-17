using System;
using System.Collections.Generic;
using System.Text.Json;

namespace OkkamiMaker.Minecraft
{
    public class Item
    {
        public string Identifier { get; set; }
        string Category { get; set; }
        string Group { get; set; }

        // Diccionario que almacena los componentes del ítem
        public Dictionary<string, object> Components { get; private set; } = new Dictionary<string, object>();
        public Dictionary<string, object> Description { get; private set; } = new Dictionary<string, object>();
        public Item(string identifier)
        {
            Identifier = identifier;
            Description["identifier"] = Identifier;
        }

        public string Compile()
        {
            var itemObject = new
            {
                format_version = "1.20.50",
                minecraft_item = new
                {
                    description = Description,
                    components = Components
                }
            };

            return JsonSerializer.Serialize(itemObject, new JsonSerializerOptions { WriteIndented = true });
        }
        public void MenuCategory()
        {
            
        }
        // Métodos para agregar componentes
        public void Icon(string texture)
        {
            Components["minecraft:icon"] = new { texture };
        }

        public void AllowOffHand(bool value)
        {
            Components["minecraft:allow_off_hand"] = new { value };
        }

        public void BlockPlacer(string block, params string[] useOn)
        {
            Components["minecraft:block_placer"] = new
            {
                block,
                use_on = useOn
            };
        }

        public void CanDestroyInCreative(bool value)
        {
            Components["minecraft:can_destroy_in_creative"] = new { value };
        }

        public void SetCooldown(string category, double duration)
        {
            Components["minecraft:cooldown"] = new
            {
                category,
                duration
            };
        }

        public void Damage(int value)
        {
            Components["minecraft:damage"] = new { value };
        }

        public void Digger(bool useEfficiency, string tag, double speed)
        {
            Components["minecraft:digger"] = new
            {
                use_efficiency = useEfficiency,
                destroy_speeds = new[]
                {
                    new { block = new { tags = tag }, speed }
                }
            };
        }

        public void DisplayName(string value)
        {
            Components["minecraft:display_name"] = new { value };
        }

        public void Durability(int minDamage, int maxDamage, int maxDurability)
        {
            Components["minecraft:durability"] = new
            {
                damage_chance = new { min = minDamage, max = maxDamage },
                max_durability = maxDurability
            };
        }

        public void Enchantable(string slot, int value)
        {
            Components["minecraft:enchantable"] = new { slot, value };
        }

        public void EntityPlacer(string entity, params string[] useOn)
        {
            Components["minecraft:entity_placer"] = new
            {
                entity,
                use_on = useOn
            };
        }

        public void Food(int nutrition, double saturationModifier, string usingConvertsTo)
        {
            Components["minecraft:food"] = new
            {
                nutrition,
                saturation_modifier = saturationModifier,
                using_converts_to = usingConvertsTo
            };
        }

        public void Fuel(double duration)
        {
            Components["minecraft:fuel"] = new { duration };
        }

        public void Glint(bool value)
        {
            Components["minecraft:glint"] = new { value };
        }

        public void HandEquipped(bool value)
        {
            Components["minecraft:hand_equipped"] = new { value };
        }

        public void HoverTextColor(string color)
        {
            Components["minecraft:hover_text_color"] = color;
        }

        public void InteractButton(string text)
        {
            Components["minecraft:interact_button"] = text;
        }

        public void LiquidClipped(bool value)
        {
            Components["minecraft:liquid_clipped"] = new { value };
        }

        public void MaxStackSize(int value = 64)
        {
            Components["minecraft:max_stack_size"] = new { value };
        }

        public void Projectile(double minimumCriticalPower, string projectileEntity)
        {
            Components["minecraft:projectile"] = new
            {
                minimum_critical_power = minimumCriticalPower,
                projectile_entity = projectileEntity
            };
        }

        public void Record(int comparatorSignal, double duration, string soundEvent)
        {
            Components["minecraft:record"] = new
            {
                comparator_signal = comparatorSignal,
                duration,
                sound_event = soundEvent
            };
        }

        public void Shooter(string item, bool useOffhand, bool searchInventory, bool useInCreative)
        {
            Components["minecraft:shooter"] = new
            {
                ammunition = new[]
                {
                    new
                    {
                        item,
                        use_offhand = useOffhand,
                        search_inventory = searchInventory,
                        use_in_creative = useInCreative
                    }
                }
            };
        }

        public void Wearable(string slot, bool dispensable)
        {
            Components["minecraft:wearable"] = new
            {
                slot,
                dispensable
            };
        }
    }
}
