using EpsilonEngine;
namespace Epsilon
{
    public class Player : Component
    {
        public Texture facingRight = new Texture();
        public Texture facingLeft = new Texture();
        public SideInfo touchingGround = SideInfo.False;

        public double moveForce = 20;
        public double jumpForce = 8.5f;
        public double maxMoveSpeed = 6.5f;
        public Vector wallJumpForce = new Vector(8.5, 6);
        public double dragForce = 8;
        public double gravityForce = 9.80665;

        public Rigidbody rigidbody = null;
        private Collider collider = null;

        public Player(GameObject gameObject) : base(gameObject)
        {

        }

        public override void Initialize()
        {
            rigidbody = (Rigidbody)gameObject.GetComponentsOfType(typeof(Rigidbody))[0];
            collider = (Collider)gameObject.GetComponentsOfType(typeof(Collider))[0];
            TextureAsset playerSpriteSheet = game.assetLoader.GetTextureAsset("playerspritesheet");
            /*
            facingLeft = new Texture(16, 16);
            facingRight = new Texture(16, 16);
            facingLeft.Blitz(playerSpriteSheet.data, 0, 0);
            facingRight.Blitz(playerSpriteSheet.data, -16, 0);
            */
            facingLeft = playerSpriteSheet.data;
            facingRight = playerSpriteSheet.data;
            gameObject.texture = facingRight;
        }
        public override void Update()
        {
            rigidbody.velocity.y -= gravityForce / 60;
            Collision();
            Move();
            Jump();
            Drag();
        }
        private void Jump()
        {
            if (game.inputDriver.IsKeyPressed(KeyCode.Space))
            {
                if (touchingGround.bottom)
                {
                    rigidbody.velocity.y = jumpForce;
                }
                else if (touchingGround.left)
                {
                    rigidbody.velocity = wallJumpForce;
                }
                else if (touchingGround.right)
                {
                    rigidbody.velocity = wallJumpForce * new Vector(-1, 1);
                }
            }
        }

        private void Move()
        {
            int moveAxis = 0;
            bool dDown = game.inputDriver.IsKeyPressed(KeyCode.D);
            bool adown = game.inputDriver.IsKeyPressed(KeyCode.A);
            if (dDown && !adown)
            {
                moveAxis = 1;
                gameObject.texture = facingRight;
            }
            else if (!dDown && adown)
            {
                moveAxis = -1;
                gameObject.texture = facingLeft;
            }

            if (rigidbody.velocity.x < maxMoveSpeed && moveAxis == 1)
            {
                rigidbody.velocity.x += moveForce / 60;
            }
            else if (rigidbody.velocity.x > -maxMoveSpeed && moveAxis == -1)
            {
                rigidbody.velocity.x += -moveForce / 60;
            }
        }

        private void Drag()
        {
            if (rigidbody.velocity.x > 0)
            {
                rigidbody.velocity.x -= dragForce / 60;
                rigidbody.velocity.x = MathHelper.Clamp(rigidbody.velocity.x, 0, double.MaxValue);
            }
            else if (rigidbody.velocity.x < 0)
            {
                rigidbody.velocity.x -= -dragForce / 60;
                rigidbody.velocity.x = MathHelper.Clamp(rigidbody.velocity.x, double.MinValue, 0);
            }
        }

        private void Collision()
        {
            touchingGround = SideInfo.False;
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
    }
}