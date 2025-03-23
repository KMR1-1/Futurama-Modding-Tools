using FuturamaLib.GLTF.Init;
using FuturamaLib.GLTF.Calculs;
using FuturamaLib.NIF.Structures;
using System.Data.Common;

namespace FuturamaLib.GLTF.Builder.NodeStructures
{
    public class Node : INode
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public List<float> Translations { get; set; }
        public List<float> Scale { get; set; }
        public List<float> Rotations { get; set; }
        public virtual int? Mesh {get; set;}
        public virtual List<INode> Children {get; set;}
        public long PosOffset {get; set;}
        public Node(ref Gltf gltf, NiAVObject node): base()
        {
            Type = this.GetType().Name;
            Name = node.Name.Value.Replace(':', '_');
            if(string.IsNullOrEmpty(Name))
                Name = $"{gltf.counter.node}";
            Id = ++gltf.counter.node;
            gltf.variables.offsets.node[Name] = node.Offsets["pos"];
            Translations = new List<float> {node.Translation.X, node.Translation.Y, node.Translation.Z};
            Rotations = RotToQuat.Quat(node.Rotation);
            Scale = new List<float> { node.Scale, node.Scale, node.Scale };
        }
    }
}


