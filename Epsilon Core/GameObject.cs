using System.Collections.Generic;
namespace Epsilon
{
    public abstract class GameObject
    {
        public Point position = Point.Zero;
        public Texture texture = new Texture();
        public List<Rectangle> colliderShape = new List<Rectangle>();
        public Vector velocity = Vector.Zero;
        public abstract void Initialize();
        public abstract void Update();
        public void PhysicsUpdate()
        {

        }
    }
}