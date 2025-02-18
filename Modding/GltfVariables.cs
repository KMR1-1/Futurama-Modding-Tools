using Newtonsoft.Json.Linq;

class GltfVariables
{
    public string projectPath { get; set; }
    public string isoPath { get; set; }
    public JObject jsonObject { get; set; }
    public JObject filePaths {get; set;}
    public long fileOffset {get; set;}
    public string relativePath { get; set; }
    public bool IsInstance { get; set; } = false;
    public GltfVariables(string gltfPath)
    {
        projectPath = GetProjectPath(gltfPath);
        jsonObject = JObject.Parse(File.ReadAllText(gltfPath));
        filePaths = GetFilePaths();
        isoPath = Path.Combine(projectPath, "Futurama.iso");


        if (Path.GetFileName(gltfPath) != "Instance.gltf")
        {
            relativePath = (string)jsonObject["asset"]?["extras"]?["filePath"]; 
            fileOffset = GetFileOffset();
        }
        else
        {
            IsInstance = true;
        }
    }
    public string GetProjectPath(string gltfPath)
    {
        string[] parts = gltfPath.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        int index = Array.IndexOf(parts, "Converted");
        return string.Join("/", parts[..index]);
    }
    public JObject GetFilePaths()
    {
        var filePaths = Path.Combine(projectPath, "filePaths.json");
        string jsonText = File.ReadAllText(filePaths);
        return JObject.Parse(jsonText);
    }
    public void GetInstancePath(int nodeId)
    {
        var relativeDir = (string)jsonObject["asset"]?["extras"]?["filePath"]; 
        var fileName = (string)jsonObject["nodes"]?[nodeId]?["extras"]?["file"];
        relativePath = Path.Combine(relativeDir,$"{fileName}.nif");
    }
    public long GetFileOffset()
    {
        if (filePaths.TryGetValue(relativePath, out JToken value))
        {
            return value.ToObject<long>();
        }
        else
        {
            return 0;
        }
    }
    public long GetPosOffset(int nodeId)
    {
        long nodepos = (long)jsonObject["nodes"]?[nodeId]?["extras"]?["posOffset"];
        if(IsInstance)
        {
            GetInstancePath(nodeId);
            fileOffset = GetFileOffset();
        }
        return nodepos + fileOffset;
    }
}