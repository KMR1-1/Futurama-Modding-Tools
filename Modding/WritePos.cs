using System.Numerics;
namespace Modding{
class WritePos
{
    public Vector3 translation { get; set; }
    public GltfVariables gltf {get;set;}
    public WritePos(GltfVariables gltf, int nodeId, Vector3 translation)
    {
        this.translation = translation;
        this.gltf = gltf;
        var offset = gltf.GetPosOffset(nodeId);
        if (File.Exists(gltf.isoPath))
        {
            using (FileStream fs = new FileStream(gltf.isoPath, FileMode.Open, FileAccess.ReadWrite))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                fs.Seek(offset, SeekOrigin.Begin);

                writer.Write(translation.X);
                writer.Write(translation.Y);
                writer.Write(translation.Z);
                System.Console.WriteLine($"pos at {offset} overriten in {gltf.isoPath}");
            }
        }
        else
        {
            System.Console.WriteLine($"could not write pos in {offset}");
        }
    }
    public void WritePosToFile()
    {

    }
    public void GetPos()
    {
        
    }
}
}