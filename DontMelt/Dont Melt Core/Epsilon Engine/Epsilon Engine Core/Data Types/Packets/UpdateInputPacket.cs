using System;
namespace DontMelt
{
    public sealed class UpdateInputPacket
    {
        public readonly TimeSpan deltaTime;
        public readonly TimeSpan elapsedTime;
        public readonly KeyboardInputPacket[] keyboardInputPackets = new KeyboardInputPacket[0];
        public readonly MouseInputPacket[] mouseInputPackets = new MouseInputPacket[0];
    }
}