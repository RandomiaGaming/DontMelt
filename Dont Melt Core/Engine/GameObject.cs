using System.Collections.Generic;

namespace DontMelt
{
    public sealed class GameObject
    {
        private List<Component> Components = new List<Component>();
        public List<Component> GetComponentList()
        {
            return new List<Component>(Components);
        }
        public List<T> GetComponents<T>() where T : Component, new()
        {
            List<T> Output = new List<T>();
            foreach (Component C in Components)
            {
                if (C.GetType() == typeof(T))
                {
                    Output.Add((T)C);
                }
            }
            return Output;
        }
        public Component GetComponent(int Index)
        {
            if (Index < 0 || Index >= Components.Count)
            {
                return null;
            }
            else
            {
                return Components[Index];
            }
        }
        public T GetComponent<T>() where T : Component, new()
        {
            foreach (Component C in Components)
            {
                if (C.GetType() == typeof(T))
                {
                    return (T)C;
                }
            }
            return null;
        }
        public int GetComponent_Count()
        {
            return Components.Count;
        }
        public void AddComponent<T>() where T : Component, new()
        {
            Components.Add(new T());
        }
        public void AddComponent(Component New_Component)
        {
            Components.Add(New_Component);
        }
        public void Initialize()
        {
            foreach (Component C in Components)
            {
                C.Initialize();
            }
        }
        internal void Update(InputPacket Packet)
        {
            foreach (Component C in Components)
            {
                C.Update(Packet.Clone());
            }
        }
        private GameObject() { }
        public static GameObject Create()
        {
            return new GameObject();
        }
    }
}