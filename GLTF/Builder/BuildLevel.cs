using FuturamaLib.GLTF.Init;
using FuturamaLib.GLTF.Builder.NodeStructures;

namespace FuturamaLib.GLTF.Builder
{
    class BuildLevel
    {
        public BuildLevel(string levelDir, string projDir)
        {
            if (Path.Exists(levelDir))
            {
                var gltf = new Gltf(levelDir, projDir);
                gltf.variables.readers.GetLevelReaders();
                new GetLevelInstances(ref gltf, levelDir);
                for (int i = 0; i < gltf.variables.readers.Readers.Count; i++)
                {
                    var reader = gltf.variables.readers.Readers[i];
                    var stageName = gltf.variables.readers.stageNames[i];
                    new NodeStructure(ref gltf, reader, stageName);
                }
            }
            else
            {
                System.Console.WriteLine($"level path: {levelDir} does not exists");
            }
            //new LevelStructure(ref gltf);

        }
    }
}