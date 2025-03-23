using SharpGLTF.Schema2;
class ConvertToGbl
{
    public string inpath;
    public ConvertToGbl(string inpath)
    {
        this.inpath = inpath;
    }
    public void Convert(string outpath)
    {
        var model = ModelRoot.Load(inpath);
        model.SaveGLB(outpath);
    }
}