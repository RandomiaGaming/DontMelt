namespace EpsilonEngine
{
    public sealed class Overlap
    {
        public Collider otherCollider { get; private set; } = null;
        public GameObject otherGameObject { get; private set; } = null;
        public Collider thisCollider { get; private set; } = null;
        public GameObject thisGameObject { get; private set; } = null;
        private Overlap() { }
        public static Overlap Create(Collider thisCollider, GameObject thisGameObject, Collider otherCollider, GameObject otherGameObject)
        {
            Overlap output = new Overlap();
            output.otherCollider = otherCollider;
            output.otherGameObject = otherGameObject;
            output.thisCollider = thisCollider;
            output.thisGameObject = thisGameObject;
            return output;
        }
    }
}