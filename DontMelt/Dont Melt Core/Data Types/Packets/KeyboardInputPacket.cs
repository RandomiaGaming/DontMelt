﻿namespace DontMelt
{
    public enum Key { A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z, NumPad0, NumPad1, NumPad2, NumPad3, NumPad4, NumPad5, NumPad6, NumPad7, NumPad8, NumPad9, NumPadEnter, NumPadPlus, NumPadMinus, NumLock, NumPadTimes, NumPadDivide, NumPadPoint, LeftShift, RightShift, LeftControl, RightControl, LeftAlt, RightAlt, LeftWindows, RightWindows, Function, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12, Backspace, Delete, ScrollLock, Escape, Tab, Tilda, Space, PrintScreen, Insert, Home, PageUp, PageDown, End, Backslash, Slash, Comma, Period, Minus, Plus, Help }
    public sealed class KeyboardInput
    {
        public readonly Key[] pressedKeys;
        public readonly bool numLockEnabled;
        public readonly bool capsLockEnabled;
        public readonly bool scrollLockEnabled;
    }
}