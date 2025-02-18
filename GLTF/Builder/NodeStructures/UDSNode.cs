using FuturamaLib.NIF.Structures;
using FuturamaLib.GLTF.Init;
using FuturamaLib.GLTF.Calculs;

namespace FuturamaLib.GLTF.Builder.NodeStructures
{
    public class UDSNode : Node
    {
        public string fileName{get;set;}
        public override List<INode> Children {get; set;} = new List<INode>();
        public UDSNode(ref Gltf gltf, NiUDSNode niUDSNode, string fileName) : base(ref gltf, niUDSNode)
        {
            this.fileName = fileName; 
            Name = niUDSNode.Name.Value.Split("|")[0];
            foreach (var instance in gltf.structure.instList)
                if(instance.Name == Name)
                    Children.Add(new BuildInstance(ref gltf, instance));
        }
    }
}
            //TODO if name is Parcel1, the models in PE will not load (special cases)
            //Parcel1|0023::HandbookParcel => PickupDef::Book
            //Parcel1|0024::KeycardParcel => PickupDef::KeyCard
            //Parcel1|005d::Keycard2Parcel => PickupDef::KeyCard
            //TODO NiUDSNode has NiStringExtraData that tells game content of destructible ex:
            //WoodenBox1_sewer|00XX::   NiStringExtraData {mode_all;Pickup::FryCash();Ammo::ammo1()}