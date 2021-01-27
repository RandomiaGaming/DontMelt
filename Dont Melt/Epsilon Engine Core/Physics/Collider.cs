using System.Collections.Generic;
namespace EpsilonEngine
{
    public class Collider : Component
    {
        public List<Collision> collisions = new List<Collision>();
        public List<Overlap> overlaps = new List<Overlap>();

        public Rectangle shape = new Rectangle(Point.Zero, new Point(16, 16));
        public Point offset = Point.Zero;
        public SideInfo sideCollision = SideInfo.True;
        public bool trigger = false;

        public static List<Collider> loadedColliders = new List<Collider>();

        public Collider(GameObject pixel2DGameObject) : base(pixel2DGameObject)
        {

        }
        public void Flush()
        {
            collisions = new List<Collision>();
            overlaps = new List<Overlap>();
        }
        public Rectangle GetWorldShape()
        {
            Rectangle output = new Rectangle(shape.min + gameObject.position + offset, shape.max + gameObject.position + offset);
            return output;
        }
        public override void Update()
        {
            Flush();
        }
        public override void Initialize()
        {
            loadedColliders.Add(this);
        }
        public virtual void LogOverlap(Collider otherCollider)
        {
            overlaps.Add(new Overlap(this, otherCollider));
        }
        public void LogCollision(Collider otherCollider, SideInfo sideInfo)
        {
            collisions.Add(new Collision(this, otherCollider, sideInfo));
        }
    }
}