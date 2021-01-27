using System;
namespace EpsilonEngine
{
    public abstract class SceneManager
    {
        public readonly Game game = null;
        public readonly Scene scene = null;
        public SceneManager(Scene scene)
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
            scene.AddSceneManager(this);
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
        public virtual void OnRemove()
        {

        }
    }
}