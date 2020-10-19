using System;
namespace EpsilonEngine
{
    public sealed class UpdateInputPacket
    {
        public TimeSpan deltaTime;
        public TimeSpan elapsedTime;
        //public KeyboardInputPacket[] keyboardInputPackets = new KeyboardInputPacket[0];
        //public MouseInputPacket[] mouseInputPackets = new MouseInputPacket[0];
    }
}