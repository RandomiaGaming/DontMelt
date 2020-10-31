using EpsilonEngine;
using System.Collections.Generic;
namespace Epsilon
{
    public class EpsilonGame : Game
    {
        public EpsilonGame(bool debugMode) : base(debugMode)
        {
            assetLoader = new AssetLoader();
            assetLoader.Initialize();

            for (int i = 0; i < 16; i++)
            {
                GameObject ground = new GameObject(this);
                ground.name = $"Ground ({i}, 0)";
                ground.position = new Point(i * 16, 0);
                ground.texture = assetLoader.GetTextureAsset("ground").data;
                ground.components = new List<Component>();

                Collider groundCollider = new Collider(ground);
                groundCollider.offset = Point.Zero;
                groundCollider.sideCollision = SideInfo.True;
                groundCollider.collisions = new List<Collision>();
                groundCollider.overlaps = new List<Overlap>();
                groundCollider.shape = new Rectangle(Point.Zero, new Point(16, 16));

                ground.components.Add(groundCollider);

                gameObjects.Add(ground);
            }

            for (int i = 0; i < 3; i++)
            {
                GameObject ground = new GameObject(this);
                ground.name = $"Ground (1, {i})";
                ground.position = new Point(16, (i * 16) + 16);
                ground.texture = assetLoader.GetTextureAsset("ground").data;
                ground.components = new List<Component>();

                Collider groundCollider = new Collider(ground);
                groundCollider.offset = Point.Zero;
                groundCollider.sideCollision = SideInfo.True;
                groundCollider.collisions = new List<Collision>();
                groundCollider.overlaps = new List<Overlap>();
                groundCollider.shape = new Rectangle(Point.Zero, new Point(16, 16));

                ground.components.Add(groundCollider);

                gameObjects.Add(ground);
            }

            GameObject player = new GameObject(this);
            player.name = $"Player)";
            player.position = new Point(128, 72);
            player.texture = assetLoader.GetTextureAsset("player").data;
            player.components = new List<Component>();

            Collider playerCollider = new Collider(player);
            playerCollider.offset = Point.Zero;
            playerCollider.sideCollision = SideInfo.True;
            playerCollider.collisions = new List<Collision>();
            playerCollider.overlaps = new List<Overlap>();
            playerCollider.shape = new Rectangle(Point.Zero, new Point(16, 16));

            Rigidbody playerRigidbody = new Rigidbody(player);
            playerRigidbody.velocity = Vector.Zero;

            Player playerComponent = new Player(player);

            player.components.Add(playerComponent);
            player.components.Add(playerCollider);
            player.components.Add(playerRigidbody);

            gameObjects.Add(player);

            if (debugMode)
            {
                gameManagers.Add(new TASHelper(this));
            }
        }
    }
}
