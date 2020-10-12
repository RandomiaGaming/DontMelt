namespace DontMelt
{
    public sealed class MouseInputPacket
    {
        public readonly Vector mousePosition = Vector.Zero;
        public readonly int[] pressedButtons = new int[0];
        public readonly bool leftButtonPressed = false;
        public readonly bool rightButtonPressed = false;
        public readonly bool scrollWheelPressed = false;
        public readonly int buttonCount = 0;
        public readonly double scrollWheelDelta = 0;
        public bool ButtonDown(int buttonID)
        {
            foreach (int b in pressedButtons)
            {
                if (b == buttonID)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
