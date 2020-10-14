namespace DontMelt
{
    public class OnOffTrack : Component
    {
        public bool IsOnTrack = false;
        private const float TrackSpeed = 20;
        private const float MaxSpeed = 10;
        private RG_Collider RGC;
        private int Direction = 1;
        private SpriteRenderer SR;

        public Sprite OnSprite1;
        public Sprite OnSprite2;
        public Sprite OffSprite1;
        public Sprite OffSprite2;
        private float Timer = 0;

        // Start is called before the first frame update
        void Start()
        {
            RGC = GetComponent<RG_Collider>();
            SR = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimation();
            if (Stage_Player.Instance != null)
            {
                if (Stage_Player.Instance.OnOffIsOn == IsOnTrack)
                {
                    Direction = 1;
                }
                else
                {
                    Direction = -1;
                }

                if (!Stage_Player.Instance.Paused)
                {
                    foreach (RG_Collision collision in RGC.Get_Collisions())
                    {
                        if (collision.Other_GameObject.tag == "Player")
                        {
                            RG_Rigidbody PlayerRGRB = collision.Other_GameObject.GetComponent<RG_Rigidbody>();
                            if (collision.Side.Top)
                            {
                                if ((Direction == 1 && PlayerRGRB.Velocity.x < MaxSpeed) ||
                                    (Direction == -1 && PlayerRGRB.Velocity.x > -MaxSpeed))
                                {
                                    PlayerRGRB.Velocity.x += Direction * TrackSpeed * Time.deltaTime;
                                }
                            }
                            else if (collision.Side.Bottom)
                            {
                                if ((Direction == 1 && PlayerRGRB.Velocity.x > -MaxSpeed) ||
                                    (Direction == -1 && PlayerRGRB.Velocity.x < MaxSpeed))
                                {
                                    PlayerRGRB.Velocity.x += -1 * Direction * TrackSpeed * Time.deltaTime;
                                }
                            }
                            else if (collision.Side.Left)
                            {
                                if ((Direction == 1 && PlayerRGRB.Velocity.y < MaxSpeed) ||
                                    (Direction == -1 && PlayerRGRB.Velocity.y > -MaxSpeed))
                                {
                                    PlayerRGRB.Velocity.y += Direction * TrackSpeed * Time.deltaTime;
                                }
                            }
                            else
                            {
                                if ((Direction == 1 && PlayerRGRB.Velocity.y > -MaxSpeed) ||
                                    (Direction == -1 && PlayerRGRB.Velocity.y < MaxSpeed))
                                {
                                    PlayerRGRB.Velocity.y += -1 * Direction * TrackSpeed * Time.deltaTime;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void UpdateAnimation()
        {
            Timer += Time.deltaTime;
            if (Timer >= 0.2f)
            {
                Timer = 0;
            }

            if (Stage_Player.Instance != null)
            {
                if (IsOnTrack == Stage_Player.Instance.OnOffIsOn)
                {
                    if (Timer >= 0 && Timer < 0.1f)
                    {
                        SR.sprite = OnSprite1;
                    }
                    else
                    {
                        SR.sprite = OnSprite2;
                    }
                }
                else
                {
                    if (Timer >= 0 && Timer < 0.1f)
                    {
                        SR.sprite = OffSprite1;
                    }
                    else
                    {
                        SR.sprite = OffSprite2;
                    }
                }
            }
            else
            {
                if (IsOnTrack)
                {
                    if (Timer >= 0 && Timer < 0.1f)
                    {
                        SR.sprite = OnSprite1;
                    }
                    else
                    {
                        SR.sprite = OnSprite2;
                    }
                }
                else
                {
                    if (Timer >= 0 && Timer < 0.1f)
                    {
                        SR.sprite = OffSprite1;
                    }
                    else
                    {
                        SR.sprite = OffSprite2;
                    }
                }
            }
        }
    }
}