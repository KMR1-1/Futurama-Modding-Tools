namespace Project
{
    public class CreateProject
    {
        string projectPath {get; set;}
        public CreateProject(string projectPath, string isoPath)
        {
            this.projectPath =  projectPath;
            ManageFolder();
            CopyIso(isoPath);
            
        }
        public void ManageFolder()
        {
            if (!Directory.Exists(projectPath))
            {
                Directory.CreateDirectory(projectPath);
                var extractedDir = Path.Combine(projectPath, "Extracted");
                Directory.CreateDirectory(extractedDir);
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
    }
}