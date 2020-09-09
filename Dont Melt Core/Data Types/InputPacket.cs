
using System;
namespace DontMelt
{
    public sealed class InputPacket
    {
        public TimeSpan DeltaTime { get; private set; }
        public TimeSpan ElapsedTime { get; private set; }
        public double FrameRate { get; private set; }
        private InputPacket() { }
        public static InputPacket Create()
        {
            InputPacket Output = new InputPacket();
            Output.DeltaTime = new TimeSpan(0);
            Output.ElapsedTime = new TimeSpan(0);
            Output.FrameRate = 0;
            return Output;
        }
        public InputPacket Clone()
        {
            InputPacket Output = new InputPacket();
            Output.DeltaTime = new TimeSpan(DeltaTime.Ticks);
            Output.ElapsedTime = new TimeSpan(ElapsedTime.Ticks);
            Output.FrameRate = FrameRate;
            return Output;
        }
    }
}