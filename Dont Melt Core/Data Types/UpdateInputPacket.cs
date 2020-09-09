using System;

namespace DontMelt
{
    public class UpdateInputPacket
    {
        public TimeSpan FrameDeltaTime;
        public TimeSpan TotalDeltaTime;
        public double FrameRate;
    }
}
