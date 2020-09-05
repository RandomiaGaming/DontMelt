using UnityEngine;
using System.Collections.Generic;

public sealed class RG_Box_Collider : RG_Collider
{
    public Vector2Int Size = new Vector2Int(RG_Physics_Helper.Pixels_Per_Unit, RG_Physics_Helper.Pixels_Per_Unit);

    private void Start()
    {
        Collider_Shape = new List<RG_Bounds>();
        Vector2Int Min = Vector2Int.zero;
        Vector2Int Max = Vector2Int.zero;
        for (int x = Size.x - 1; x > 0; x--)
        {
            if (Mathf.Abs(Min.x) > Mathf.Abs(Max.x))
            {
                Max.x++;
            }
            else
            {
                Min.x--;
            }
        }

        for (int y = Size.y - 1; y > 0; y--)
        {
            if (Mathf.Abs(Min.y) > Mathf.Abs(Max.y))
            {
                Max.y++;
            }
            else
            {
                Min.y--;
            }
        }

        Collider_Shape.Add(new RG_Bounds(Min, Max));
    }

    protected override void OnDrawGizmos()
    {
        Collider_Shape = new List<RG_Bounds>();
        Vector2Int Min = Vector2Int.zero;
        Vector2Int Max = Vector2Int.zero;
        for (int x = Size.x - 1; x > 0; x--)
        {
            if (Mathf.Abs(Min.x) > Mathf.Abs(Max.x))
            {
                Max.x++;
            }
            else
            {
                Min.x--;
            }
        }

        for (int y = Size.y - 1; y > 0; y--)
        {
            if (Mathf.Abs(Min.y) > Mathf.Abs(Max.y))
            {
                Max.y++;
            }
            else
            {
                Min.y--;
            }
        }

        Collider_Shape.Add(new RG_Bounds(Min, Max));
        base.OnDrawGizmos();
    }
}