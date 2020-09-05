using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

[RequireComponent(typeof(Tilemap))]
public sealed class RG_Tilemap_Collider : RG_Collider
{
    private Tilemap TM = null;
    [Range(0, 1)] public float Alpha_Threshold = 0.5f;

    public void Regenerate_Collider()
    {
        if (TM == null)
        {
            TM = GetComponent<Tilemap>();
        }

        Collider_Shape = new List<RG_Bounds>();
        TM.CompressBounds();

        List<RG_Bounds> First_Stage = new List<RG_Bounds>();
        for (int x = TM.cellBounds.xMin; x <= TM.cellBounds.xMax; x++)
        {
            RG_Bounds Bounds = new RG_Bounds(new Vector2Int(1, 1), new Vector2Int(0, 0));
            for (int y = TM.cellBounds.yMin; y <= TM.cellBounds.yMax; y++)
            {
                if (TM.GetTile(new Vector3Int(x, y, 0)) != null)
                {
                    if (TM.GetColliderType(new Vector3Int(x, y, 0)) == Tile.ColliderType.Grid)
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
                            First_Stage.Add(new RG_Bounds(Bounds.Min, Bounds.Max));
                            Bounds.Min = new Vector2Int(x, y);
                            Bounds.Max = new Vector2Int(x, y);
                        }
                    }
                    else if (TM.GetColliderType(new Vector3Int(x, y, 0)) == Tile.ColliderType.Sprite)
                    {
                        List<RG_Bounds> Sprite_Tile_Collider = Get_Sprite_Tile_Collider(new Vector3Int(x, y, 0));
                        if (Sprite_Tile_Collider != null)
                        {
                            Collider_Shape.AddRange(Sprite_Tile_Collider);
                        }
                    }
                }
            }

            First_Stage.Add(new RG_Bounds(Bounds.Min, Bounds.Max));
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

            Collider_Shape.Add(new RG_Bounds(Current_Bounds.Min * RG_Physics_Helper.Pixels_Per_Unit,
                ((Current_Bounds.Max + Vector2Int.one) * RG_Physics_Helper.Pixels_Per_Unit) - Vector2Int.one));
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

    private List<RG_Bounds> Get_Sprite_Tile_Collider(Vector3Int Position)
    {
        if (TM == null)
        {
            TM = GetComponent<Tilemap>();
        }

        Sprite Tile_Sprite = TM.GetSprite(Position);
        List<RG_Bounds> Tile_Collider = new List<RG_Bounds>();
        if (Tile_Sprite != null && Tile_Sprite.texture != null && Tile_Sprite.texture.isReadable &&
            Tile_Sprite.rect.width <= RG_Physics_Helper.Pixels_Per_Unit &&
            Tile_Sprite.rect.height <= RG_Physics_Helper.Pixels_Per_Unit)
        {
            Texture2D Tile_Texture = new Texture2D((int) Tile_Sprite.rect.width, (int) Tile_Sprite.rect.height);
            Tile_Texture.SetPixels(0, 0, (int) Tile_Sprite.rect.width, (int) Tile_Sprite.rect.height,
                Tile_Sprite.texture.GetPixels((int) Tile_Sprite.rect.x, (int) Tile_Sprite.rect.y,
                    (int) Tile_Sprite.rect.width, (int) Tile_Sprite.rect.height));
            List<RG_Bounds> First_Stage = new List<RG_Bounds>();
            for (int x = 0; x < Tile_Texture.width; x++)
            {
                RG_Bounds Bounds = new RG_Bounds(new Vector2Int(1, 1), new Vector2Int(0, 0));
                for (int y = 0; y < Tile_Texture.height; y++)
                {
                    if (Tile_Texture.GetPixel(x, y).a >= Alpha_Threshold)
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
                            First_Stage.Add(new RG_Bounds(Bounds.Min, Bounds.Max));
                            Bounds.Min = new Vector2Int(x, y);
                            Bounds.Max = new Vector2Int(x, y);
                        }
                    }
                }

                if (Bounds.Min.x <= Bounds.Max.x)
                {
                    First_Stage.Add(new RG_Bounds(Bounds.Min, Bounds.Max));
                }
            }

            for (int i = 0; i < First_Stage.Count; i++)
            {
                RG_Bounds Current_Bounds = First_Stage[i];
                for (int i2 = i + 1; i2 < First_Stage.Count; i2++)
                {
                    if (Current_Bounds.Min.y == First_Stage[i2].Min.y &&
                        Current_Bounds.Max.y == First_Stage[i2].Max.y &&
                        Current_Bounds.Max.x + 1 == First_Stage[i2].Min.x)
                    {
                        Current_Bounds.Max.x = First_Stage[i2].Max.x;
                        First_Stage.RemoveAt(i2);
                        i2--;
                    }
                }

                Tile_Collider.Add(new RG_Bounds(
                    Current_Bounds.Min + new Vector2Int(Position.x * RG_Physics_Helper.Pixels_Per_Unit,
                        Position.y * RG_Physics_Helper.Pixels_Per_Unit),
                    Current_Bounds.Max + new Vector2Int(Position.x * RG_Physics_Helper.Pixels_Per_Unit,
                        Position.y * RG_Physics_Helper.Pixels_Per_Unit)));
            }

            return Tile_Collider;
        }
        else
        {
            Debug.LogWarning("Collider could not be generated for tile at " + Position + ".");
            return null;
        }
    }
}