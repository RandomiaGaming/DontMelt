namespace EpsilonEngine
{
    public sealed class Overlap
    {
        public Collider otherCollider = null;
        public GameObject otherGameObject = null;
        public Collider thisCollider = null;
        public GameObject thisGameObject = null;
        private Overlap() { }
        public static Overlap Create()
        {
            Overlap output = new Overlap();
            output.otherCollider = null;
            output.otherGameObject = null;
            output.thisCollider = null;
            output.thisGameObject = null;
            return output;
        }
        public static Overlap Create(Collider thisCollider, GameObject thisGameObject, Collider otherCollider, GameObject otherGameObject)
        {
            Overlap output = new Overlap();
            output.otherCollider = otherCollider;
            output.otherGameObject = otherGameObject;
            output.thisCollider = thisCollider;
            output.thisGameObject = thisGameObject;
            return output;
        }
        public Overlap Clone()
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