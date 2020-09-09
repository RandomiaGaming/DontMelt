using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
public class InterfaceGame : Game
{
    private GraphicsDeviceManager GDM;
    public InterfaceGame()
    {
        Content.RootDirectory = "Dont Melt Core/Assets";
        GDM = new GraphicsDeviceManager(this);
        GDM.IsFullScreen = false ;
        Window.AllowUserResizing = true;
        this.IsMouseVisible = true;
    }
    protected override void Initialize()
    {
        base.Initialize();
    }
    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(1f, 0f, 1f));

        DontMelt.UpdateInputPacket DontMeltUpdateInputPacket = new DontMelt.UpdateInputPacket();
        DontMeltUpdateInputPacket.FrameDeltaTime = new System.TimeSpan(gameTime.ElapsedGameTime.Ticks);
        DontMeltUpdateInputPacket.TotalDeltaTime = new System.TimeSpan(gameTime.TotalGameTime.Ticks);
        DontMelt.UpdateOutputPacket DontMeltUpdateOutputPacket = DontMelt.Internal.DontMeltKernal.Update(DontMeltUpdateInputPacket);
        DontMelt.Texture DontMeltFrame = DontMeltUpdateOutputPacket.Frame.Clone();
        Color[] MonoGameFrameColors = new Color[DontMeltFrame.Get_Width() * DontMeltFrame.Get_Height()];
        int DontMeltFrameWidth = DontMeltFrame.Get_Width();
        int DontMeltFrameHeight = DontMeltFrame.Get_Height();
        for (int x = 0; x < DontMeltFrameWidth; x++)
        {
            for (int y = 0; y < DontMeltFrameHeight; y++)
            {
                DontMelt.Color DontMeltColor = DontMeltFrame.Get_Pixel(DontMelt.Point.Create(x, DontMeltFrameHeight - y - 1));
                MonoGameFrameColors[(y * DontMeltFrameWidth) + x] = new Color(DontMeltColor.r / 255f, DontMeltColor.g / 255f, DontMeltColor.b / 255f);
            }
        }
        Texture2D MonoGameFrame = new Texture2D(GraphicsDevice, DontMeltFrameWidth, DontMeltFrameHeight);
        MonoGameFrame.SetData(MonoGameFrameColors);
        SpriteBatch FrameSpriteBatch = new SpriteBatch(GraphicsDevice);
        FrameSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null);
        FrameSpriteBatch.Draw(MonoGameFrame, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
        FrameSpriteBatch.End();
        base.Draw(gameTime);
    }
}