using System.Diagnostics;
using FuturamaLib.GLTF.Init;
using Project;
using SharpGLTF;
using SharpGLTF.Schema2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    public static void Main(string[] args)
    {
        ConvertLevel();
    }
    public static void ConvertLevel()
    {
        string lvl = "1-1";
        string projectPath = "/home/kmr1/Documents/FuturamaModding/Project1";
        var level = new List<string> { lvl };
        var levelFactory = new GltfFactory(projectPath);
        levelFactory.Level(level);
    }
    public void CreateProject()
    {
        var name = "Project1";
        var isoPath = "/home/kmr1/Futurama.iso";
        var offsets = new List<long>{
            2349, 397346, 885107
        };
        var option = new List<string> { "1-1" };
        new ProjectBuilder(isoPath, name, offsets, option);
    }
}