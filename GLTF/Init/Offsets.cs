using Newtonsoft.Json;

public class Offsets
{
    public Dictionary<string, long> node;
    public Dictionary<string, long> mesh;
    public Dictionary<string, long> image;
    public Offsets()
    {
    }
    public string ToDict()
    {
        var offsetDict = new Dictionary<string, object>
        {
            {"node", node},
            {"mesh", mesh},
            {"image", image},
        };
        return JsonConvert.SerializeObject(offsetDict, Formatting.Indented);
    }
}