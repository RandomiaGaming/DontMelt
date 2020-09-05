using DontMelt.Internal;
using DontMelt;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
public class InterfaceGame : Game
{
    private GraphicsDeviceManager GDM;
    public InterfaceGame()
    {
        Content.RootDirectory = "Dont Melt Core/Assets";
        GDM = new GraphicsDeviceManager(this);
        GDM.IsFullScreen = false;
    }
    protected override void Initialize()
    {
        DontMeltKernal.Initialize();
        base.Initialize();
    }
    protected override void Update(GameTime gameTime)
    {
        DontMeltKernal.PhysicsUpdate();
        DontMeltKernal.Update();
        base.Update(gameTime);
    }
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Microsoft.Xna.Framework.Color(1f, 0f, 1f));
        DontMelt.Texture EpsilonFrame = DontMeltKernal.Render();
        List<Microsoft.Xna.Framework.Color> FrameColors = new List<Microsoft.Xna.Framework.Color>();
        for (int x = 0; x < EpsilonFrame.Get_Width(); x++)
        {
            for (int y = 0; y < EpsilonFrame.Get_Height(); y++)
            {
                DontMelt.Color EpsilonPixelColor = EpsilonFrame.Get_Pixel(DontMelt.Point.Create(x, y));
                FrameColors.Add(new Microsoft.Xna.Framework.Color(EpsilonPixelColor.r / 255f, EpsilonPixelColor.g / 255f, EpsilonPixelColor.b / 255f));
            }
        }
        Texture2D FrameTexture = new Texture2D(GraphicsDevice, EpsilonFrame.Get_Width(), EpsilonFrame.Get_Height());
        FrameTexture.SetData(FrameColors.ToArray());
        SpriteBatch FrameSpriteBatch = new SpriteBatch(GraphicsDevice);
        FrameSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null);
        FrameSpriteBatch.Draw(FrameTexture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Microsoft.Xna.Framework.Color.White);
        FrameSpriteBatch.End();
        base.Draw(gameTime);
    }
}