using System.Collections.Generic;

namespace DontMelt
{
    public sealed class StagePlayer
    {
        private List<GameObject> gameObjects = new List<GameObject>();
        public static void InstantiateLevel()
        {
            groundTexture = AssetHelper.GetTextureAsset("Ground.png").data;
            for (int i = 0; i < 16; i++)
            {
                CreateGround(i, 0);
            }
            for (int i = 0; i < 16; i++)
            {
                CreateGround(i, 8);
            }
            for (int i = 1; i < 8; i++)
            {
                CreateGround(0, i);
            }
            for (int i = 1; i < 8; i++)
            {
                CreateGround(15, i);
            }
            CreatePlayer();
        }
        private static void CreateGround(int x, int y)
        {
            GameObject tile = GameObject.Create();
            tile.position = new Point(x * 16, y * 16);
            tile.graphic = groundTexture;
            tile.name = "Ground";
            Ground groundComp = Ground.Create(tile);
            Collider collider = (Collider)Collider.Create(tile, new Rectangle(Point.Zero, new Point(16, 16)));
            tile.AddComponent(collider);
            tile.AddComponent(groundComp);
            DontMeltKernal.InstantiateGameObject(tile);
        }
        private static void CreatePlayer()
        {
            GameObject player = GameObject.Create();
            player.position = new Point(120, 64);
            player.name = "Player";
            player.AddComponent(Rigidbody.Create(player));
            Player playerComp = Player.Create(player);
            playerComp.facingRight = AssetHelper.LoadImage("DontMelt.Dont_Melt_Core.Embedded_Items.Player.PlayerRight.png");
            playerComp.facingLeft = AssetHelper.LoadImage("DontMelt.Dont_Melt_Core.Embedded_Items.Player.PlayerLeft.png");
            player.AddComponent(playerComp);
            player.graphic = playerComp.facingRight;
            player.AddComponent(Collider.Create(player, new Rectangle(new Point(2, 2), new Point(14, 14))));
            DontMeltKernal.InstantiateGameObject(player);
        }
    }
}
