using FuturamaLib.NIF.Structures;
using FuturamaLib.GLTF.Init;

namespace FuturamaLib.GLTF.Builder.NodeStructures
{
    class GetLevelInstances
    {
        public GetLevelInstances(ref Gltf gltf, string levelDir)
        {
            gltf.variables.folderManager.CreateInstance(gltf.variables.readers.stageNames);
            gltf.variables.folderManager.Misc();
            var extradict = new Dictionary<string, object>{{"filePath", gltf.variables.folderManager.relativePath}};
            gltf.structure.asset["extras"] = extradict;
            GetInstanceStructure(ref gltf);
        }
        public void GetInstanceStructure(ref Gltf gltf)
        {
            var defsRootNodes = new List<Node>();

            foreach (var root in gltf.variables.readers.defsReader.Footer.RootNodes)
                if (root.Object is NiNode niNode)
                    defsRootNodes.Add(new InstanceNode(ref gltf, niNode));

            new DebugStructure<Node>(defsRootNodes, gltf, "defsStructure");
            gltf.structure.instList = defsRootNodes;
            gltf.counter.Kill();
            new BuildLevelInstance(ref gltf);

        } 
    }
}