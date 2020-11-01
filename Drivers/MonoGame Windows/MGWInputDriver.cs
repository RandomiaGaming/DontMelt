using EpsilonEngine;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
namespace Epsilon.Drivers.MGW
{
    public class MGWInputDriver : InputDriver
    {
        private bool capsLockState = false;
        private bool numLockState = false;
        private List<KeyCode> pressedKeys = new List<KeyCode>();
        public MGWInputDriver(Game game) : base(game)
        {

        }
        public override bool GetCapsLockState()
        {
            return capsLockState;
        }

        public override bool GetNumLockState()
        {
            return numLockState;
        }

        public override void Initialize()
        {

        }
        public override void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            List<KeyCode> pressedKeys = new List<KeyCode>();
            foreach (Keys key in keyboardState.GetPressedKeys())
            {
                switch (key)
                {
                    case Keys.A:
                        pressedKeys.Add(KeyCode.A);
                        break;
                    case Keys.D:
                        pressedKeys.Add(KeyCode.D);
                        break;
                    case Keys.Space:
                        pressedKeys.Add(KeyCode.Space);
                        break;
                }
            }
            this.pressedKeys = pressedKeys;
            capsLockState = keyboardState.CapsLock;
            numLockState = keyboardState.NumLock;
        }
        public override List<KeyCode> GetPressedKeys()
        {
            return pressedKeys;
        }
        public override bool IsKeyPressed(KeyCode targetKeyCode)
        {
            for (int i = 0; i < pressedKeys.Count; i++)
            {
                if (pressedKeys[i] == targetKeyCode)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
