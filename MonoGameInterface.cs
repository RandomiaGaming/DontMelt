using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
public sealed class MonoGameInterface : Game
{
    private readonly string[] args = null;
    private EpsilonEngine.ReturnPacket packetBuffer = null;
    private readonly EpsilonEngine.Game epsilonGame = new EpsilonEngine.Game();
    public MonoGameInterface(string[] args)
    {
        GraphicsDeviceManager graphics = new GraphicsDeviceManager(this)
        {
            SynchronizeWithVerticalRetrace = false
        };
        Window.AllowUserResizing = true;
        Window.AllowAltF4 = true;
        Window.IsBorderless = false;
        Window.Title = "Dont Melt - RandomiaGaming";
        IsMouseVisible = true;
        IsFixedTimeStep = false;
        this.args = args;
        epsilonGame = new EpsilonEngine.Game();
    }
    protected sealed override void Initialize()
    {
        EpsilonEngine.InitializationPacket packet = new EpsilonEngine.InitializationPacket
        {
            args = args,
            platform = EpsilonEngine.Platform.Windows
        };
        epsilonGame.Initialize(packet);
        base.Initialize();
    }
    protected sealed override void Update(GameTime gameTime)
    {
        List<EpsilonEngine.Key> dmPressedKeys = new List<EpsilonEngine.Key>();
        Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();
        foreach (Keys key in pressedKeys)
        {
            switch (key)
            {
                case Keys.A:
                    dmPressedKeys.Add(EpsilonEngine.Key.A);
                    break;
                case Keys.B:
                    dmPressedKeys.Add(EpsilonEngine.Key.B);
                    break;
            }
        }
        EpsilonEngine.UpdatePacket packet = new EpsilonEngine.UpdatePacket
        {
            pressedKeys = dmPressedKeys
        };
        packetBuffer = epsilonGame.Update(packet);
        if (packetBuffer.requestQuit)
        {
            Exit();
        }
        base.Update(gameTime);
    }
    protected sealed override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        if (packetBuffer != null)
        {
            //Convert the frameTexture to a Texture2D which MonoGame can read.
            Texture2D frame = new Texture2D(GraphicsDevice, packetBuffer.frameTexture.width, packetBuffer.frameTexture.height);
            Color[] data = new Color[packetBuffer.frameTexture.width * packetBuffer.frameTexture.height];
            int i = 0;
            for (int y = 0; y < packetBuffer.frameTexture.height; y++)
            {
                for (int x = 0; x < packetBuffer.frameTexture.width; x++)
                {
                    EpsilonEngine.Color pixelColor = packetBuffer.frameTexture.GetPixelUnsafe((ushort)x, (ushort)(packetBuffer.frameTexture.height - y - 1));
                    data[i] = new Color(pixelColor.r, pixelColor.g, pixelColor.b);
                    i++;
                }
            }
            frame.SetData(data);
            //Render the frame in MonoGame.
            SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(frame, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            spriteBatch.End();
        }
        //Draw the base.
        base.Draw(gameTime);
    }
}