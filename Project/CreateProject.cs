using FuturamaLib.GLTF.Init;

namespace Project
{
    public class CreateProject
    {
        string projectPath {get; set;}
        public CreateProject(string projectPath)
        {
            this.projectPath = projectPath;
        }
        public void ManageFolder()
        {
            if (!Directory.Exists(projectPath))
            {
                Directory.CreateDirectory(projectPath);
                var convertedDir = Path.Combine(projectPath, "Converted");
                Directory.CreateDirectory(convertedDir);
            }
            else
            {
                System.Console.WriteLine("Project Directory already exists");
            }
            
        }
        public void CopyIso(string isoPath)
        {
            if (File.Exists(isoPath))
            {
                string isoName = "Futurama.iso";
                string destinationPath = Path.Combine(projectPath, isoName);
                if (!File.Exists(destinationPath))
                {
                    File.Copy(isoPath, destinationPath, overwrite: true);
                }
                else
                {
                    System.Console.WriteLine("Iso already exists");
                }
            }
            else
            {
                System.Console.WriteLine("Iso Not Found");
            }
        }
        public void ExtractImg(List<long> offsets)
        {
            var extractor = new ImgExtractor(offsets, projectPath);
            foreach (var offset in offsets)
            {
                extractor.ListFiles(offset*2048);
            }
            extractor.Close(projectPath);
        }
        public void ConvertToGltf(List<string> option=null)
        {
            var gltf = new GltfFactory(projectPath);
            gltf.Level(option);
        }
    }
}