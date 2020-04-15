using System;
using HarmonyLib;
using UnityEngine;
using System.Reflection;

namespace CoreLoader.Core
{
    [HarmonyPatch(typeof(Console), "Init", new Type[] { typeof(IConsoleUI) })]
    class ConsolePatcher
    {
        [HarmonyPostfix]
        static void addMoarCommands(Console __instance, IConsoleUI consoleUI)
        {
            MethodInfo addCommands = AccessTools.Method(typeof(Console), "AddCommands", new Type[] { typeof(Type) });
            addCommands.Invoke(__instance, new object[] { typeof(CommandsPatcher) });
        }
    }
}