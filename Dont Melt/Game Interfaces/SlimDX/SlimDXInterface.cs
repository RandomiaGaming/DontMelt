using System;
using System.Windows.Forms;
using SlimDX;
using SlimDX.Direct3D11;
using SlimDX.DXGI;
using SlimDX.Windows;
using Device = SlimDX.Direct3D11.Device;
using Resource = SlimDX.Direct3D11.Resource;
using System.Collections.Generic;

public sealed class SlimDXInterface
{
    [STAThread]
    public static void Main()
    {
        DontMelt.Game game = new DontMelt.Game();
        _ = game.Initialize(new DontMelt.InitializeInputPacket(null));
        RenderForm form = new RenderForm();
        MessagePump.Run(form, () =>
        {
            DontMelt.UpdateReturnPacket rPacket = game.Update(new DontMelt.InputPacket(null, null));
            System.Drawing.Bitmap frame = new System.Drawing.Bitmap(rPacket.frameBuffer.width, rPacket.frameBuffer.height);
            for (int x = 0; x < rPacket.frameBuffer.width; x++)
            {
                for (int y = 0; y < rPacket.frameBuffer.height; y++)
                {
                    DontMelt.Color pixelColor = rPacket.frameBuffer.GetPixelUnsafe(x, y);
                    frame.SetPixel(x, y, System.Drawing.Color.FromArgb(pixelColor.r, pixelColor.g, pixelColor.b));
                }
            }
        });
    }
}