using System;
using System.Collections.Generic;
namespace EpsilonEngine
{
    public class Collider : Component
    {
        private Collision[] collisions = new Collision[0];
        private Overlap[] overlaps = new Overlap[0];
        protected Rectangle[] shape = new Rectangle[0];

        public Point offset = Point.Create(0, 0);
        public SideInfo sideCollision = SideInfo.Create(true);
        public bool trigger = false;

        public virtual void LogCollision(Collider otherCollider, SideInfo sideInfo)
        {
            RG_Collision New_Collision = RG_Collision.Create();
            New_Collision.Other_Game_Object = OtherRGC;
            New_Collision.Other_Collider = OtherRGC;
            New_Collision.Side_Info = SideInfo;
            for (int i = 0; i < Collisions.Count; i++)
            {
                if (Collisions[i].Other_Collider == OtherRGC)
                {
                    if (Collisions[i].Side_Info.Bottom)
                    {
                        New_Collision.Side_Info.Bottom = true;
                    }

                    if (Collisions[i].Side_Info.Top)
                    {
                        New_Collision.Side_Info.Top = true;
                    }

                    if (Collisions[i].Side_Info.Right)
                    {
                        New_Collision.Side_Info.Right = true;
                    }

                    if (Collisions[i].Side_Info.Left)
                    {
                        New_Collision.Side_Info.Left = true;
                    }

                    Collisions.RemoveAt(i);
                    i--;
                }
            }

            Collisions.Add(New_Collision);
        }

        public virtual void LogOverlap(Collider OtherRGC)
        {
            RG_Trigger_Overlap New_Trigger_Overlap = RG_Trigger_Overlap.Create();
            New_Trigger_Overlap.Other_Game_Object = OtherRGC.gameObject;
            New_Trigger_Overlap.Other_Collider = OtherRGC;
            for (int i = 0; i < Collisions.Count; i++)
            {
                if (Collisions[i].Other_Collider == OtherRGC)
                {
                    Collisions.RemoveAt(i);
                }
            }

            Trigger_Overlaps.Add(New_Trigger_Overlap);
        }

        public virtual List<RG_Bounds> Get_Collider_Shape_Local()
        {
            List<RG_Bounds> Output = new List<RG_Bounds>();
            foreach (RG_Bounds This_Bounds in Collider_Shape)
            {
                Output.Add(RG_Bounds.Create(This_Bounds.Get_Min() + Offset, This_Bounds.Get_Max() + Offset));
            }
            return Output;
        }

        public virtual List<RG_Bounds> Get_Collider_Shape_World()
        {
            List<RG_Bounds> Output = new List<RG_Bounds>();
            foreach (RG_Bounds This_Bounds in Collider_Shape)
            {
                Output.Add(new RG_Bounds(Delocalize(This_Bounds.Min + Offset), Delocalize(This_Bounds.Max + Offset)));
            }

            return Output;
        }

        public virtual List<RG_Collision> Get_Collisions()
        {
            return new List<RG_Collision>(Collisions);
        }

        public virtual List<RG_Trigger_Overlap> Get_Trigger_Overlaps()
        {
            return new List<RG_Trigger_Overlap>(Trigger_Overlaps);
        }

        public virtual Vector2Int Localize(Vector2Int PixelPoint)
        {
            return PixelPoint - RG_Physics_Helper.World_To_Pixel(transform.position);
        }

        public virtual Vector2Int Delocalize(Vector2Int LocalPoint)
        {
            return LocalPoint + RG_Physics_Helper.World_To_Pixel(transform.position);
        }
        public override void Update(UpdatePacket Packet)
        {
            Console.WriteLine(1 / Packet.deltaTime.TotalSeconds);
            parent.position += Packet.inputPacket.keyDirection;
        }
        public static new Component Create(GameObject parent)
        {
            Component output = new Collider();
            output.parent = parent;
            return output;
        }
        public static new TestComponent Create()
        {
            TestComponent output = new TestComponent();
            output.name = "Unnamed Component";
            output.parent = null;
            return output;
        }
    }
}