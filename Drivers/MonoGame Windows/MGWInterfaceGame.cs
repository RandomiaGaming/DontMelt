using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public class MGWInterfaceGame : Game
{
    private static List<MGWInterfaceGame> MGWInterfaceGames = new List<MGWInterfaceGame>();
    public static MGWInterfaceGame GetFromEpsilonGame(EpsilonEngine.Game game)
    {
        if (MGWInterfaceGames == null)
        {
            MGWInterfaceGames = new List<MGWInterfaceGame>();
        }
        for (int i = 0; i < MGWInterfaceGames.Count; i++)
        {
            if (MGWInterfaceGames[i].epsilonGame == game)
            {
                return MGWInterfaceGames[i];
            }
        }
        return null;
    }

    public EpsilonEngine.Texture frameBuffer = new EpsilonEngine.Texture();
    private readonly EpsilonEngine.Game epsilonGame = null;
    public MGWInterfaceGame(EpsilonEngine.Game epsilonGame)
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
        this.epsilonGame = epsilonGame;

        if (MGWInterfaceGames == null)
        {
            MGWInterfaceGames = new List<MGWInterfaceGame>();
        }

        MGWInterfaceGames.Add(this);
    }
    protected override void Initialize()
    {
        base.Initialize();
    }
    protected override void Update(GameTime gameTime)
    {
        EpsilonEngine.ReturnPacket packet = epsilonGame.Update();
        if (packet.requestQuit)
        {
            Exit();
        }
        base.Update(gameTime);
    }
    protected override void Draw(GameTime gameTime)
    {
        if (frameBuffer.width > 0 && frameBuffer.height > 0)
        {
            Texture2D frame = new Texture2D(GraphicsDevice, frameBuffer.width, frameBuffer.height);
            Color[] data = new Color[frameBuffer.width * frameBuffer.height];
            int i = 0;
            for (int y = 0; y < frameBuffer.height; y++)
            {
                for (int x = 0; x < frameBuffer.width; x++)
                {
                    EpsilonEngine.Color pixelColor = frameBuffer.GetPixelUnsafe((ushort)x, (ushort)(frameBuffer.height - y - 1));
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