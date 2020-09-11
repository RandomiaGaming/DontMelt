using EpsilonEngine;
namespace DontMelt
{
    public enum DeathState
    {
        Dead,
        Alive,
        Animating
    }
    public class Player : Component
    {
        public Rigidbody rigidbody;
        public Collider collider;
        private DeathState CurrentDeathState = DeathState.Alive;
        private SideInfo touchingGround = SideInfo.Create(false);

        private const float MoveForce = 20;
        private const float MaxMoveSpeed = 6.5f;
        private const float JumpForce = 8.5f;
        private const float WalljumpForceX = 8.5f;
        private const float WalljumpForceY = 6;
        private const float DragForce = 8;
        public override void Initialize()
        {
            rigidbody = (Rigidbody)parent.GetComponent(typeof(Rigidbody));
            collider = (Collider)parent.GetComponent(typeof(Collider));
        }
        public override void Update(UpdatePacket packet)
        {
            Move(packet);
            Jump(packet);
            Drag(packet);
            Collision(packet);
        }
        private void Jump(UpdatePacket packet)
        {
            if (TouchingGround.y < 0 && Input_Manager.Jump_Down())
            {
                RGRB.Velocity.y = JumpForce;
            }
            else if (Input_Manager.Jump_Down() && TouchingGround.x < 0)
            {
                RGRB.Velocity = new Vector2(WalljumpForceX, WalljumpForceY);
            }
            else if (Input_Manager.Jump_Down() && TouchingGround.x > 0)
            {
                RGRB.Velocity = new Vector2(-WalljumpForceX, WalljumpForceY);
            }
        }

        private void Move(UpdatePacket packet)
        {
            if (Input_Manager.Move_Axis() != 0)
            {
                transform.localScale = new Vector3(Input_Manager.Move_Axis(), 1, 1);
            }

            if ((RGRB.Velocity.x < MaxMoveSpeed && Input_Manager.Move_Axis() == 1) ||
                (RGRB.Velocity.x > -MaxMoveSpeed && Input_Manager.Move_Axis() == -1))
            {
                RGRB.Velocity += new Vector2(MoveForce * Input_Manager.Move_Axis() * Time.deltaTime, 0);
            }
        }

        private void Drag(UpdatePacket packet)
        {
            float Sign = Mathf.Sign(RGRB.Velocity.x);
            RGRB.Velocity -= new Vector2(DragForce * Sign * Time.deltaTime, 0);
            if (Mathf.Sign(RGRB.Velocity.x) != Sign)
            {
                RGRB.Velocity = new Vector2(0, RGRB.Velocity.y);
            }
        }

        private void Collision(UpdatePacket packet)
        {
            TouchingGround = Vector2Int.zero;
            foreach (RG_Collision collision in MainCollider.Get_Collisions())
            {
                if (collision.Other_GameObject.tag == "Hazzard")
                {
                    KillPlayer();
                }

                if (collision.Other_Collider.gameObject.tag == "Ground" && !collision.Other_Collider.Is_Trigger)
                {
                    if (collision.Side.Bottom)
                    {
                        TouchingGround.y = -1;
                    }
                    else if (collision.Side.Left)
                    {
                        TouchingGround.x = -1;
                    }
                    else if (collision.Side.Right)
                    {
                        TouchingGround.x = 1;
                    }
                }

                if (collision.Side.Top && collision.Side.Bottom && collision.Side.Left &&
                    collision.Side.Right && collision.Other_Collider.Is_Trigger == false)
                {
                    KillPlayer();
                }

                foreach (RG_Trigger_Overlap overlap in MainCollider.Get_Trigger_Overlaps())
                {
                    if (overlap.Other_GameObject.tag == "Hazzard")
                    {
                        KillPlayer();
                    }
                }
            }
        }
    }
}