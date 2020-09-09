namespace DontMelt
{
    public abstract class Component
    {
        private Component() { }
        public virtual void Update(GameObject Parent_Game_Object, double Delta_Time) { }
        public virtual void Initialize(GameObject Parent_Game_Object) { }
    }
}