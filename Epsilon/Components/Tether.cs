namespace DontMelt
{
    public class Tether : Component
    {
        private SpriteRenderer sr;
        public Sprite Glowing;
        private Sprite Normal;
        private bool CanTether = true;

        private const float TetherRange = 4;
        private const float TetherForce = 11.5f;

        void Start()
        {
            sr = GetComponent<SpriteRenderer>();
            Normal = sr.sprite;
        }

        void Update()
        {
            if (Stage_Player.Instance == null)
            {
                sr.sprite = Normal;
            }
            else
            {
                Vector2 direction = transform.position - Player.Instance.gameObject.transform.position;
                direction = new Vector2(Mathf.Sign(direction.x), Mathf.Sign(direction.y));
                transform.localScale = direction;
                if (Vector3.Distance(transform.position, Player.Instance.gameObject.transform.position) < 4 &&
                    Input_Manager.Jump_Down() && CanTether)
                {
                    CanTether = false;
                    Player.Instance.gameObject.gameObject.GetComponent<RG_Rigidbody>().Velocity =
                        transform.localScale * TetherForce;
                }

                if (Vector3.Distance(transform.position, Player.Instance.gameObject.transform.position) > TetherRange &&
                    !CanTether)
                {
                    CanTether = true;
                }

                if (CanTether && Vector3.Distance(transform.position, Player.Instance.gameObject.transform.position) <
                    TetherRange)
                {
                    sr.sprite = Glowing;
                }
                else
                {
                    sr.sprite = Normal;
                }
            }
        }
    }
}