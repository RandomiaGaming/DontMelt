namespace EpsilonEngine
{
    public class Component
    {
        public static int nextFreeID { set; protected get; } = 0;
        public int ID { get; protected set; } = 0;
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != typeof(Component))
            {
                return false;
            }
            else
            {
                return this == (Component)obj;
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static bool operator ==(Component a, Component b)
        {
            if (a is null && b is null)
            {
                return true;
            }
            else if (a is null || b is null)
            {
                return false;
            }
            return a.ID == b.ID;
        }
        public static bool operator !=(Component a, Component b)
        {
            return !(a == b);
        }
        protected Component()
        {
            ID = nextFreeID;
            nextFreeID++;
        }
        private GameObject _parent = null;
        public GameObject parent
        {
            get { return _parent; }
            set
            {
                if (_parent != value)
                {
                    if (_parent != null)
                    {
                        _parent.RemoveComponent(GetType());
                    }
                    value.RemoveComponent(GetType());
                    _parent = value;
                }
            }
        }
        public virtual void Update(UpdatePacket packet)
        {

        }
        public virtual void Initialize()
        {

        }
        public static Component Create(GameObject parent)
        {
            Component output = new Component();
            output._parent = parent;
            return output;
        }
        public static Component Create()
        {
            Component output = new Component();
            output._parent = null;
            return output;
        }
    }
}