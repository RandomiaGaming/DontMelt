namespace EpsilonEngine
{
    public sealed class InputPacket
    {
        public readonly KeyboardState keyboardState = null;
        public readonly MouseState mouseState = null;
        public InputPacket(KeyboardState keyboardState, MouseState mouseState)
        {
            this.keyboardState = keyboardState;
            this.mouseState = mouseState;
        }
    }
}
