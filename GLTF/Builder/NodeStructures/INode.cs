namespace FuturamaLib.GLTF.Builder.NodeStructures
{
    public interface INode
    {
        string Type { get; set; }
        string Name { get; set; }
        int Id { get; set; }
        List<float> Translations { get; set; }
        List<float> Scale { get; set; }
        List<float> Rotations { get; set; }
        List<INode> Children {get; set;}
        int? Mesh {get;set;}
        long PosOffset {get; set;}
    }
}