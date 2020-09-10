namespace EpsilonEngine
{
    public class GameManager
    {
        public string name;
        protected GameManager() { }
        public static GameManager Create()
        {
            GameManager output = new GameManager();
            return output;
        }
        public virtual void Initialize()
        {

        }
        public virtual void Update(UpdatePacket packet)
        {

        }
    }
}