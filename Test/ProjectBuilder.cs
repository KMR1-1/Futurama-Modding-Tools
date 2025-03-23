using Project;

class ProjectBuilder
{
    public string homePath {get; set;}
    public string projectPath {get; set;}

    public ProjectBuilder(string isoPath, string projectName, List<long> offsets, List<string> option)
    {

    }
    public void BuildProject(string isoPath, string projectName, List<long> offsets, List<string> option)
    {
        var homePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var parentPath = Path.Combine(homePath, "Documents", "FuturamaModding");
        if(!Path.Exists(parentPath))
        {
            Directory.CreateDirectory(parentPath);
        }
        var projectPath = Path.Combine(parentPath, projectName);
        var project = new CreateProject(projectPath);
        project.ManageFolder();
        project.CopyIso(isoPath);
        project.ExtractImg(offsets);
        project.ConvertToGltf(option);
    }
}