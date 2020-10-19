using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

public class MonoGameInterface : Game
{
    public GraphicsDeviceManager graphics;
    public Random RNG = new Random();
    public SoundEffect soundBuffer = null;
    public Texture2D frameBuffer = null;
    public MonoGameInterface()
    {
        graphics = new GraphicsDeviceManager(this);
        graphics.SynchronizeWithVerticalRetrace = false;
        IsMouseVisible = true;
    }
    protected override void Initialize()
    {
        base.Initialize();
    }
    protected override void Update(GameTime gameTime)
    {
        EpsilonEngine.UpdateInputPacket inputPacket = new EpsilonEngine.UpdateInputPacket();
        inputPacket.deltaTime = gameTime.ElapsedGameTime;
        inputPacket.elapsedTime = gameTime.TotalGameTime;
        EpsilonEngine.UpdateOutputPacket outputPacket = EpsilonEngine.EpsilonKernal.Update(inputPacket);
        GraphicsDevice.Clear(Color.Black);
        SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice);
        spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        Texture2D frame = new Texture2D(GraphicsDevice, outputPacket.frame.width, outputPacket.frame.height);
        Color[] data = new Color[outputPacket.frame.width * outputPacket.frame.height];
        EpsilonEngine.Color[] eData = outputPacket.frame.GetData();
        for (int i = 0; i < outputPacket.frame.width * outputPacket.frame.height; i++)
        {
            data[i] = new Color(eData[i].r, eData[i].g, eData[i].b);
        }
        frame.SetData(data);
        spriteBatch.Draw(frame, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
        spriteBatch.End();
        base.Update(gameTime);
    }
}