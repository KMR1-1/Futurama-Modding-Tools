using FuturamaLib.GLTF.Init;

namespace FuturamaLib.GLTF.Builder.NodeStructures
{
    public class BuildInstance : INode
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public List<float> Translations { get; set; }
        public List<float> Scale { get; set; }
        public List<float> Rotations { get; set; }
        public int? Mesh {get; set;}
        public List<INode> Children {get; set;}= new List<INode>();
        public long PosOffset {get; set;}
        
        public BuildInstance(ref Gltf gltf, INode instance) : base()
        {
            Type = instance.GetType().Name;
            Name = instance.Name;
            Id = ++gltf.counter.node;
            Children = new List<INode>();
            PosOffset = instance.PosOffset;
            Translations = instance.Translations;
            Rotations = instance.Rotations;
            Scale = instance.Scale;

            if (instance is MeshNode meshNode)
            {
                Type = "MeshNode";
                Mesh = meshNode.Mesh;
            }
            if(instance is ChildNode childNode)
                foreach (var child in childNode.Children)
                    Children.Add(new BuildInstance(ref gltf, child));
            
        }
    }
}