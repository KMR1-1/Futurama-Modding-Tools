using FuturamaLib.GLTF.Init;
using FuturamaLib.NIF.Structures;
namespace FuturamaLib.GLTF.Builder.NodeStructures
{
    class BuildLevelInstance
    {
        public BuildLevelInstance(ref Gltf gltf)
        {
            var instRootNode = new List<Node>();
            for (int i = 0; i < gltf.variables.readers.Readers.Count; i++)
            {
                var reader = gltf.variables.readers.Readers[i];
                var fileName = gltf.variables.readers.stageNames[i];
                foreach (var rootNode in reader.Footer.RootNodes)
                {
                    if (rootNode.Object is NiNode niNode && niNode.Name.Value == "Instances")
                    {
                        if (gltf.counter.node + 1 != 0)
                        {
                            gltf.structure.scenes.Add(gltf.counter.node + 1);
                        }
                        instRootNode.Add(new InstancesNode(ref gltf, niNode, fileName));
                    }
                }
            }
            new DebugStructure<Node>(instRootNode, gltf, "LevelInstances");
            gltf.structure.nodeStructure = instRootNode;
            gltf.structure.scenes.Add(0);
            new GBuild(ref gltf);
        }
    }
}
