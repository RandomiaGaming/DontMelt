namespace DontMelt
{
    public abstract class Component
    {
        private Component() { }
        public virtual void Update(InputPacket Packet) { }
        public virtual void Initialize() { }
    }
}