namespace DontMelt
{
    public class Spiny : Component
    {
        private RG_Rigidbody RGRB;
        public RG_Collider RGC;
        private const float MoveSpeed = 7.5f;
        private int Direction = -1;
        private ParticleSystem PS;
        private bool Dead = false;

        public GameObject DeathParticles;

        // Start is called before the first frame update
        void Start()
        {
            RGC = GetComponent<RG_Collider>();
            RGRB = GetComponent<RG_Rigidbody>();
            PS = GetComponent<ParticleSystem>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!Dead)
            {
                if (Stage_Player.Instance == null)
                {
                    RGRB.enabled = false;
                    transform.localScale = Vector3.one;
                }
                else if (Stage_Player.Instance.Paused)
                {
                    RGRB.enabled = false;
                }
                else
                {
                    RGRB.enabled = true;
                    foreach (RG_Collision collision in RGC.Get_Collisions())
                    {
                        if (!collision.Other_Collider.Is_Trigger)
                        {
                            if (collision.Side.Left)
                            {
                                Direction = 1;
                            }

                            if (collision.Side.Right)
                            {
                                Direction = -1;
                            }
                        }

                        if (collision.Side.Top == true && collision.Side.Bottom == true && collision.Side.Left == true &&
                            collision.Side.Right == true && collision.Other_Collider.Is_Trigger == false)
                        {
                            Instantiate(DeathParticles, transform.position, Quaternion.identity);
                            Destroy(gameObject);
                        }
                    }

                    RGRB.Velocity.x = MoveSpeed * Direction;
                    transform.localScale = new Vector3(Direction * -1, 1, 1);
                }
            }
        }
    }
}