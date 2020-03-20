using UnityEngine;
using System.Collections;
using System;

namespace CatAstropheGames
{
    public static class ActionExtensions
    {
        public static bool SafeInvoke(this Action action)
        {
            if (action != null)
            {
                action();
                return true;
            }
            return false;
        }

        public static bool SafeInvoke<T>(this Action<T> action, T argument)
        {
            if (action != null)
            {
                action(argument);
                return true;
            }
            return false;
        }
    }
}