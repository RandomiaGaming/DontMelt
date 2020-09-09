using System.Collections.Generic;
namespace DontMelt
{
    public enum KeyCode { None, Backspace, Delete, Tab, Clear, Return, Pause, Escape, Space, Keypad0, Keypad1, Keypad2, Keypad3, Keypad4, Keypad5, Keypad6, Keypad7, Keypad8, Keypad9, KeypadPeriod, KeypadDivide, KeypadMultiply, KeypadMinus, KeypadPlus, KeypadEnter, KeypadEquals, UpArrow, DownArrow, RightArrow, LeftArrow, Insert, Home, End, PageUp, PageDown, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, F13, F14, F15, Alpha0, Alpha1, Alpha2, Alpha3, Alpha4, Alpha5, Alpha6, Alpha7, Alpha8, Alpha9, Exclaim, DoubleQuote, Hash, Dollar, Percent, Ampersand, Quote, LeftParen, RightParen, Asterisk, Plus, Comma, Minus, Period, Slash, Colon, Semicolon, Less, Equals, Greater, Question, At, LeftBracket, Backslash, RightBracket, Caret, Underscore, BackQuote, A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z, LeftCurlyBracket, Pipe, RightCurlyBracket, Tilde, Numlock, CapsLock, ScrollLock, RightShift, LeftShift, RightControl, LeftControl, RightAlt, LeftAlt, LeftCommand, LeftApple, LeftWindows, RightCommand, RightApple, RightWindows, AltGr, Help, Print, SysReq, Break, Menu, Mouse0, Mouse1, Mouse2, Mouse3, Mouse4, Mouse5, Mouse6, JoystickButton0, JoystickButton1, JoystickButton2, JoystickButton3, JoystickButton4, JoystickButton5, JoystickButton6, JoystickButton7, JoystickButton8, JoystickButton9, JoystickButton10, JoystickButton11, JoystickButton12, JoystickButton13, JoystickButton14, JoystickButton15, JoystickButton16, JoystickButton17, JoystickButton18, JoystickButton19 }
    public sealed class InputPacket
    {
        public KeyCode[] heldKeys { get; private set; }
        public KeyCode[] downKeys { get; private set; }
        public KeyCode[] upKeys { get; private set; }
        public Point mousePosition { get; private set; }
        public Point keyDirection { get; private set; }
        private InputPacket() { }
        public static InputPacket Create(KeyCode[] heldKeys, KeyCode[] downKeys, KeyCode[] upKeys, Point mousePosition, Point keyDirection)
        {
            InputPacket Output = new InputPacket();
            Output.heldKeys = new List<KeyCode>(heldKeys).ToArray();
            Output.downKeys = new List<KeyCode>(downKeys).ToArray();
            Output.upKeys = new List<KeyCode>(upKeys).ToArray();
            Output.mousePosition = mousePosition.Clone();
            Output.keyDirection = keyDirection.Clone();
            return Output;
        }
        public InputPacket Clone()
        {
            InputPacket Output = new InputPacket();
            Output.heldKeys = new List<KeyCode>(heldKeys).ToArray();
            Output.downKeys = new List<KeyCode>(downKeys).ToArray();
            Output.upKeys = new List<KeyCode>(upKeys).ToArray();
            Output.mousePosition = mousePosition.Clone();
            Output.keyDirection = keyDirection.Clone();
            return Output;
        }
    }
}