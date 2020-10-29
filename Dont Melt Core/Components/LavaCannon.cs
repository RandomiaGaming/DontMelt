
namespace DontMelt
{
    public class LavaCannon : Component
    {
        public GameObject Projectile;

        private float Timer;
        private const float TimeBTWShots = 3;

        // Update is called once per frame
        void Update()
        {
            if (Stage_Player.Instance != null && !Stage_Player.Instance.Paused)
            {
                Timer += Time.deltaTime;
                if (Timer > TimeBTWShots)
                {
                    Instantiate(Projectile, transform).transform.localScale = transform.localScale;
                    Timer = 0;
                }
            }
        }
    }
}