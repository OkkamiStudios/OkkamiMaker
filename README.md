# OkkamiMaker

## Basic code


'''

using OkkamiMaker.Addon;
using OkkamiMaker.Manager;
using OkkamiMaker.Minecraft;

public class Program
{
    public static void Main()
    {
        Addon Addon = new Addon(ProjectManager.GetProjectPath("Addon tetas"));

        Item sword = new Item("prueba:1");
        sword.Damage(11);


        Addon.AddItem("sword", sword.Compile());
    }

}

'''