namespace EpsilonEngine
{
    public sealed class MouseInputPacket
    {
        public Vector mousePosition = Vector.Zero;
        public int[] pressedButtons = new int[0];
        public bool leftButtonPressed = false;
        public bool rightButtonPressed = false;
        public bool scrollWheelPressed = false;
        public int buttonCount = 0;
        public double scrollWheelDelta = 0;
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
