namespace EpsilonEngine
{
    public sealed class Rigidbody : Component
    {
        private Vector2 subPixel = Vector2.Zero;
        public Vector2 velocity = Vector2.Zero;

        private Collider thisCollider = null;
        public Rigidbody(GameObject gameObject) : base(gameObject)
        {

        }
        public override void Initialize()
        {
            thisCollider = gameObject.GetComponent<Collider>();
        }
        public override void Update()
        {
            subPixel += velocity;
            Point targetMove = new Point((int)subPixel.x, (int)subPixel.y);
            subPixel -= new Vector2((int)subPixel.x, (int)subPixel.y);

            Move(targetMove);
            LogCollisionsAndOverlaps();
        }
        private void Move(Point targetMove)
        {
            if (thisCollider != null && !thisCollider.trigger)
            {
                Rectangle thisColliderShape = thisCollider.GetWorldShape();
                if (targetMove.x > 0)
                {
                    for (int i = 0; i < Collider.loadedColliders.Count; i++)
                    {
                        if (Collider.loadedColliders[i] != thisCollider)
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
                    for (int i = 0; i < Collider.loadedColliders.Count; i++)
                    {
                        if (Collider.loadedColliders[i] != thisCollider)
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
                thisColliderShape = thisCollider.GetWorldShape();
                if (targetMove.y > 0)
                {
                    for (int i = 0; i < Collider.loadedColliders.Count; i++)
                    {
                        if (Collider.loadedColliders[i] != thisCollider)
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
                    for (int i = 0; i < Collider.loadedColliders.Count; i++)
                    {
                        if (Collider.loadedColliders[i] != thisCollider)
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
           gameObject.position += targetMove;
        }
        public void LogCollisionsAndOverlaps()
        {
            if (thisCollider != null)
            {
                if (thisCollider.trigger)
                {
                    Rectangle thisColliderShape = thisCollider.GetWorldShape();
                    for (int i = 0; i < Collider.loadedColliders.Count; i++)
                    {
                        if (Collider.loadedColliders[i] != thisCollider)
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
                    for (int i = 0; i < Collider.loadedColliders.Count; i++)
                    {
                        if (Collider.loadedColliders[i] != thisCollider)
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
    }
}