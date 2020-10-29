namespace DontMelt
{
    public class Spring : Component
    {
        public Sprite Squashed;
        private Sprite Normal;
        private SpriteRenderer sr;
        private RG_Collider RGC;
        private float timer;
        private const float BounceForce = 11f;

        private void Start()
        {
            RGC = GetComponent<RG_Collider>();
            sr = GetComponent<SpriteRenderer>();
            Normal = sr.sprite;
        }

        private void Update()
        {
            if (Stage_Player.Instance == null)
            {
                sr.sprite = Normal;
            }
            else
            {
                if (timer > 0)
                {
                    sr.sprite = Squashed;
                }
                else
                {
                    sr.sprite = Normal;
                }

                if (!Stage_Player.Instance.Paused)
                {
                    timer -= Time.deltaTime;
                    foreach (RG_Trigger_Overlap overlap in RGC.Get_Trigger_Overlaps())
                    {
                        if (overlap.Other_GameObject.tag == "Player")
                        {
                            Player.Instance.RGRB.Velocity = new Vector2(Player.Instance.RGRB.Velocity.x, BounceForce);
                            timer = 1;
                        }
                    }
                }
            }
        }
    }
}