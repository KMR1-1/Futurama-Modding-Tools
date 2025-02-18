using System.IO;

namespace FuturamaLib.NIF.Structures
{
    public class NiNode : NiAVObject
    {
        public NiRef<NiAVObject>[] Children;

        public NiRef<NiDynamicEffect>[] Effects;

        public NiNode(NIFReader file, BinaryReader reader) : base(file, reader)
        {
            Children = new NiRef<NiAVObject>[reader.ReadUInt32()];
            for (var i = 0; i < Children.Length; i++)
                Children[i] = new NiRef<NiAVObject>(reader.ReadUInt32());

            Effects = new NiRef<NiDynamicEffect>[reader.ReadUInt32()];
            for (var i = 0; i < Effects.Length; i++)
                Effects[i] = new NiRef<NiDynamicEffect>(reader.ReadUInt32());
        }
        public override Dictionary<string, object> GetDetails()
        {
            var details = base.GetDetails();
            var childrens = new Dictionary<string, object>();
            var effects = new Dictionary<string, object>();

            foreach(var child in Children)
            {
                childrens[$"{child.RefId}"] = child.IsValid ? child.Object.GetType().Name :"";
            }

            foreach (var effect in Effects)
            {
                effects[$"{effect.RefId}"] = effect.IsValid ? effect.Object.GetType().Name :"";
            }
            
            details["Effects"] = effects;
            details["Children"] = childrens;
            return details;
        }
    }
}