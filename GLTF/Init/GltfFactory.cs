using FuturamaLib.GLTF.Builder;

namespace FuturamaLib.GLTF.Init
{
    public class GltfFactory
    {
        public string projDir;
        public GltfFactory(string projDir)
        {
            this.projDir = projDir;
        }
        public void Level(List<string> option)
        {
            var level = new LevelFactory(projDir);
            level.ConvertLevels(option);
        }
        public void Menu(List<string> fileNames)
        {
            new BuildMenu(projDir, fileNames);
        }
    }
}