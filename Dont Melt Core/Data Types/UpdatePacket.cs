
using System;
namespace DontMelt
{
    public sealed class UpdatePacket
    {
        public TimeSpan deltaTime { get; private set; }
        public TimeSpan elapsedTime { get; private set; }
        public InputPacket inputPacket { get; private set; }
        private UpdatePacket() { }
        public static UpdatePacket Create(TimeSpan deltaTime, TimeSpan elapsedTime, InputPacket inputPacket)
        {
            UpdatePacket Output = new UpdatePacket();
            Output.deltaTime = new TimeSpan(deltaTime.Ticks);
            Output.elapsedTime = new TimeSpan(elapsedTime.Ticks);
            Output.inputPacket = inputPacket.Clone();
            return Output;
        }
        public UpdatePacket Clone()
        {
            UpdatePacket Output = new UpdatePacket();
            Output.deltaTime = new TimeSpan(deltaTime.Ticks);
            Output.elapsedTime = new TimeSpan(elapsedTime.Ticks);
            Output.inputPacket = inputPacket.Clone();
            return Output;
        }
    }
}