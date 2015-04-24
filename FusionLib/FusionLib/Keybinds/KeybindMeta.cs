using Microsoft.Xna.Framework.Input;
using System;
using System.Reflection;

namespace FusionLib.Keybinds
{
    internal class KeybindMeta
    {
        //TODO: remove public
        public Keys key;

        public Object caller;
        private String functionName;
        private object[] parameters;

        public KeybindMeta(Keys key, Object caller, String functionName, params object[] parameters)
        {
            this.key = key;
            this.caller = caller;
            this.functionName = functionName;
            this.parameters = parameters;
        }

        public void Execute()
        {
            MethodInfo m = caller.GetType().GetMethod(functionName);
            m.Invoke(caller, parameters);
        }
    }
}