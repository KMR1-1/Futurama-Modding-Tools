

using System.Numerics;

class WriteSize
{
    public float size { get; set; }
    public GltfVariables gltf {get;set;}
    public WriteSize(GltfVariables gltf, int nodeId, float size)
    {
        this.size = size;
        this.gltf = gltf;
        var offset = gltf.GetPosOffset(nodeId) + 48;
        if (File.Exists(gltf.isoPath))
        {
            using (FileStream fs = new FileStream(gltf.isoPath, FileMode.Open, FileAccess.ReadWrite))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                fs.Seek(offset, SeekOrigin.Begin);

                writer.Write(size);
                System.Console.WriteLine($"pos at {offset} overriten in {gltf.isoPath}");
            }
        }
        else
        {
            System.Console.WriteLine($"could not write pos in {offset}");
        }
        
    }
    public void GetToOffset()
    {
        
    }

}