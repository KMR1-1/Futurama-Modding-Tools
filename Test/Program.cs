using System.Diagnostics;
using FuturamaLib.GLTF.Init;
using Project;

class Program
{
    public static void Main(string[] args)
    {
        WritingPos(path)
    }
    public void WritingPos(string path)
    {
        new ModLevel(path);
        
    }
    public void CreateProject()
    {
        var name = "Project1";
        var isoPath = "/home/kmr1/Futurama.iso";
        var offsets = new List<long>{
            2349, 397346, 885107
        };
        var option = new List<string>{"1-1"};
        new ProjectBuilder(isoPath, name, offsets, option);
    }
    
}