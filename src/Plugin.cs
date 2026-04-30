using BepInEx;
using HarmonyLib;


namespace ConfigurationManagerLocalizer
{
    [BepInPlugin("net.okanon.configurationmanager.localizer", "Configuration Manager Localizer", "1.0.0")]
    [BepInDependency("com.bepis.bepinex.configurationmanager")]
    public class ConfigurationManagerLocalizer : BaseUnityPlugin
    {
        private void Awake()
        {
            var harmony = new Harmony("net.okanon.configurationmanager.localizer");
            harmony.PatchAll();
            Logger.LogInfo("Configuration Manager Localizer Loaded");
        }
    }
}
