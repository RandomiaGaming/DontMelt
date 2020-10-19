using System.Collections.Generic;
namespace EpsilonEngine
{
    public sealed class Collider : Component
    {
        public static Collider[] loadedColliders { get; private set; } = new Collider[0];
        private Collision[] _collisions = new Collision[0];
        public Collision[] collisions { get { return new List<Collision>(_collisions).ToArray(); } private set { _collisions = value; } }
        private Overlap[] _overlaps = new Overlap[0];
        public Overlap[] overlaps { get { return new List<Overlap>(_overlaps).ToArray(); } private set { _overlaps = value; } }

        public Rectangle shape = new Rectangle(Point.Zero, Point.One);
        public Point offset = Point.Zero;
        public SideInfo sideCollision = SideInfo.True;
        public bool trigger = false;

        public override UpdateOutputPacket Update(UpdateInputPacket packet)
        {
            Flush();
            return new UpdateOutputPacket();
        }
        public void LogCollision(Collider otherCollider, SideInfo sideInfo)
        {
            Collision newCollision = Collision.Create(this, parent, otherCollider, otherCollider.parent, sideInfo);
            List<Collision> temp = new List<Collision>(collisions);
            temp.Add(newCollision);
            collisions = temp.ToArray();
        }
        public void LogOverlap(Collider otherCollider)
        {
            Overlap newOverlap = Overlap.Create(this, parent, otherCollider, otherCollider.parent);
            List<Overlap> temp = new List<Overlap>(overlaps);
            temp.Add(newOverlap);
            overlaps = temp.ToArray();
        }
        public void Flush()
        {
            collisions = new Collision[0];
            overlaps = new Overlap[0];
        }
        private Collider()
        {
            ID = nextFreeID;
            nextFreeID++;
            List<Collider> temp = new List<Collider>(loadedColliders);
            temp.Add(this);
            loadedColliders = temp.ToArray();
        }
        public static new Component Create(GameObject parent)
        {
            Collider output = new Collider();
            output.offset = Point.Zero;
            output.trigger = false;
            output.shape = new Rectangle(Point.Zero, Point.Zero);
            output.collisions = new Collision[0];
            output.overlaps = new Overlap[0];
            output.sideCollision = SideInfo.True;
            output.parent = parent;
            return output;
        }
        public static Component Create(GameObject parent, Rectangle shape)
        {
            Collider output = new Collider();
            output.offset = Point.Zero;
            output.trigger = false;
            output.shape = shape;
            output.collisions = new Collision[0];
            output.overlaps = new Overlap[0];
            output.sideCollision = SideInfo.True;
            output.parent = parent;
            return output;
        }
        public static new Collider Create()
        {
            Collider output = new Collider();
            output.offset = Point.Zero;
            output.trigger = false;
            output.shape = new Rectangle(Point.Zero, Point.Zero);
            output.collisions = new Collision[0];
            output.overlaps = new Overlap[0];
            output.sideCollision = SideInfo.True;
            output.parent = null;
            return output;
        }
        public Rectangle GetWorldShape()
        {
            Rectangle output = new Rectangle(shape.min + parent.position + offset, shape.max + parent.position + offset);
            return output;
        }
    }
}