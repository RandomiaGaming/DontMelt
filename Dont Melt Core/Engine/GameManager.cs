namespace DontMelt
{
    public abstract class GameManager
    {
        public GameManager() { }
        public virtual void Initialize() { }
        public virtual void Update(double DeltaTime) { }
    }
}