using System;
using System.Collections.Generic;
namespace DontMelt
{
    public sealed class GameObject
    {
        public string name;
        public Point position = Point.Zero;
        public Texture graphic = Texture.Create(1, 1);
        private Component[] components = new Component[0];
        public List<Component> GetAllComponents()
        {
            return new List<Component>(components);
        }
        public Component GetComponent(int index)
        {
            if (index < 0 || index >= components.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            else
            {
                return components[index];
            }
        }
        public Component GetComponent(Type targetType)
        {
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i].GetType() == targetType)
                {
                    return components[i];
                }
            }
            return null;
        }
        public int GetComponentCount()
        {
            return components.Length;
        }
        public void AddComponent(Component newComponent)
        {
            if (GetComponent(newComponent.GetType()) != null)
            {
                throw new Exception("Duplicate Component Exception!");
            }
            newComponent.parent = this;
            List<Component> temp = new List<Component>(components);
            temp.Add(newComponent);
            components = temp.ToArray();
        }
        public void RemoveComponent(Type targetType)
        {
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i].GetType() == targetType)
                {
                    List<Component> temp = new List<Component>(components);
                    temp.RemoveAt(i);
                    components = temp.ToArray();
                    return;
                }
            }
        }
        public void RemoveComponent(int index)
        {
            List<Component> temp = new List<Component>(components);
            temp.RemoveAt(index);
            components = temp.ToArray();
        }
        public void Initialize(InitializationPacket packet)
        {
            foreach (Component c in components)
            {
                c.parent = this;
                c.Initialize(packet);
            }
        }
        public void Update(UpdatePacket packet)
        {
            foreach (Component c in components)
            {
                c.Update(packet);
            }
        }
        public static GameObject Create()
        {
            GameObject output = new GameObject();
            output.components = new Component[0];
            output.position = Point.Zero;
            return output;
        }
        public static GameObject Create(Component[] components)
        {
            GameObject output = new GameObject();
            output.components = new List<Component>(components).ToArray();
            for (int c = 0; c < components.Length; c++)
            {
                components[c].parent = output;
            }
            output.position = Point.Zero;
            output.graphic = Texture.Create(1, 1);
            output.name = "Unnamed GameObject";
            return output;
        }
        public static GameObject Create(Component[] components, Point position)
        {
            GameObject output = new GameObject();
            output.components = new List<Component>(components).ToArray();
            for (int c = 0; c < components.Length; c++)
            {
                components[c].parent = output;
            }
            output.position = position;
            output.graphic = Texture.Create(1, 1);
            output.name = "Unnamed GameObject";
            return output;
        }
        public static GameObject Create(Component[] components, Point position, Texture graphic)
        {
            GameObject output = new GameObject();
            output.components = new List<Component>(components).ToArray();
            for (int c = 0; c < components.Length; c++)
            {
                components[c].parent = output;
            }
            output.position = position;
            output.graphic = graphic;
            output.name = "Unnamed GameObject";
            return output;
        }
        public static GameObject Create(Component[] components, Point position, Texture graphic, string name)
        {
            GameObject output = new GameObject();
            output.components = new List<Component>(components).ToArray();
            for (int c = 0; c < components.Length; c++)
            {
                components[c].parent = output;
            }
            output.position = position;
            output.graphic = graphic;
            output.name = name;
            return output;
        }
        public static int nextFreeID = 0;
        private readonly int ID = 0;
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != typeof(GameObject))
            {
                return false;
            }
            else
            {
                return this == (GameObject)obj;
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static bool operator ==(GameObject a, GameObject b)
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
        public static bool operator !=(GameObject a, GameObject b)
        {
            return !(a == b);
        }
        private GameObject()
        {
            ID = nextFreeID;
            nextFreeID++;
        }
    }
}