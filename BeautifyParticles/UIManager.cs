using BTKUILib;
using BTKUILib.UIObjects;
using MelonLoader;
using System;
using System.Reflection;

namespace BeautifyParticles
{
    public static class UIManager
    {
        //  Required to avoid errors if dependency is not installed ugh
        public static void setupBTKUILib(Main m)
        {
            QuickMenuAPI.PrepareIcon("BeautifyParticles", "BeautifyParticlesIcon", Assembly.GetExecutingAssembly().GetManifestResourceStream("BeautifyParticles.Icon.png"));
            //  Initialise UI
            var page = new Page("BeautifyParticles", "Beautify Particles", true, "BeautifyParticlesIcon");
            page.MenuTitle = "Beautify Particles";
            page.MenuSubtitle = "Improve the look of particles";

            //  Basic settings
            var basicSettings = page.AddCategory("Basic Settings");

            var manualSet = basicSettings.AddButton("Beautify Particles!", "BeautifyParticlesIcon", "Make them pretty");
            manualSet.OnPress = m.SetParticles;

            var autoBeautifyToggle = basicSettings.AddToggle("Auto-Beautify Worlds", "Automatically beautify worlds on join", m.autoBeautify.Value);
            autoBeautifyToggle.OnValueUpdated += b =>
            {
                m.autoBeautify.Value = b;
            };

            //  Advanced settings
            var advancedSettings = page.AddCategory("Advanced Settings");

            var setFacingToggle = advancedSettings.AddToggle("Set to face camera", "Default: true", m.setFacing.Value);
            setFacingToggle.OnValueUpdated += b =>
            {
                m.setFacing.Value = b;
            };

            var setRollToggle = advancedSettings.AddToggle("Disallow roll", "Default: true", m.setRoll.Value);
            setRollToggle.OnValueUpdated += b =>
            {
                m.setRoll.Value = b;
            };

            var onlyViewToggle = advancedSettings.AddToggle("Only when set to view", "Default: true", m.onlyView.Value);
            onlyViewToggle.OnValueUpdated += b =>
            {
                m.onlyView.Value = b;
            };
        }
    }
}
