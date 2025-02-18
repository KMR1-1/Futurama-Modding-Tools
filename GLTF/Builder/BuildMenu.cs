using FuturamaLib.GLTF.Init;
using FuturamaLib.GLTF.Builder.NodeStructures;

namespace FuturamaLib.GLTF.Builder
{
    class BuildMenu
    {
        public BuildMenu(string projDir, List<string> fileNames)
        {
            string menuDir = Path.Combine(projDir, "Extracted", "menu");
            var gltf = new Gltf(menuDir, projDir);
            gltf.variables.folderManager.Misc();
            gltf.variables.readers.GetReaders(fileNames);
            for (int i = 0; i < gltf.variables.readers.Readers.Count; i++)
            {
                var reader = gltf.variables.readers.Readers[i];
                var stageName = gltf.variables.readers.stageNames[i];
                new NodeStructure(ref gltf, reader, stageName);
            }
        }
    }
}