using Epsilon.Drivers.MGW;
using EpsilonEngine;
using System.Collections.Generic;
namespace Epsilon
{
    public class EpsilonGame : Game
    {
        public EpsilonGame()
        {
            assetLoader = new AssetLoader(this);
            assetLoader.Initialize();
            inputDriver = new MGWInputDriver(this);
            graphicsDriver = new MGWGraphicsDriver(this);

            for (int i = 0; i < 16; i++)
            {
                GameObject ground = new GameObject(this)
                {
                    name = $"Ground ({i}, 0)",
                    position = new Point(i * 16, 0),
                    texture = assetLoader.GetTextureAsset("ground").data,
                    components = new List<Component>()
                };

                Collider groundCollider = new Collider(ground)
                {
                    offset = Point.Zero,
                    sideCollision = SideInfo.True,
                    collisions = new List<Collision>(),
                    overlaps = new List<Overlap>(),
                    shape = new Rectangle(Point.Zero, new Point(16, 16))
                };

                ground.components.Add(groundCollider);

                gameObjects.Add(ground);
            }

            GameObject player = new GameObject(this)
            {
                name = $"Player)",
                position = new Point(128, 72),
                texture = assetLoader.GetTextureAsset("player").data,
                components = new List<Component>()
            };

            Collider playerCollider = new Collider(player)
            {
                offset = Point.Zero,
                sideCollision = SideInfo.True,
                collisions = new List<Collision>(),
                overlaps = new List<Overlap>(),
                shape = new Rectangle(Point.Zero, new Point(16, 16))
            };

            Rigidbody playerRigidbody = new Rigidbody(player);

            Player playerComponent = new Player(player);

            player.components.Add(playerComponent);
            player.components.Add(playerCollider);
            player.components.Add(playerRigidbody);

            gameObjects.Add(player);

            gameManagers.Add(new TASHelper(this));
        }
    }
}
