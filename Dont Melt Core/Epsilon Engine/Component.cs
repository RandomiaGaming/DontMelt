using System;

namespace EpsilonEngine
{
    public class Component
    {
        private static int[] takenIDs = new int[0];
        private int ID = 0;
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null) || obj.GetType() != typeof(Component))
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
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return true;
            }
            else if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
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
            bool TryAgain = true;
            while (TryAgain)
            {
                TryAgain = false;
                ID = RandomnessHelper.Next(0, int.MaxValue);
                for (int i = 0; i < takenIDs.Length; i++)
                {
                    if (takenIDs[i] == ID)
                    {
                        TryAgain = true;
                        break;
                    }
                }
            }
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