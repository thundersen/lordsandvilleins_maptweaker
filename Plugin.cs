using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;

namespace WorldTweaker
{
    [BepInPlugin("com.thundersen.lordsandvilleins.worldtweaker", "World Tweaker", "0.0.0.1")]
    [BepInProcess("Lords and Villeins.exe")]
    public class Plugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log;
        internal static WorldTweakerConfig ModConfig;

        private void Awake()
        {
            Log = base.Logger;

            ModConfig = new WorldTweakerConfig(Config);

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());

            Logger.LogInfo($"Plugin com.thundersen.lordsandvilleins.worldtweaker is loaded!");
        }
    }
}
