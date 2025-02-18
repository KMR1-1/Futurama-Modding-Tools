public class GltfCounter
{
    public int node { get; set; }
    public int mesh { get; set; }
    public int accessor { get; set; }
    public int texture { get; set; }
    public int image { get; set; }
    public int room { get; set; }
    public int bufferOffset { get; set; }
    public Dictionary<uint, int> refIdToCounter;
    public GltfCounter()
    {
    }
    public void Kill()
    {
        refIdToCounter = new Dictionary<uint, int>();

        node = -1;
        mesh = 0;
        accessor = 0;
        texture = 0;
        image = 0;
        room = 0;
    }
}