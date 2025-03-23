using FuturamaLib.GLTF.Builder;

namespace FuturamaLib.GLTF.Init
{
    public class GltfFactory
    {
        public string projectPath;
        public GltfFactory(string projectPath)
        {
            this.projectPath = projectPath;
        }
        public void Level(List<string> option = null)
        {
            var level = new LevelFactory(projectPath);
            if(option == null || option.Count == 0)
            {
                option = level.GetAllLevel();
            }
            level.ConvertLevels(option);
        }
        public void Menu(List<string> fileNames)
        {
            new BuildMenu(projectPath, fileNames);
        }
    }
}