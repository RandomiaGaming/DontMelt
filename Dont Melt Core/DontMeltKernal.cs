namespace DontMelt.Internal
{
    internal static class DontMeltKernal
    {
        private static Point Position = Point.Create(0, 0);
        private static long LastMoveTicks = 0;
        public static UpdateOutputPacket Update(UpdateInputPacket Packet)
        {
            UpdateOutputPacket Output = new UpdateOutputPacket();
            Texture Frame = Texture.Create(256, 144, Color.Create(255, 255, 100));
            if (Packet.TotalDeltaTime.Ticks - LastMoveTicks > 100000)
            {
                if (Position.x < Frame.Get_Width() - 1)
                {
                    Position.x++;
                }
                else
                {
                    Position.x = 0;
                    if (Position.y < Frame.Get_Height() - 1)
                    {
                        Position.y++;
                    }
                    else
                    {
                        Position.y = 0;
                    }
                }
                LastMoveTicks = Packet.TotalDeltaTime.Ticks;
            }
            Frame.Set_Pixel(Position.Clone(), Color.Create(0, 0, 0));
            Output.Frame = Frame;
            return Output;
        }
    }
}