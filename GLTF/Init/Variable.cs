namespace FuturamaLib.GLTF.Init
{
    public class Variables
    {
        public NifManager readers {get; set;}
        public FolderManager folderManager {get;}
        public Offsets offsets;
        public Variables(string rootDir, string projDir)
        {
            folderManager = new FolderManager(rootDir, projDir);
            readers = new NifManager(rootDir);
            folderManager.InitOutDir();
        }     
    }
}
