using DontMelt;
using DontMelt.Internal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
public class InterfaceGame : Game
{
    private GraphicsDeviceManager GDM = null;
    private OutputPacket LastPacket = null;
    public InterfaceGame()
    {
        Content.RootDirectory = "Dont Melt Core/Assets";
        GDM = new GraphicsDeviceManager(this);
    }
    protected override void Initialize()
    {
        GDM.IsFullScreen = false;
        Window.AllowUserResizing = true;
        IsMouseVisible = true;
        DontMeltKernal.Initialize(InitializationPacket.Create());
        base.Initialize();
    }
    protected override void Update(GameTime gameTime)
    {
        LastPacket = DontMeltKernal.Update(InputPacket.Create());
        if (LastPacket.RequestExit)
        {
            Exit();
        }
        base.Update(gameTime);
    }
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Microsoft.Xna.Framework.Color(1f, 0f, 1f));
        if (LastPacket != null)
        {
            int FrameWidth = LastPacket.FrameTexture.Get_Width();
            int FrameHeight = LastPacket.FrameTexture.Get_Height();
            var MonoGameFrameColors = new Microsoft.Xna.Framework.Color[FrameHeight * FrameWidth];
            for (int x = 0; x < FrameWidth; x++)
            {
                for (int y = 0; y < FrameHeight; y++)
                {
                    DontMelt.Color DontMeltColor = LastPacket.FrameTexture.Get_Pixel(DontMelt.Point.Create(x, FrameHeight - y - 1));
                    MonoGameFrameColors[(y * FrameWidth) + x] = new Microsoft.Xna.Framework.Color(DontMeltColor.r / 255f, DontMeltColor.g / 255f, DontMeltColor.b / 255f);
                }
            }
            Texture2D MonoGameFrame = new Texture2D(GraphicsDevice, FrameWidth, FrameHeight);
            MonoGameFrame.SetData(MonoGameFrameColors);
            SpriteBatch FrameSpriteBatch = new SpriteBatch(GraphicsDevice);
            FrameSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null);
            FrameSpriteBatch.Draw(MonoGameFrame, new Microsoft.Xna.Framework.Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Microsoft.Xna.Framework.Color.White);
            FrameSpriteBatch.End();
        }
        base.Draw(gameTime);
    }
}