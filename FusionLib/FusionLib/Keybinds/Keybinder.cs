using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FusionLib.Keybinds
{
    /// <summary>
    /// Class to act as a central way of binding keys and handling configuration.
    /// </summary>
    public class Keybinder
    {
        private List<KeybindMeta> bindings;

        public Keybinder()
        {
            this.bindings = new List<KeybindMeta>();
        }

        public void AddBind(Keys key, Object caller, String functionName, params object[] parameters)
        {
            bindings.Add(new KeybindMeta(key, caller, functionName, parameters));
        }

        public void RemoveBind(Keys key, Object caller)
        {
            List<KeybindMeta> result = (from binding in bindings
                                        where binding.key.Equals(key) &&
                                              binding.caller.Equals(caller)
                                        select binding).ToList<KeybindMeta>();
            foreach (KeybindMeta meta in result)
            {
                bindings.Remove(meta);
            }
            result.Clear();
        }

        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            List<KeybindMeta> calls = (from binding in bindings
                                       where keyboardState.GetPressedKeys().Contains(binding.key)
                                       select binding).ToList<KeybindMeta>();
            foreach (KeybindMeta meta in calls)
            {
                meta.Execute();
            }
        }

        public static void GenerateConfig()
        {
            throw new NotImplementedException();
        }

        public static void LoadConfig()
        {
            throw new NotImplementedException();
        }
    }
}