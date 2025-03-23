using Newtonsoft.Json;

public class Offsets
{
    public Dictionary<string, long> node = new Dictionary<string, long>();
    public Dictionary<string, long> mesh = new Dictionary<string, long>();
    public Dictionary<string, long> image = new Dictionary<string, long>();
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
        node = new Dictionary<string, long>();
        mesh = new Dictionary<string, long>();
        image = new Dictionary<string, long>();
        return JsonConvert.SerializeObject(offsetDict, Formatting.Indented);
    }
}