
using FuturamaLib.GLTF.Builder;

namespace FuturamaLib.GLTF.Init
{
    public class LevelFactory
    {
        public string LvlDirectory { get; }
        public string projDir { get; set; }
        public LevelFactory(string projDir)

        {
            this.projDir = projDir;
            LvlDirectory = Path.Combine(projDir, "Extracted", "levels");
            if (!Directory.Exists(LvlDirectory))
            {
                System.Console.WriteLine($"{LvlDirectory} dir not found");
            }
        }
        public void ConvertLevels(List<string> option)
        {
            foreach (var lvl in option)
            {
                var levelDir = Path.Combine(LvlDirectory, $"level{lvl}");
                if (Path.Exists(levelDir))
                {
                    Console.WriteLine($"Level: {Path.GetFileName(levelDir)}");
                    new BuildLevel(levelDir, projDir);
                }
                else
                {
                    System.Console.WriteLine($"level{lvl} : not foundg");
                }
            }
        }
    }
}
