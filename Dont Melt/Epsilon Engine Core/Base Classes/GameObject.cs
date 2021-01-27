using System;
using System.Collections.Generic;
namespace EpsilonEngine
{
    public class GameObject
    {
        public Point position = Point.Zero;
        public Texture texture = null;
        public string name = "Unnamed GameObject";
        public List<string> tags = new List<string>();

        protected List<Component> components = new List<Component>();
        protected List<int> componentsToRemove = new List<int>();
        protected List<Component> componentsToAdd = new List<Component>();

        public readonly Game game = null;
        public readonly Scene scene = null;
        public GameObject(Scene scene)
        {
            if (scene is null)
            {
                throw new NullReferenceException();
            }
            this.scene = scene;
            if (scene.game is null)
            {
                throw new NullReferenceException();
            }
            game = scene.game;
            scene.InstantiateGameObject(this);
        }
        public virtual void Initialize()
        {

        }
        public virtual void Update()
        {
            foreach (Component c in components)
            {
                c.Update();
            }
        }
        public virtual void OnRemove()
        {

        }
        public virtual void Cleanup()
        {
            if (componentsToRemove is null)
            {
                componentsToRemove = new List<int>();
            }
            componentsToRemove.Sort();
            foreach (int componentID in componentsToRemove)
            {
                components.RemoveAt(componentID);
            }
            componentsToRemove = new List<int>();

            foreach (Component componentToAdd in componentsToAdd)
            {
                components.Add(componentToAdd);
            }
            foreach (Component componentToAdd in componentsToAdd)
            {
                componentToAdd.Initialize();
            }
            componentsToAdd = new List<Component>();
        }
        #region Component Management Methods
        public virtual Component GetComponent(int index)
        {
            if (components is null)
            {
                components = new List<Component>();
                return null;
            }
            if (index < 0 || index >= components.Count)
            {
                throw new ArgumentException();
            }
            return components[index];
        }
        public virtual Component GetComponent(Type targetType)
        {
            if (components is null)
            {
                components = new List<Component>();
                return null;
            }
            if (targetType is null)
            {
                throw new NullReferenceException();
            }
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].GetType().IsAssignableFrom(targetType))
                {
                    return components[i];
                }
            }
            return null;
        }
        public virtual T GetComponent<T>() where T : Component
        {
            if (components is null)
            {
                components = new List<Component>();
                return null;
            }
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)components[i];
                }
            }
            return null;
        }
        public virtual List<Component> GetComponents()
        {
            return new List<Component>(components);
        }
        public virtual List<Component> GetComponents(Type targetType)
        {
            if (components is null)
            {
                components = new List<Component>();
                return null;
            }
            if (targetType is null)
            {
                throw new NullReferenceException();
            }
            List<Component> output = new List<Component>();
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].GetType().IsAssignableFrom(targetType))
                {
                    output.Add(components[i]);
                }
            }
            return output;
        }
        public virtual List<T> GetComponents<T>() where T : Component
        {
            if (components is null)
            {
                components = new List<Component>();
                return null;
            }
            List<T> output = new List<T>();
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].GetType().IsAssignableFrom(typeof(T)))
                {
                    output.Add((T)components[i]);
                }
            }
            return output;
        }
        public virtual int GetComponentCount()
        {
            if (components is null)
            {
                components = new List<Component>();
                return 0;
            }
            return components.Count;
        }
        public virtual void RemoveComponent(int index)
        {
            if (components is null)
            {
                components = new List<Component>();
                return;
            }
            if (index < 0 || index >= components.Count)
            {
                throw new ArgumentException();
            }
            if (componentsToRemove is null)
            {
                componentsToRemove = new List<int>();
            }
            componentsToRemove.Add(index);
        }
        public virtual void RemoveComponent(Component targetComponent)
        {
            if (components is null)
            {
                components = new List<Component>();
                return;
            }
            if (targetComponent is null)
            {
                throw new NullReferenceException();
            }
            if (componentsToRemove is null)
            {
                componentsToRemove = new List<int>();
            }
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i] == targetComponent)
                {
                    componentsToRemove.Add(i);
                }
            }
        }
        public virtual void RemoveComponents(Type targetType)
        {
            if (components is null)
            {
                components = new List<Component>();
                return;
            }
            if (targetType is null)
            {
                throw new NullReferenceException();
            }
            if (componentsToRemove is null)
            {
                componentsToRemove = new List<int>();
            }
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].GetType().IsAssignableFrom(targetType))
                {
                    componentsToRemove.Add(i);
                }
            }
        }
        public virtual void RemoveComponents<T>() where T : Component
        {
            if (components is null)
            {
                components = new List<Component>();
                return;
            }
            if (componentsToRemove is null)
            {
                componentsToRemove = new List<int>();
            }
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].GetType().IsAssignableFrom(typeof(T)))
                {
                    componentsToRemove.Add(i);
                }
            }
        }
        public virtual void AddComponent(Component newComponent)
        {
            if (componentsToAdd is null)
            {
                componentsToAdd = new List<Component>();
            }
            if (newComponent is null)
            {
                throw new NullReferenceException();
            }
            componentsToAdd.Add(newComponent);
        }
        #endregion
    }
}
