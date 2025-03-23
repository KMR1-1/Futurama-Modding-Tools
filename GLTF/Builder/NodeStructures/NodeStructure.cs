using FuturamaLib.GLTF.Init;
using FuturamaLib.NIF;
using FuturamaLib.NIF.Structures;

namespace FuturamaLib.GLTF.Builder.NodeStructures
{
    class NodeStructure
    {
        public NodeStructure(ref Gltf gltf, NIFReader reader, string stageName)
        {
            var forbidden = new List<string>
            {
                "AINets",
                "Waypoints",
                "Triggers",
                "Lights",
                "Instances"
            };

            foreach (var rootNode in reader.Footer.RootNodes)
            {
                if (rootNode.Object is NiNode niNode && !forbidden.Any(niNode.Name.Value.Contains))
                {
                    var relativeFilePath = Path.Combine(gltf.variables.folderManager.relativePath, $"{stageName}.nif");
                    var extradict = new Dictionary<string, object> { { "filePath", relativeFilePath } };
                    gltf.variables.folderManager.CreateStage(stageName);
                    gltf.structure.scenes.Add(0);
                    var name = niNode.Name.Value;
                    System.Console.WriteLine(name);
                    gltf.variables.folderManager.CreateRoom(name, stageName);
                    gltf.structure.nodeStructure = new List<Node> { new BasicNode(ref gltf, niNode) };
                    new DebugStructure<Node>(gltf.structure.nodeStructure, gltf, name);
                    new GBuild(ref gltf);
                }
            }
        }
    }
}