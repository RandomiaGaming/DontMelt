using EpsilonEngine;
namespace DontMelt
{
    public class Player : Component
    {
        public Rigidbody rigidbody;
        public Collider collider;
        private SideInfo touchingGround = SideInfo.Create(false);

        private static float moveForce = 20;
        private static float jumpForce = 8.5f;
        private static float maxMoveSpeed = 6.5f;
        private static Vector wallJumpForce = Vector.Create(8.5, 6);
        private static float dragForce = 8;
        public override void Initialize()
        {
            rigidbody = (Rigidbody)parent.GetComponent(typeof(Rigidbody));
            collider = (Collider)parent.GetComponent(typeof(Collider));
        }
        public override void Update(UpdatePacket packet)
        {
            Collision(packet);
            Move(packet);
            Jump(packet);
            Drag(packet);
        }
        private void Jump(UpdatePacket packet)
        {
            if (packet.inputPacket.KeyDown(KeyCode.Space))
            {
                if (touchingGround.bottom)
                {
                    rigidbody.velocity.y = jumpForce;
                }
                else if (touchingGround.left)
                {
                    rigidbody.velocity = wallJumpForce.Clone();
                }
                else if (touchingGround.right)
                {
                    rigidbody.velocity = wallJumpForce * Vector.Create(-1, 1);
                }
            }
        }

        private void Move(UpdatePacket packet)
        {
            int moveAxis = 0;
            bool dDown = packet.inputPacket.KeyDown(KeyCode.D);
            bool adown = packet.inputPacket.KeyDown(KeyCode.A);
            if (dDown && !adown)
            {
                moveAxis = 1;
            }
            else if (!dDown && adown)
            {
                moveAxis = -1;
            }

            if (rigidbody.velocity.x < maxMoveSpeed && moveAxis == 1)
            {
                rigidbody.velocity.x += moveForce * packet.deltaTime.TotalSeconds;
            }
            else if (rigidbody.velocity.x > -maxMoveSpeed && moveAxis == -1)
            {
                rigidbody.velocity.x += -moveForce * packet.deltaTime.TotalSeconds;
            }
        }

        private void Drag(UpdatePacket packet)
        {
            if (rigidbody.velocity.x > 0)
            {
                rigidbody.velocity.x -= dragForce * packet.deltaTime.TotalSeconds;
                rigidbody.velocity.x = MathHelper.Clamp(rigidbody.velocity.x, 0, double.MaxValue);
            }
            else if (rigidbody.velocity.x > 0)
            {
                rigidbody.velocity.x -= -dragForce * packet.deltaTime.TotalSeconds;
                rigidbody.velocity.x = MathHelper.Clamp(rigidbody.velocity.x, double.MinValue, 0);
            }
        }

        private void Collision(UpdatePacket packet)
        {
            touchingGround = SideInfo.Create(false);
            foreach (Collision c in collider.collisions)
            {
                if (c.sideInfo.bottom)
                {
                    touchingGround.bottom = true;
                }
                if (c.sideInfo.top)
                {
                    touchingGround.top = true;
                }
                if (c.sideInfo.left)
                {
                    touchingGround.left = true;
                }
                if (c.sideInfo.right)
                {
                    touchingGround.right = true;
                }
            }
        }
        private Player()
        {
            ID = nextFreeID;
            nextFreeID++;
        }
        public static new Player Create(GameObject parent)
        {
            Player output = new Player();
            output.touchingGround = SideInfo.Create(false);
            output.rigidbody = null;
            output.collider = null;
            output.parent = parent;
            return output;
        }
        public static new Player Create()
        {
            Player output = new Player();
            output.touchingGround = SideInfo.Create(false);
            output.rigidbody = null;
            output.collider = null;
            output.parent = null;
            return output;
        }
    }
}