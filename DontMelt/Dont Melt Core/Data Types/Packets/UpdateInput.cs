using System;
namespace DontMelt
{
    public sealed class UpdatePacket
    {
        public readonly TimeSpan deltaTime;
        public readonly TimeSpan elapsedTime;
        public readonly KeyboardInput[] keyboardInputPackets = new KeyboardInput[0];
        public readonly MouseInputPacket[] mouseInputPackets = new MouseInputPacket[0];
        private UpdatePacket() { }
        public UpdatePacket(TimeSpan deltaTime, TimeSpan elapsedTime, KeyboardInput[] keyboardInputPackets, MouseInputPacket[] mouseInputPackets)
        {
            this.deltaTime = deltaTime;
            this.elapsedTime = elapsedTime;
            this.mouseInputPackets = mouseInputPackets;
            this.keyboardInputPackets = keyboardInputPackets;
        }
    }
}