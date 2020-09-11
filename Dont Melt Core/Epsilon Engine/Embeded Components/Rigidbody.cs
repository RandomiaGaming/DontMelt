using System;

namespace EpsilonEngine
{
    public sealed class Rigidbody : Component
    {
        private Vector subPixelPosition = Vector.Create(0, 0);
        public Vector velocity = Vector.Create(0, 0);
        public float gravityScale = 0;
        public float liniarDrag = 0;
        public float bouncyness = 0;
        private Collider thisCollider = null;

        public override void Initialize()
        {
            thisCollider = (Collider)parent.GetComponent(typeof(Collider));
        }
        public override void Update(UpdatePacket packet)
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

            subPixelPosition += velocity * packet.deltaTime.TotalSeconds * EngineKernal.pixelsPerUnit;
            Point targetMove = Point.Create((int)subPixelPosition.x, (int)subPixelPosition.y);
            subPixelPosition -= Vector.Create((int)subPixelPosition.x, (int)subPixelPosition.y);
            Move(targetMove);
            //LogCollisions();
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
                                    targetMove.y = MathHelper.Clamp(maxMove, int.MinValue, 0);
                                }
                            }
                        }
                    }
                }
            }
            parent.position += targetMove;
        }
        /*
        private void LogCollisions()
        {
            foreach (RG_Collider This_RG_Collider in GetComponents<RG_Collider>())
            {
                if (This_RG_Collider.Is_Trigger)
                {
                    foreach (RG_Collider Other_RG_Collider in RG_Physics_Helper.Get_Managed_Colliders())
                    {
                        if (Other_RG_Collider.gameObject != gameObject)
                        {
                            bool Overlapped_Already = false;
                            foreach (RG_Bounds This_RG_Bounds in This_RG_Collider.Get_Collider_Shape_World())
                            {
                                if (Overlapped_Already)
                                {
                                    break;
                                }

                                foreach (RG_Bounds Other_RG_Bounds in Other_RG_Collider.Get_Collider_Shape_World())
                                {
                                    if (This_RG_Bounds.Max.x < Other_RG_Bounds.Min.x ||
                                        This_RG_Bounds.Min.x > Other_RG_Bounds.Max.x ||
                                        This_RG_Bounds.Max.y < Other_RG_Bounds.Min.y ||
                                        This_RG_Bounds.Min.y > Other_RG_Bounds.Max.y)
                                    {
                                    }
                                    else
                                    {
                                        This_RG_Collider.Log_Trigger_Overlap(Other_RG_Collider);
                                        Other_RG_Collider.Log_Trigger_Overlap(This_RG_Collider);
                                        Overlapped_Already = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (RG_Collider Other_RG_Collider in RG_Physics_Helper.Get_Managed_Colliders())
                    {
                        if (Other_RG_Collider.gameObject != gameObject)
                        {
                            if (Other_RG_Collider.Is_Trigger)
                            {
                                bool Overlapped_Already = false;
                                foreach (RG_Bounds This_RG_Bounds in This_RG_Collider.Get_Collider_Shape_World())
                                {
                                    if (Overlapped_Already)
                                    {
                                        break;
                                    }

                                    foreach (RG_Bounds Other_RG_Bounds in Other_RG_Collider.Get_Collider_Shape_World())
                                    {
                                        if (This_RG_Bounds.Max.x < Other_RG_Bounds.Min.x ||
                                            This_RG_Bounds.Min.x > Other_RG_Bounds.Max.x ||
                                            This_RG_Bounds.Max.y < Other_RG_Bounds.Min.y ||
                                            This_RG_Bounds.Min.y > Other_RG_Bounds.Max.y)
                                        {
                                        }
                                        else
                                        {
                                            This_RG_Collider.Log_Trigger_Overlap(Other_RG_Collider);
                                            Other_RG_Collider.Log_Trigger_Overlap(This_RG_Collider);
                                            Overlapped_Already = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                foreach (RG_Bounds This_RG_Bounds in This_RG_Collider.Get_Collider_Shape_World())
                                {
                                    foreach (RG_Bounds Other_RG_Bounds in Other_RG_Collider.Get_Collider_Shape_World())
                                    {
                                        if (This_RG_Bounds.Max.x < Other_RG_Bounds.Min.x ||
                                            This_RG_Bounds.Min.x > Other_RG_Bounds.Max.x ||
                                            This_RG_Bounds.Max.y < Other_RG_Bounds.Min.y ||
                                            This_RG_Bounds.Min.y > Other_RG_Bounds.Max.y)
                                        {
                                            if (This_RG_Bounds.Max.x + 1 < Other_RG_Bounds.Min.x ||
                                                This_RG_Bounds.Max.x + 1 > Other_RG_Bounds.Max.x ||
                                                This_RG_Bounds.Max.y < Other_RG_Bounds.Min.y ||
                                                This_RG_Bounds.Min.y > Other_RG_Bounds.Max.y)
                                            {
                                                if (This_RG_Bounds.Min.x - 1 < Other_RG_Bounds.Min.x ||
                                                    This_RG_Bounds.Min.x - 1 > Other_RG_Bounds.Max.x ||
                                                    This_RG_Bounds.Max.y < Other_RG_Bounds.Min.y ||
                                                    This_RG_Bounds.Min.y > Other_RG_Bounds.Max.y)
                                                {
                                                    if (This_RG_Bounds.Max.x < Other_RG_Bounds.Min.x ||
                                                        This_RG_Bounds.Min.x > Other_RG_Bounds.Max.x ||
                                                        This_RG_Bounds.Max.y + 1 < Other_RG_Bounds.Min.y ||
                                                        This_RG_Bounds.Max.y + 1 > Other_RG_Bounds.Max.y)
                                                    {
                                                        if (This_RG_Bounds.Max.x < Other_RG_Bounds.Min.x ||
                                                            This_RG_Bounds.Min.x > Other_RG_Bounds.Max.x ||
                                                            This_RG_Bounds.Min.y - 1 < Other_RG_Bounds.Min.y ||
                                                            This_RG_Bounds.Min.y - 1 > Other_RG_Bounds.Max.y)
                                                        {
                                                        }
                                                        else
                                                        {
                                                            This_RG_Collider.Log_Collision(Other_RG_Collider,
                                                                new RG_Side_Info(false, true, false, false));
                                                            Other_RG_Collider.Log_Collision(This_RG_Collider,
                                                                new RG_Side_Info(true, false, false, false));
                                                        }
                                                    }
                                                    else
                                                    {
                                                        This_RG_Collider.Log_Collision(Other_RG_Collider,
                                                            new RG_Side_Info(true, false, false, false));
                                                        Other_RG_Collider.Log_Collision(This_RG_Collider,
                                                            new RG_Side_Info(false, true, false, false));
                                                    }
                                                }
                                                else
                                                {
                                                    This_RG_Collider.Log_Collision(Other_RG_Collider,
                                                        new RG_Side_Info(false, false, true, false));
                                                    Other_RG_Collider.Log_Collision(This_RG_Collider,
                                                        new RG_Side_Info(false, false, false, true));
                                                }
                                            }
                                            else
                                            {
                                                This_RG_Collider.Log_Collision(Other_RG_Collider,
                                                    new RG_Side_Info(false, false, false, true));
                                                Other_RG_Collider.Log_Collision(This_RG_Collider,
                                                    new RG_Side_Info(false, false, true, false));
                                            }
                                        }
                                        else
                                        {
                                            This_RG_Collider.Log_Collision(Other_RG_Collider,
                                                new RG_Side_Info(true, true, true, true));
                                            Other_RG_Collider.Log_Collision(This_RG_Collider,
                                                new RG_Side_Info(true, true, true, true));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }*/
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
            output.subPixelPosition = Vector.Create(0, 0);
            output.thisCollider = (Collider)parent.GetComponent(typeof(Collider));
            output.velocity = Vector.Create(0, 0);
            output.parent = parent;
            return output;
        }
        public static new Rigidbody Create()
        {
            Rigidbody output = new Rigidbody();
            output.gravityScale = 1;
            output.bouncyness = 0;
            output.liniarDrag = 0;
            output.subPixelPosition = Vector.Create(0, 0);
            output.thisCollider = null;
            output.velocity = Vector.Create(0, 0);
            output.parent = null;
            return output;
        }
    }
}