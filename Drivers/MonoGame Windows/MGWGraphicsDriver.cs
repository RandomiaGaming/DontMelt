using EpsilonEngine;
namespace Epsilon.Drivers.MGW
{
    public class MGWGraphicsDriver : GraphicsDriver
    {
        private MGWInterfaceGame MGWInterfaceGame = null;
        public MGWGraphicsDriver(Game game) : base(game)
        {

        }
        public override void Draw(Texture frame)
        {
            MGWInterfaceGame.frameBuffer = frame;
        }

        public override Point GetViewPortRect()
        {
            return new Point(MGWInterfaceGame.GraphicsDevice.Viewport.Width, MGWInterfaceGame.GraphicsDevice.Viewport.Height);
        }

        public override void Initialize()
        {
            MGWInterfaceGame MGWIG = MGWInterfaceGame.GetFromEpsilonGame(game);
            if (MGWIG != null)
            {
                MGWInterfaceGame = MGWIG;
            }
            else
            {
                MGWInterfaceGame = new MGWInterfaceGame(game);
            }
            MGWInterfaceGame.Run();
        }

        public override void Update()
        {

        }
    }
}
