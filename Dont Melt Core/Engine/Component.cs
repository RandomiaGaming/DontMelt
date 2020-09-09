namespace DontMelt
{
    public abstract class Component
    {
        private Component() { }
        public virtual void Update(UpdatePacket Packet) { }
        public virtual void Initialize() { }
    }
}