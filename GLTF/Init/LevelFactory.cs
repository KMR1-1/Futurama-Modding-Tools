
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
                    System.Console.WriteLine($"level{lvl} : don't exist");
                }
            }
        }
        public List<string> GetAllLevel()
        {
            List<string> levelNumbers = new List<string>();
            foreach (var dir in Directory.GetDirectories(LvlDirectory))
            {
                string dirName = Path.GetFileName(dir); // Récupère le nom du dossier
                if (dirName.StartsWith("level"))
                {
                    string numberPart = dirName.Replace("level", ""); // Supprime "level"
                    levelNumbers.Add(numberPart);
                }
            }
            return levelNumbers;
        }
    }
}
