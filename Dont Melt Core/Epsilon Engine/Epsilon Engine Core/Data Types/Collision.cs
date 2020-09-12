namespace EpsilonEngine
{
    public sealed class Collision
    {
        public Collider otherCollider = null;
        public GameObject otherGameObject = null;
        public Collider thisCollider = null;
        public GameObject thisGameObject = null;
        public SideInfo sideInfo = null;
        private Collision() { }
        public static Collision Create()
        {
            Collision output = new Collision();
            output.otherCollider = null;
            output.otherGameObject = null;
            output.thisCollider = null;
            output.thisGameObject = null;
            output.sideInfo = SideInfo.Create();
            return output;
        }
        public static Collision Create(Collider thisCollider, GameObject thisGameObject, Collider otherCollider, GameObject otherGameObject, SideInfo sideInfo)
        {
            Collision output = new Collision();
            output.otherCollider = otherCollider;
            output.otherGameObject = otherGameObject;
            output.thisCollider = thisCollider;
            output.thisGameObject = thisGameObject;
            output.sideInfo = sideInfo;
            return output;
        }
        public Collision Clone()
        {
            Collision output = new Collision();
            output.otherCollider = otherCollider;
            output.otherGameObject = otherGameObject;
            output.thisCollider = thisCollider;
            output.thisGameObject = thisGameObject;
            output.sideInfo = sideInfo.Clone();
            return output;
        }
    }
}
