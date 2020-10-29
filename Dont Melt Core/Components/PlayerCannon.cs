namespace DontMelt
{
    public class PlayerCannon : Component
    {
        private float Timer = 0;
        private int PlayerCollisionTime;
        private RG_Collider RGC;
        private const float LaunchForce = 12.5f;

        private void Start()
        {
            RGC = GetComponent<RG_Collider>();
        }

        private void Update()
        {
            if (Stage_Player.Instance == null)
            {
                transform.localScale = Vector3.one;
            }
            else if (!Stage_Player.Instance.Paused)
            {
                PlayerCollisionTime--;
                Timer += Time.deltaTime;
                if (Timer >= 1)
                {
                    if ((Vector2)transform.localScale == new Vector2(1, 1))
                    {
                        transform.localScale = new Vector2(1, -1);
                    }
                    else if ((Vector2)transform.localScale == new Vector2(1, -1))
                    {
                        transform.localScale = new Vector2(-1, -1);
                    }
                    else if ((Vector2)transform.localScale == new Vector2(-1, -1))
                    {
                        transform.localScale = new Vector2(-1, 1);
                    }
                    else
                    {
                        transform.localScale = new Vector2(1, 1);
                    }

                    Timer = 0;
                }

                foreach (RG_Trigger_Overlap overlap in RGC.Get_Trigger_Overlaps())
                {
                    if (overlap.Other_GameObject.tag == "Player")
                    {
                        if (PlayerCollisionTime < 0)
                        {
                            Player.Instance.CurrentCannon = transform;
                        }

                        PlayerCollisionTime = 5;
                    }
                }

                if (Player.Instance.CurrentCannon == transform && Input_Manager.Jump_Down())
                {
                    PlayerCollisionTime = 5;
                    Player.Instance.CurrentCannon = null;
                    Player.Instance.RGRB.Velocity = transform.localScale * LaunchForce;
                }
            }
        }
    }
}