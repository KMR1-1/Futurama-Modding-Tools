namespace FuturamaLib.GLTF.Init
{
    public class FolderManager
    {
        public string rootDir { get; }
        public string outPutPath { get; }
        public string roomPath { get; set; }
        public string relativePath {get; set;}
        public FolderManager(string rootDir, string projDir)
        {
            this.rootDir = rootDir;
            outPutPath = Path.Combine(projDir, "Converted", Path.GetFileName(rootDir));
            relativePath = GetRelativePath(projDir, rootDir);
        }
        public void InitOutDir()
        {
            if (Directory.Exists(outPutPath))
                Directory.Delete(outPutPath, recursive: true);
            Directory.CreateDirectory(outPutPath);
            var debugPath = Path.Combine(outPutPath, "debug");
            Directory.CreateDirectory(debugPath);
        }
        public string GetRelativePath(string projDir, string rootDir)
        {
            Uri extractedUri = new Uri(Path.Combine(projDir, "Extracted"));
            Uri fileUri = new Uri(rootDir);
            Uri relativeUri = extractedUri.MakeRelativeUri(fileUri);
            string relativePath = Uri.UnescapeDataString(relativeUri.ToString());
            relativePath = relativePath.Substring("Extracted/".Length - 1);
            return relativePath;
        }
        public void CreateRoom(string roomName, string stageName)
        {
            roomPath = Path.Combine(outPutPath, stageName, roomName);
            Directory.CreateDirectory(roomPath);
            Directory.CreateDirectory(Path.Combine(roomPath, "data"));
            Directory.CreateDirectory(Path.Combine(roomPath, "texture"));
        }
        public void CreateInstance(List<string> stageNames)
        {
            roomPath = Path.Combine(outPutPath, "Instance");
            Directory.CreateDirectory(roomPath);
            Directory.CreateDirectory(Path.Combine(roomPath, "data"));
            Directory.CreateDirectory(Path.Combine(roomPath, "texture"));
        }
        public void Misc()
        {
            Directory.CreateDirectory(Path.Combine(outPutPath, "external"));
        }
        public void CreateStage(string name)
        {
            var stagepath = Path.Combine(outPutPath, name);
            if (!Path.Exists(stagepath))
            {
                Directory.CreateDirectory(stagepath);
            }
        }
    }

}