namespace EpsilonEngine
{
    public sealed class Collision
    {
        public Collider otherCollider { get; private set; } = null;
        public GameObject otherGameObject { get; private set; } = null;
        public Collider thisCollider { get; private set; } = null;
        public GameObject thisGameObject { get; private set; } = null;
        public SideInfo sideInfo { get; private set; }
        private Collision() { }
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
    }
}
