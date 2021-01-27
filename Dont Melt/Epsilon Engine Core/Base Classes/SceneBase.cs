using System;
namespace EpsilonEngine
{
    public abstract class SceneBase
    {
        public readonly Game game = null;
        public SceneBase(Game game)
        {
            if (game is null)
            {
                throw new NullReferenceException();
            }
            this.game = game;
            game.AddScene(this);
        }
        public virtual void Initialize()
        {

        }
        public abstract Texture Render();
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
