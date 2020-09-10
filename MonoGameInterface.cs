using EpsilonEngine;
using EpsilonEngine.Internal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

public class MonoGameInterface : Game
{
    private GraphicsDeviceManager graphicsDeviceManager = null;
    private OutputPacket lastPacket = null;
    public MonoGameInterface()
    {
        Content.RootDirectory = "Dont Melt Core/Assets";
        graphicsDeviceManager = new GraphicsDeviceManager(this);
    }
    protected override void Initialize()
    {
        graphicsDeviceManager.IsFullScreen = false;
        graphicsDeviceManager.SynchronizeWithVerticalRetrace = false;
        graphicsDeviceManager.ApplyChanges();
        Window.AllowUserResizing = true;
        IsMouseVisible = true;
        IsFixedTimeStep = false;
        DontMeltKernal.Initialize();
        base.Initialize();
    }
    protected override void Update(GameTime gameTime)
    {
        TimeSpan deltaTime = new TimeSpan(gameTime.ElapsedGameTime.Ticks);
        TimeSpan elapsedTime = new TimeSpan(gameTime.TotalGameTime.Ticks);
        MouseState mouseState = Mouse.GetState();
        double mouseX = (double)mouseState.X / GraphicsDevice.Viewport.Width * 256;
        double mouseY = 144 - ((double)mouseState.Y / GraphicsDevice.Viewport.Height * 144);
        EpsilonEngine.Point mousePosition = EpsilonEngine.Point.Create((int)mouseX, (int)mouseY);
        EpsilonEngine.Point keyDirection = EpsilonEngine.Point.Create(0, 0);
        if (Keyboard.GetState().IsKeyDown(Keys.W))
        {
            keyDirection.y = 1;
        }
        else if (Keyboard.GetState().IsKeyDown(Keys.S))
        {
            keyDirection.y = -1;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.D))
        {
            keyDirection.x = 1;
        }
        else if (Keyboard.GetState().IsKeyDown(Keys.A))
        {
            keyDirection.x = -1;
        }
        InputPacket inputPacket = InputPacket.Create(new KeyCode[0], new KeyCode[0], new KeyCode[0], mousePosition, keyDirection);
        UpdatePacket Packet = UpdatePacket.Create(deltaTime, elapsedTime, inputPacket);
        lastPacket = DontMeltKernal.Update(Packet);
        if (lastPacket.requestExit)
        {
            Exit();
        }
        base.Update(gameTime);
    }
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Microsoft.Xna.Framework.Color(1f, 1f, 0.58823529411f));
        if (lastPacket != null)
        {
            int FrameWidth = lastPacket.frameTexture.width;
            int FrameHeight = lastPacket.frameTexture.height;
            var MonoGameFrameColors = new Microsoft.Xna.Framework.Color[FrameHeight * FrameWidth];
            for (int x = 0; x < FrameWidth; x++)
            {
                for (int y = 0; y < FrameHeight; y++)
                {
                    EpsilonEngine.Color DontMeltColor = lastPacket.frameTexture.GetPixel(EpsilonEngine.Point.Create(x, FrameHeight - y - 1));
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