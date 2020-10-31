using Epsilon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public sealed class MonoGameInterface : Game
{
    private int lastScrollWheelValue = 0;
    private EpsilonEngine.ReturnPacket packetBuffer = null;
    private readonly EpsilonEngine.Game epsilonGame = null;
    public MonoGameInterface(bool debugMode)
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
        IsFixedTimeStep = true;
        TargetElapsedTime = new TimeSpan(10000000 / 60);
        epsilonGame = new EpsilonGame(debugMode);
    }
    protected sealed override void Initialize()
    {
        epsilonGame.Initialize();
        base.Initialize();
    }
    protected sealed override void Update(GameTime gameTime)
    {
        List<EpsilonEngine.KeyCode> pressedKeys = new List<EpsilonEngine.KeyCode>();
        foreach (Keys key in Keyboard.GetState().GetPressedKeys())
        {
            switch (key)
            {
                case Keys.A:
                    pressedKeys.Add(EpsilonEngine.KeyCode.A);
                    break;
                case Keys.D:
                    pressedKeys.Add(EpsilonEngine.KeyCode.D);
                    break;
                case Keys.Space:
                    pressedKeys.Add(EpsilonEngine.KeyCode.Space);
                    break;
            }
        }
        EpsilonEngine.UpdatePacket packet = new EpsilonEngine.UpdatePacket
        {
            pressedKeys = pressedKeys,
            capsLock = Keyboard.GetState().CapsLock,
            numLock = Keyboard.GetState().NumLock,
            mousePosition = new EpsilonEngine.Point(Mouse.GetState().X, GraphicsDevice.Viewport.Height - Mouse.GetState().Y),
            scrollWheelDelta = (Mouse.GetState().ScrollWheelValue / 120) - lastScrollWheelValue,
        };
        packetBuffer = epsilonGame.Update(packet);
        if (packetBuffer.requestQuit)
        {
            Exit();
        }
        lastScrollWheelValue = Mouse.GetState().ScrollWheelValue / 120;
        base.Update(gameTime);
    }
    protected sealed override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        if (packetBuffer != null)
        {
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
            SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(frame, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            spriteBatch.End();
        }
        base.Draw(gameTime);
    }
}