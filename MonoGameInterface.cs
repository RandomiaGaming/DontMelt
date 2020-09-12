using EpsilonEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

public class MonoGameInterface : Game
{
    private readonly GraphicsDeviceManager graphicsDeviceManager = null;
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
        EpsilonKernal.Initialize(InitializationPacket.Create());
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
        InputPacket inputPacket = InputPacket.Create(GetPressedKeys(), mousePosition, 0);
        UpdatePacket Packet = UpdatePacket.Create(deltaTime, elapsedTime, inputPacket);
        lastPacket = EpsilonKernal.Update(Packet);
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
    private KeyCode[] GetPressedKeys()
    {
        List<KeyCode> output = new List<KeyCode>();
        foreach (Keys key in Keyboard.GetState().GetPressedKeys())
        {
            if (key == Keys.W)
            {
                output.Add(KeyCode.W);
            }
            else if (key == Keys.A)
            {
                output.Add(KeyCode.A);
            }
            else if (key == Keys.S)
            {
                output.Add(KeyCode.S);
            }
            else if (key == Keys.D)
            {
                output.Add(KeyCode.D);
            }
            else if (key == Keys.Space)
            {
                output.Add(KeyCode.Space);
            }
            else if (key == Keys.LeftShift || key == Keys.RightShift)
            {
                output.Add(KeyCode.Shift);
            }
        }
        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
        {
            output.Add(KeyCode.Mouse0);
        }
        if (Mouse.GetState().RightButton == ButtonState.Pressed)
        {
            output.Add(KeyCode.Mouse2);
        }
        if (Mouse.GetState().MiddleButton == ButtonState.Pressed)
        {
            output.Add(KeyCode.Mouse1);
        }
        return output.ToArray();
    }
}