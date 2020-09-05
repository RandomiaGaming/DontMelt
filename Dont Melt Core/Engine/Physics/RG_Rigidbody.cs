using UnityEngine;

[DisallowMultipleComponent]
public sealed class RG_Rigidbody : MonoBehaviour
{
    private Vector2 SubPixel = new Vector2(0, 0);
    [HideInInspector] public Vector2 Velocity = new Vector2(0, 0);
    public float Gravity_Scale = 1;
    public float Drag_Scale = 5;
    public float Bounce_Scale = 0;

    private void Update()
    {
        Velocity.y -= 9.8067f * Time.deltaTime * Gravity_Scale;
        if (Velocity.x < 0)
        {
            Velocity.x += Drag_Scale * Time.deltaTime;
            Velocity.x = Mathf.Clamp(Velocity.x, float.MinValue, 0);
        }
        else
        {
            Velocity.x -= Drag_Scale * Time.deltaTime;
            Velocity.x = Mathf.Clamp(Velocity.x, 0, float.MaxValue);
        }

        if (Velocity.y < 0)
        {
            Velocity.y += Drag_Scale * Time.deltaTime;
            Velocity.y = Mathf.Clamp(Velocity.y, float.MinValue, 0);
        }
        else
        {
            Velocity.y -= Drag_Scale * Time.deltaTime;
            Velocity.y = Mathf.Clamp(Velocity.y, 0, float.MaxValue);
        }

        SubPixel += Velocity * Time.deltaTime * RG_Physics_Helper.Pixels_Per_Unit;
        Vector2Int Target_Move = new Vector2Int((int) SubPixel.x, (int) SubPixel.y);
        SubPixel -= new Vector2((int) SubPixel.x, (int) SubPixel.y);
        Move(Target_Move);
        Log_Collisions_And_Trigger_Overlaps();
    }

    private void Move(Vector2Int Target_Move)
    {
        foreach (RG_Collider This_RG_Collider in GetComponents<RG_Collider>())
        {
            if (!This_RG_Collider.Is_Trigger)
            {
                foreach (RG_Bounds This_RG_Bounds in This_RG_Collider.Get_Collider_Shape_World())
                {
                    if (Target_Move.x > 0)
                    {
                        foreach (RG_Collider Other_Collider in RG_Physics_Helper.Get_Managed_Colliders())
                        {
                            if (Other_Collider.gameObject != gameObject && !Other_Collider.Is_Trigger)
                            {
                                foreach (RG_Bounds Other_RG_Bounds in Other_Collider.Get_Collider_Shape_World())
                                {
                                    if (This_RG_Bounds.Max.y < Other_RG_Bounds.Min.y ||
                                        This_RG_Bounds.Min.y > Other_RG_Bounds.Max.y ||
                                        This_RG_Bounds.Max.x + Target_Move.x < Other_RG_Bounds.Min.x ||
                                        This_RG_Bounds.Min.x > Other_RG_Bounds.Max.x)
                                    {
                                    }
                                    else
                                    {
                                        RG_Rigidbody Other_Rigidbody =
                                            Other_Collider.gameObject.GetComponent<RG_Rigidbody>();
                                        if (Other_Rigidbody != null)
                                        {
                                            Other_Rigidbody.Velocity.x = Velocity.x;
                                        }

                                        Velocity.x = Velocity.x * -1 * Bounce_Scale;
                                        Target_Move.x = (Other_RG_Bounds.Min.x - This_RG_Bounds.Max.x) - 1;
                                        Target_Move.x = Mathf.Clamp(Target_Move.x, 0, int.MaxValue);
                                    }
                                }
                            }
                        }
                    }
                    else if (Target_Move.x < 0)
                    {
                        foreach (RG_Collider Other_Collider in RG_Physics_Helper.Get_Managed_Colliders())
                        {
                            if (Other_Collider.gameObject != gameObject && !Other_Collider.Is_Trigger)
                            {
                                foreach (RG_Bounds Other_RG_Bounds in Other_Collider.Get_Collider_Shape_World())
                                {
                                    if (This_RG_Bounds.Max.y < Other_RG_Bounds.Min.y ||
                                        This_RG_Bounds.Min.y > Other_RG_Bounds.Max.y ||
                                        This_RG_Bounds.Max.x < Other_RG_Bounds.Min.x ||
                                        This_RG_Bounds.Min.x + Target_Move.x > Other_RG_Bounds.Max.x)
                                    {
                                    }
                                    else
                                    {
                                        RG_Rigidbody Other_Rigidbody =
                                            Other_Collider.gameObject.GetComponent<RG_Rigidbody>();
                                        if (Other_Rigidbody != null)
                                        {
                                            Other_Rigidbody.Velocity.x = Velocity.x;
                                        }

                                        Velocity.x = Velocity.x * -1 * Bounce_Scale;
                                        Target_Move.x = (Other_RG_Bounds.Max.x - This_RG_Bounds.Min.x) + 1;
                                        Target_Move.x = Mathf.Clamp(Target_Move.x, int.MinValue, 0);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            transform.position =
                RG_Physics_Helper.Pixel_To_World(RG_Physics_Helper.World_To_Pixel(transform.position) +
                                                 new Vector2Int(Target_Move.x, 0));
        }

        foreach (RG_Collider This_RG_Collider in GetComponents<RG_Collider>())
        {
            if (!This_RG_Collider.Is_Trigger)
            {
                foreach (RG_Bounds This_RG_Bounds in This_RG_Collider.Get_Collider_Shape_World())
                {
                    if (Target_Move.y > 0)
                    {
                        foreach (RG_Collider Other_Collider in RG_Physics_Helper.Get_Managed_Colliders())
                        {
                            if (Other_Collider.gameObject != gameObject && !Other_Collider.Is_Trigger)
                            {
                                foreach (RG_Bounds Other_RG_Bounds in Other_Collider.Get_Collider_Shape_World())
                                {
                                    if (This_RG_Bounds.Max.x < Other_RG_Bounds.Min.x ||
                                        This_RG_Bounds.Min.x > Other_RG_Bounds.Max.x ||
                                        This_RG_Bounds.Max.y + Target_Move.y < Other_RG_Bounds.Min.y ||
                                        This_RG_Bounds.Min.y > Other_RG_Bounds.Max.y)
                                    {
                                    }
                                    else
                                    {
                                        RG_Rigidbody Other_Rigidbody =
                                            Other_Collider.gameObject.GetComponent<RG_Rigidbody>();
                                        if (Other_Rigidbody != null)
                                        {
                                            Other_Rigidbody.Velocity.y = Velocity.y;
                                        }

                                        Velocity.y = Velocity.y * -1 * Bounce_Scale;
                                        Target_Move.y = (Other_RG_Bounds.Min.y - This_RG_Bounds.Max.y) - 1;
                                        Target_Move.y = Mathf.Clamp(Target_Move.y, 0, int.MaxValue);
                                    }
                                }
                            }
                        }
                    }
                    else if (Target_Move.y < 0)
                    {
                        foreach (RG_Collider Other_Collider in RG_Physics_Helper.Get_Managed_Colliders())
                        {
                            if (Other_Collider.gameObject != gameObject && !Other_Collider.Is_Trigger)
                            {
                                foreach (RG_Bounds Other_RG_Bounds in Other_Collider.Get_Collider_Shape_World())
                                {
                                    if (This_RG_Bounds.Max.x < Other_RG_Bounds.Min.x ||
                                        This_RG_Bounds.Min.x > Other_RG_Bounds.Max.x ||
                                        This_RG_Bounds.Max.y < Other_RG_Bounds.Min.y ||
                                        This_RG_Bounds.Min.y + Target_Move.y > Other_RG_Bounds.Max.y)
                                    {
                                    }
                                    else
                                    {
                                        RG_Rigidbody Other_Rigidbody =
                                            Other_Collider.gameObject.GetComponent<RG_Rigidbody>();
                                        if (Other_Rigidbody != null)
                                        {
                                            Other_Rigidbody.Velocity.y = Velocity.y;
                                        }

                                        Velocity.y = Velocity.y * -1 * Bounce_Scale;
                                        Target_Move.y = (Other_RG_Bounds.Max.y - This_RG_Bounds.Min.y) + 1;
                                        Target_Move.y = Mathf.Clamp(Target_Move.y, int.MinValue, 0);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            transform.position =
                RG_Physics_Helper.Pixel_To_World(RG_Physics_Helper.World_To_Pixel(transform.position) +
                                                 new Vector2Int(0, Target_Move.y));
        }
    }

    private void Log_Collisions_And_Trigger_Overlaps()
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
    }
}