namespace DontMelt
{
    public class GhostBlock : Component
    {
        private float Timer = 0;
        private const float BreakTime = 0.1f;
        private const float RegenerateTime = 2;

        private SpriteRenderer SR;
        private RG_Collider RGC;

        public GameObject Particle;

        private void Start()
        {
            SR = GetComponent<SpriteRenderer>();
            RGC = GetComponent<RG_Collider>();
        }

        void Update()
        {
            if (Stage_Player.Instance == null)
            {
                Timer = 0;
            }
            else if (!Stage_Player.Instance.Paused)
            {
                if (Timer <= 0)
                {
                    RGC.enabled = true;
                    SR.color = GUIHelper.White;
                    foreach (RG_Collision collision in RGC.Get_Collisions())
                    {
                        if (collision.Side.Top && collision.Other_GameObject.tag == "Player")
                        {
                            Timer = RegenerateTime + BreakTime;
                            Instantiate(Particle, transform.position, Quaternion.identity);
                            break;
                        }
                    }
                }
                else if (Timer >= RegenerateTime)
                {
                    Timer -= Time.deltaTime;
                    RGC.enabled = true;
                    SR.color = GUIHelper.White;
                }
                else if (Timer > 0 && Timer < RegenerateTime)
                {
                    Timer -= Time.deltaTime;
                    RGC.enabled = false;
                    SR.color = GUIHelper.Translucent;
                }
            }
        }
    }
}