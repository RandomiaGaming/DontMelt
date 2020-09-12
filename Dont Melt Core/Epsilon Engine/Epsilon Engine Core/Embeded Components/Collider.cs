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

        public Rectangle shape = Rectangle.Create(Point.Create(0, 0), Point.Create(EpsilonKernal.pixelsPerUnit, EpsilonKernal.pixelsPerUnit));
        public Point offset = Point.Create(0, 0);
        public SideInfo sideCollision = SideInfo.Create(true);
        public bool trigger = false;

        public override void Update(UpdatePacket packet)
        {
            Flush();
        }
        public void LogCollision(Collider otherCollider, SideInfo sideInfo)
        {
            Collision newCollision = Collision.Create();
            newCollision.otherGameObject = otherCollider.parent;
            newCollision.otherCollider = otherCollider;
            newCollision.thisCollider = this;
            newCollision.thisGameObject = parent;
            newCollision.sideInfo = sideInfo.Clone();
            List<Collision> temp = new List<Collision>(collisions);
            temp.Add(newCollision);
            collisions = temp.ToArray();
        }
        public void LogOverlap(Collider otherCollider)
        {
            Overlap newOverlap = Overlap.Create();
            newOverlap.otherGameObject = otherCollider.parent;
            newOverlap.otherCollider = otherCollider;
            newOverlap.thisCollider = this;
            newOverlap.thisGameObject = parent;
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
            output.offset = Point.Create(0, 0);
            output.trigger = false;
            output.shape = Rectangle.Create(Point.Create(0, 0), Point.Create(0, 0));
            output.collisions = new Collision[0];
            output.overlaps = new Overlap[0];
            output.sideCollision = SideInfo.Create(true);
            output.parent = parent;
            return output;
        }
        public static Component Create(GameObject parent, Rectangle shape)
        {
            Collider output = new Collider();
            output.offset = Point.Create(0, 0);
            output.trigger = false;
            output.shape = shape.Clone();
            output.collisions = new Collision[0];
            output.overlaps = new Overlap[0];
            output.sideCollision = SideInfo.Create(true);
            output.parent = parent;
            return output;
        }
        public static new Collider Create()
        {
            Collider output = new Collider();
            output.offset = Point.Create(0, 0);
            output.trigger = false;
            output.shape = Rectangle.Create(Point.Create(0, 0), Point.Create(0, 0));
            output.collisions = new Collision[0];
            output.overlaps = new Overlap[0];
            output.sideCollision = SideInfo.Create(true);
            output.parent = null;
            return output;
        }
        public Rectangle GetWorldShape()
        {
            Rectangle output = Rectangle.Create(shape.min + parent.position + offset, shape.max + parent.position + offset);
            return output;
        }
    }
}