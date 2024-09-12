using System.Diagnostics;
using System.Runtime.Serialization;


namespace LinscEditor.Components
{
    [DataContract]
    internal abstract class Component : ViewModelBase
    {
        [DataMember]
        public GameEntity Owner { get; private set; }

        public Component(GameEntity owner)
        {
            Debug.Assert(owner != null);
            Owner = owner;
        }
    }
}