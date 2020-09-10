using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
public sealed class RG_Sprite_Collider : RG_Collider
{
    [Range(0, 1)] public float Alpha_Threshold = 0.5f;
    private SpriteRenderer SR = null;

    public void Regenerate_Collider()
    {
        if (SR == null)
        {
            SR = GetComponent<SpriteRenderer>();
        }

        if (SR == null || SR.sprite == null || SR.sprite.texture == null || !SR.sprite.texture.isReadable)
        {
            return;
        }

        Texture2D Collider_Shape_Texture = new Texture2D((int) SR.sprite.rect.width, (int) SR.sprite.rect.height);
        Collider_Shape_Texture.SetPixels(0, 0, (int) SR.sprite.rect.width, (int) SR.sprite.rect.height,
            SR.sprite.texture.GetPixels((int) SR.sprite.rect.x, (int) SR.sprite.rect.y, (int) SR.sprite.rect.width,
                (int) SR.sprite.rect.height));
        Collider_Shape = new List<RG_Bounds>();
        Vector2Int Sprite_Offset = new Vector2Int((int) SR.sprite.pivot.x * -1, (int) SR.sprite.pivot.y * -1);

        List<RG_Bounds> First_Stage = new List<RG_Bounds>();
        for (int x = 0; x < Collider_Shape_Texture.width; x++)
        {
            RG_Bounds Bounds = new RG_Bounds(new Vector2Int(1, 1), new Vector2Int(0, 0));
            for (int y = 0; y < Collider_Shape_Texture.height; y++)
            {
                if (Collider_Shape_Texture.GetPixel(x, y).a >= Alpha_Threshold)
                {
                    if (Bounds.Min.x > Bounds.Max.x)
                    {
                        Bounds.Min = new Vector2Int(x, y);
                        Bounds.Max = new Vector2Int(x, y);
                    }
                    else if (Bounds.Max.y + 1 == y)
                    {
                        Bounds.Max = new Vector2Int(x, y);
                    }
                    else
                    {
                        First_Stage.Add(new RG_Bounds(Bounds.Min + Sprite_Offset, Bounds.Max + Sprite_Offset));
                        Bounds.Min = new Vector2Int(x, y);
                        Bounds.Max = new Vector2Int(x, y);
                    }
                }
            }

            if (Bounds.Min.x <= Bounds.Max.x)
            {
                First_Stage.Add(new RG_Bounds(Bounds.Min + Sprite_Offset, Bounds.Max + Sprite_Offset));
            }
        }

        for (int i = 0; i < First_Stage.Count; i++)
        {
            RG_Bounds Current_Bounds = First_Stage[i];
            for (int i2 = i + 1; i2 < First_Stage.Count; i2++)
            {
                if (Current_Bounds.Min.y == First_Stage[i2].Min.y && Current_Bounds.Max.y == First_Stage[i2].Max.y &&
                    Current_Bounds.Max.x + 1 == First_Stage[i2].Min.x)
                {
                    Current_Bounds.Max.x = First_Stage[i2].Max.x;
                    First_Stage.RemoveAt(i2);
                    i2--;
                }
            }

            Collider_Shape.Add(Current_Bounds);
        }
    }

    private void Start()
    {
        Regenerate_Collider();
    }

    protected override void OnDrawGizmos()
    {
        Regenerate_Collider();
        base.OnDrawGizmos();
    }
}