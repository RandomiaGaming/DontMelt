using System.Collections.Generic;

namespace DontMelt
{
    public sealed class GameObject
    {
        public Point position = Point.Create(0, 0);
        public Texture sprite = Texture.Create();
        private List<Component> components = new List<Component>();
        public List<Component> GetComponentList()
        {
            return new List<Component>(components);
        }
        public List<T> GetComponents<T>() where T : Component, new()
        {
            List<T> Output = new List<T>();
            foreach (Component C in components)
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
            if (Index < 0 || Index >= components.Count)
            {
                return null;
            }
            else
            {
                return components[Index];
            }
        }
        public T GetComponent<T>() where T : Component, new()
        {
            foreach (Component C in components)
            {
                if (C.GetType() == typeof(T))
                {
                    return (T)C;
                }
            }
            return null;
        }
        public int GetComponentCount()
        {
            return components.Count;
        }
        public void AddComponent<T>() where T : Component, new()
        {
            components.Add(new T());
        }
        public void AddComponent(Component New_Component)
        {
            components.Add(New_Component);
        }
        public void Initialize()
        {
            foreach (Component C in components)
            {
                C.Initialize();
            }
        }
        internal void Update(UpdatePacket Packet)
        {
            foreach (Component C in components)
            {
                C.Update(Packet.Clone());
            }
        }
        private GameObject() { }
        public static GameObject Create()
        {
            GameObject Output = new GameObject();
            Output.sprite = Texture.Create();
            return Output;
        }
    }
}