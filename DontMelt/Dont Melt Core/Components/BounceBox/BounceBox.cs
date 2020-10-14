namespace DontMelt
{
    public class BounceBox : Component
    {
        private RG_Collider RGC;
        private const float BounceForce = 10f;

        private void Start()
        {
            RGC = GetComponent<RG_Collider>();
        }

        private void Update()
        {
            if (Stage_Player.Instance != null && !Stage_Player.Instance.Paused)
            {
                foreach (RG_Collision collision in RGC.Get_Collisions())
                {
                    if (collision.Other_Collider.gameObject.tag == "Player")
                    {
                        if (collision.Side.Top)
                        {
                            Player.Instance.RGRB.Velocity = new Vector2(Player.Instance.RGRB.Velocity.x, BounceForce);
                        }

                        if (collision.Side.Bottom)
                        {
                            Player.Instance.RGRB.Velocity = new Vector2(Player.Instance.RGRB.Velocity.x, -BounceForce);
                        }

                        if (collision.Side.Right)
                        {
                            Player.Instance.RGRB.Velocity = new Vector2(BounceForce, Player.Instance.RGRB.Velocity.y);
                        }

                        if (collision.Side.Left)
                        {
                            Player.Instance.RGRB.Velocity = new Vector2(-BounceForce, Player.Instance.RGRB.Velocity.y);
                        }

                        return;
                    }
                }
            }
        }
    }
}