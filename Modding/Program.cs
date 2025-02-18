using System.Numerics;

class Program
{
    public static void Main(string[] args)
    {
        var projDir = "/home/kmr1/Futurama-modding/Project1";
        var gltfPath = "/home/kmr1/Futurama-modding/Project1/Converted/level1-1/Instance/Instance.gltf";
        int nodeID = 220;
        Vector3 translation = new Vector3(350.78986f, 5.916136f, 300.06999f);
        var size = 0.5f;
        var gltf = new GltfVariables(gltfPath);
        new WritePos(gltf, nodeID, translation);
        new WriteSize(gltf, nodeID, size);
    }
}