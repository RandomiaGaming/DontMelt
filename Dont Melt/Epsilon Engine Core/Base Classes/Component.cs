using System;
namespace EpsilonEngine
{
    public abstract class Component
    {
        public readonly Game game = null;
        public readonly Scene scene = null;
        public readonly GameObject gameObject = null;
        public Component(GameObject gameObject)
        {
            if (gameObject is null)
            {
                throw new NullReferenceException();
            }
            this.gameObject = gameObject;
            if (gameObject.scene is null)
            {
                throw new NullReferenceException();
            }
            scene = gameObject.scene;
            if (gameObject.game is null)
            {
                throw new NullReferenceException();
            }
            game = gameObject.game;

            gameObject.AddComponent(this);
        }
        public virtual void Initialize()
        {

        }
        public virtual void Update()
        {

        }
        public virtual void Cleanup()
        {

        }
    }
}