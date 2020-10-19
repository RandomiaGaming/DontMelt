namespace EpsilonEngine
{
    public sealed class Rigidbody : Component
    {
        private Vector subPixelPosition = Vector.Zero;
        public Vector velocity = Vector.Zero;
        public float gravityScale = 0;
        public float liniarDrag = 0;
        public float bouncyness = 0;
        private Collider thisCollider = null;

        public override void Initialize(InitializationInputPacket packet)
        {
            thisCollider = (Collider)parent.GetComponent(typeof(Collider));
        }
        public override UpdateOutputPacket Update(UpdateInputPacket packet)
        {
            velocity.y -= 9.80665f * packet.deltaTime.TotalSeconds * gravityScale;
            if (velocity.x < 0)
            {
                velocity.x += liniarDrag * packet.deltaTime.TotalSeconds;
                velocity.x = MathHelper.Clamp(velocity.x, float.MinValue, 0);
            }
            else
            {
                velocity.x -= liniarDrag * packet.deltaTime.TotalSeconds;
                velocity.x = MathHelper.Clamp(velocity.x, 0, float.MaxValue);
            }
            if (velocity.y < 0)
            {
                velocity.y += liniarDrag * packet.deltaTime.TotalSeconds;
                velocity.y = MathHelper.Clamp(velocity.y, float.MinValue, 0);
            }
            else
            {
                velocity.y -= liniarDrag * packet.deltaTime.TotalSeconds;
                velocity.y = MathHelper.Clamp(velocity.y, 0, float.MaxValue);
            }

            subPixelPosition += velocity * packet.deltaTime.TotalSeconds;
            Point targetMove = new Point((int)subPixelPosition.x, (int)subPixelPosition.y);
            subPixelPosition -= new Vector((int)subPixelPosition.x, (int)subPixelPosition.y);
            Move(targetMove);
            LogCollisionsAndOverlaps();
            return new UpdateOutputPacket();
        }
        private void Move(Point targetMove)
        {
            if (thisCollider != null && !thisCollider.trigger)
            {
                Rectangle thisColliderShape = thisCollider.GetWorldShape();
                if (targetMove.x > 0)
                {
                    for (int i = 0; i < Collider.loadedColliders.Length; i++)
                    {
                        if (Collider.loadedColliders[i].ID != thisCollider.ID)
                        {
                            Rectangle otherColliderShape = Collider.loadedColliders[i].GetWorldShape();
                            if (thisColliderShape.min.y < otherColliderShape.max.y && thisColliderShape.max.y > otherColliderShape.min.y)
                            {
                                if (thisColliderShape.min.x < otherColliderShape.max.x)
                                {
                                    int maxMove = MathHelper.Min(targetMove.x, otherColliderShape.min.x - thisColliderShape.max.x);
                                    if (maxMove != targetMove.x)
                                    {
                                        velocity.x = 0;
                                    }
                                    targetMove.x = MathHelper.Clamp(maxMove, 0, int.MaxValue);
                                }
                            }
                        }
                    }
                }
                else if (targetMove.x < 0)
                {
                    for (int i = 0; i < Collider.loadedColliders.Length; i++)
                    {
                        if (Collider.loadedColliders[i].ID != thisCollider.ID)
                        {
                            Rectangle otherColliderShape = Collider.loadedColliders[i].GetWorldShape();
                            if (thisColliderShape.min.y < otherColliderShape.max.y && thisColliderShape.max.y > otherColliderShape.min.y)
                            {
                                if (thisColliderShape.max.x > otherColliderShape.min.x)
                                {
                                    int maxMove = MathHelper.Max(targetMove.x, otherColliderShape.max.x - thisColliderShape.min.x);
                                    if (maxMove != targetMove.x)
                                    {
                                        velocity.x = 0;
                                    }
                                    targetMove.x = MathHelper.Clamp(maxMove, int.MinValue, 0);
                                }
                            }
                        }
                    }
                }
                if (targetMove.y > 0)
                {
                    for (int i = 0; i < Collider.loadedColliders.Length; i++)
                    {
                        if (Collider.loadedColliders[i].ID != thisCollider.ID)
                        {
                            Rectangle otherColliderShape = Collider.loadedColliders[i].GetWorldShape();
                            if (thisColliderShape.min.x < otherColliderShape.max.x && thisColliderShape.max.x > otherColliderShape.min.x)
                            {
                                if (thisColliderShape.min.y < otherColliderShape.max.y)
                                {
                                    int maxMove = MathHelper.Min(targetMove.y, otherColliderShape.min.y - thisColliderShape.max.y);
                                    if (maxMove != targetMove.y)
                                    {
                                        velocity.y = 0;
                                    }
                                    targetMove.y = MathHelper.Clamp(maxMove, 0, int.MaxValue);
                                }
                            }
                        }
                    }
                }
                else if (targetMove.y < 0)
                {
                    for (int i = 0; i < Collider.loadedColliders.Length; i++)
                    {
                        if (Collider.loadedColliders[i].ID != thisCollider.ID)
                        {
                            Rectangle otherColliderShape = Collider.loadedColliders[i].GetWorldShape();
                            if (thisColliderShape.min.x < otherColliderShape.max.x && thisColliderShape.max.x > otherColliderShape.min.x)
                            {
                                if (thisColliderShape.max.y > otherColliderShape.min.y)
                                {
                                    int maxMove = MathHelper.Max(targetMove.y, otherColliderShape.max.y - thisColliderShape.min.y);
                                    if (maxMove != targetMove.y)
                                    {
                                        velocity.y = 0;
                                    }
                                    targetMove.y = MathHelper.Clamp(maxMove, int.MinValue, 0);
                                }
                            }
                        }
                    }
                }
            }
            parent.position += targetMove;
        }
        public void LogCollisionsAndOverlaps()
        {
            if (thisCollider != null)
            {
                if (thisCollider.trigger)
                {
                    Rectangle thisColliderShape = thisCollider.GetWorldShape();
                    for (int i = 0; i < Collider.loadedColliders.Length; i++)
                    {
                        if (Collider.loadedColliders[i].ID != thisCollider.ID)
                        {
                            Rectangle otherColliderShape = Collider.loadedColliders[i].GetWorldShape();
                            if (thisColliderShape.min.y < otherColliderShape.max.y && thisColliderShape.max.y > otherColliderShape.min.y && thisColliderShape.min.x < otherColliderShape.max.x && thisColliderShape.max.x > otherColliderShape.min.x)
                            {
                                thisCollider.LogOverlap(Collider.loadedColliders[i]);
                                Collider.loadedColliders[i].LogOverlap(thisCollider);
                            }
                        }
                    }
                }
                else
                {
                    Rectangle thisColliderShape = thisCollider.GetWorldShape();
                    for (int i = 0; i < Collider.loadedColliders.Length; i++)
                    {
                        if (Collider.loadedColliders[i].ID != thisCollider.ID)
                        {
                            Rectangle otherColliderShape = Collider.loadedColliders[i].GetWorldShape();
                            if (Collider.loadedColliders[i].trigger)
                            {
                                if (thisColliderShape.min.y < otherColliderShape.max.y && thisColliderShape.max.y > otherColliderShape.min.y && thisColliderShape.min.x < otherColliderShape.max.x && thisColliderShape.max.x > otherColliderShape.min.x)
                                {
                                    thisCollider.LogOverlap(Collider.loadedColliders[i]);
                                }
                            }
                            else
                            {
                                if (thisColliderShape.min.y < otherColliderShape.max.y && thisColliderShape.max.y > otherColliderShape.min.y && thisColliderShape.max.x == otherColliderShape.min.x)
                                {
                                    thisCollider.LogCollision(Collider.loadedColliders[i], new SideInfo(false, false, false, true));
                                    Collider.loadedColliders[i].LogCollision(thisCollider, new SideInfo(false, false, true, false));
                                }
                                else if (thisColliderShape.min.y < otherColliderShape.max.y && thisColliderShape.max.y > otherColliderShape.min.y && thisColliderShape.min.x == otherColliderShape.max.x)
                                {
                                    thisCollider.LogCollision(Collider.loadedColliders[i], new SideInfo(false, false, true, false));
                                    Collider.loadedColliders[i].LogCollision(thisCollider, new SideInfo(false, false, false, true));
                                }
                                else if (thisColliderShape.min.x < otherColliderShape.max.x && thisColliderShape.max.x > otherColliderShape.min.x && thisColliderShape.max.y == otherColliderShape.min.y)
                                {
                                    thisCollider.LogCollision(Collider.loadedColliders[i], new SideInfo(true, false, false, false));
                                    Collider.loadedColliders[i].LogCollision(thisCollider, new SideInfo(false, true, false, false));
                                }
                                else if (thisColliderShape.min.x < otherColliderShape.max.x && thisColliderShape.max.x > otherColliderShape.min.x && thisColliderShape.min.y == otherColliderShape.max.y)
                                {
                                    thisCollider.LogCollision(Collider.loadedColliders[i], new SideInfo(false, true, false, false));
                                    Collider.loadedColliders[i].LogCollision(thisCollider, new SideInfo(true, false, false, false));
                                }
                                else if (thisColliderShape.min.y < otherColliderShape.max.y && thisColliderShape.max.y > otherColliderShape.min.y && thisColliderShape.min.x < otherColliderShape.max.x && thisColliderShape.max.x > otherColliderShape.min.x)
                                {
                                    thisCollider.LogCollision(Collider.loadedColliders[i], SideInfo.True);
                                    Collider.loadedColliders[i].LogCollision(thisCollider, SideInfo.True);
                                }
                            }
                        }
                    }
                }
            }
        }
        private Rigidbody()
        {
            ID = nextFreeID;
            nextFreeID++;
        }
        public static new Rigidbody Create(GameObject parent)
        {
            Rigidbody output = new Rigidbody();
            output.gravityScale = 1;
            output.bouncyness = 0;
            output.liniarDrag = 0;
            output.subPixelPosition = Vector.Zero;
            output.thisCollider = (Collider)parent.GetComponent(typeof(Collider));
            output.velocity = Vector.Zero;
            output.parent = parent;
            return output;
        }
        public static new Rigidbody Create()
        {
            Rigidbody output = new Rigidbody();
            output.gravityScale = 1;
            output.bouncyness = 0;
            output.liniarDrag = 0;
            output.subPixelPosition = Vector.Zero;
            output.thisCollider = null;
            output.velocity = Vector.Zero;
            output.parent = null;
            return output;
        }
    }
}