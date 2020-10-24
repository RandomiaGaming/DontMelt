//UNSAFE
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
public sealed class MonoGameInterface : Game
{
    private bool debug = false;
    private Epsilon.ReturnPacket rPacketBuffer = null;
    public MonoGameInterface(bool debug)
    {
        new GraphicsDeviceManager(this);
        Window.AllowUserResizing = true;
        Window.AllowAltF4 = true;
        Window.IsBorderless = false;
        Window.Title = "Epsilon - RandomiaGaming";
        IsMouseVisible = true;
        this.debug = debug;
    }
    protected sealed override void Initialize()
    {
        Epsilon.InitializationPacket iPacket = new Epsilon.InitializationPacket(debug);
        Epsilon.EpsilonKernal.Initialize(iPacket);
        base.Initialize();
    }
    protected sealed override void Update(GameTime gameTime)
    {
        List<Epsilon.Key> dmPressedKeys = new List<Epsilon.Key>();
        Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();
        foreach (Keys key in pressedKeys)
        {
            switch (key)
            {
                case Keys.A:
                    dmPressedKeys.Add(Epsilon.Key.A);
                    break;
                case Keys.B:
                    dmPressedKeys.Add(Epsilon.Key.B);
                    break;
            }
        }
        Epsilon.UpdatePacket updatePacket = new Epsilon.UpdatePacket(dmPressedKeys.ToArray());
        rPacketBuffer = Epsilon.EpsilonKernal.Update(updatePacket);
        if (rPacketBuffer.requestQuit)
        {
            Exit();
        }
        base.Update(gameTime);
    }
    protected sealed override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        if (rPacketBuffer != null)
        {
            //Convert the frame to a Texture2D which MonoGame can read.
            Texture2D frame = new Texture2D(GraphicsDevice, rPacketBuffer.frame.width, rPacketBuffer.frame.height);
            Color[] data = new Color[rPacketBuffer.frame.width * rPacketBuffer.frame.height];
            int i = 0;
            for (int y = 0; y < rPacketBuffer.frame.height; y++)
            {
                for (int x = 0; x < rPacketBuffer.frame.width; x++)
                {
                    Epsilon.Color pixelColor = rPacketBuffer.frame.GetPixelUnsafe(x, rPacketBuffer.frame.height - y - 1);
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