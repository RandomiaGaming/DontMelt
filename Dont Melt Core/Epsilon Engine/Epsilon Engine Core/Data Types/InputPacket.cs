using System.Collections.Generic;
namespace EpsilonEngine
{
    public enum KeyCode { None, Mouse0, Mouse1, Mouse2, W, A, S, D, Space, Shift, }
    public sealed class InputPacket
    {
        public KeyCode[] heldKeys { get; private set; }
        public double mouseScroll { get; private set; }
        public Point mousePosition { get; private set; }
        private InputPacket() { }
        public static InputPacket Create()
        {
            InputPacket Output = new InputPacket();
            Output.heldKeys = new KeyCode[0];
            Output.mousePosition = Point.Create(0, 0);
            return Output;
        }
        public static InputPacket Create(KeyCode[] heldKeys, Point mousePosition, double mouseScroll)
        {
            InputPacket output = new InputPacket();
            output.heldKeys = new List<KeyCode>(heldKeys).ToArray();
            output.mousePosition = mousePosition.Clone();
            output.mouseScroll = mouseScroll;
            return output;
        }
        public InputPacket Clone()
        {
            InputPacket output = new InputPacket();
            output.heldKeys = new List<KeyCode>(heldKeys).ToArray();
            output.mousePosition = mousePosition.Clone();
            mouseScroll = 0;
            return output;
        }
        public bool KeyDown(KeyCode keyCode)
        {
            for (int i = 0; i < heldKeys.Length; i++)
            {
                if (heldKeys[i] == keyCode)
                {
                    return true;
                }
            }
            return false;
        }
    }
}