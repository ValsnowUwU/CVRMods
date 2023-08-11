using MelonLoader;
using System;
using System.Linq;
using UnityEngine;
using BTKUILib.UIObjects;
using BTKUILib;
using System.Reflection;

namespace BeautifyParticles
{
    public class Main : MelonMod
    {
        //  Root page of mod
        private Page page;

        private MelonPreferences_Category preferencesCategory;
        private MelonPreferences_Entry<bool> autoBeautify;
        private MelonPreferences_Entry<bool> setFacing;
        private MelonPreferences_Entry<bool> setRoll;
        private MelonPreferences_Entry<bool> onlyView;

        public override void OnInitializeMelon() //Sets up UI Expansion Kit
        {
            //  Create preferences category
            preferencesCategory = MelonPreferences.CreateCategory("VRParticles", "Beautify Particles");

            //  Create entries
            autoBeautify = preferencesCategory.CreateEntry<bool>("autoBeautify", true);
            setFacing = preferencesCategory.CreateEntry<bool>("setFacing", true);
            setRoll = preferencesCategory.CreateEntry<bool>("setRoll", true);
            onlyView = preferencesCategory.CreateEntry<bool>("onlyView", true);


            //  Check if BTKUILib is installed
            if (RegisteredMelons.Any(it => it.Info.Name == "BTKUILib"))
            {
                LoggerInstance.Msg("Found BTKUILib");

                QuickMenuAPI.PrepareIcon("BeautifyParticles", "BeautifyParticlesIcon", Assembly.GetExecutingAssembly().GetManifestResourceStream("BeautifyParticles.Icon.png"));
                //  Initialise UI
                page = new Page("BeautifyParticles", "Beautify Particles", true, "BeautifyParticlesIcon");
                page.MenuTitle = "Beautify Particles";
                page.MenuSubtitle = "Improve the look of particles";

                //  Basic settings
                var basicSettings = page.AddCategory("Basic Settings");

                var manualSet = basicSettings.AddButton("Beautify Particles!", "BeautifyParticlesIcon", "Make them pretty");
                manualSet.OnPress = SetParticles;
                
                var autoBeautifyToggle = basicSettings.AddToggle("Auto-Beautify Worlds", "Automatically beautify worlds on join", autoBeautify.Value);
                autoBeautifyToggle.OnValueUpdated += b =>
                {
                    autoBeautify.Value = b;
                };

                //  Advanced settings
                var advancedSettings = page.AddCategory("Advanced Settings");

                var setFacingToggle = advancedSettings.AddToggle("Set to face camera", "Default: true", setFacing.Value);
                setFacingToggle.OnValueUpdated += b =>
                {
                    setFacing.Value = b;
                };

                var setRollToggle = advancedSettings.AddToggle("Disallow roll", "Default: true", setRoll.Value);
                setRollToggle.OnValueUpdated += b =>
                {
                    setRoll.Value = b;
                };

                var onlyViewToggle = advancedSettings.AddToggle("Only when set to view", "Default: true", onlyView.Value);
                onlyViewToggle.OnValueUpdated += b =>
                {
                    onlyView.Value = b;
                };
            }
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (autoBeautify.Value) SetParticles();
        }

        public void SetParticles() // Beautifies particles.
        {
            var particleRenderers = Resources.FindObjectsOfTypeAll<ParticleSystemRenderer>();

            foreach (var r in particleRenderers)
            {
                //  If only updating particles set to view, and check if the particle is view
                if (onlyView.Value && r.alignment == ParticleSystemRenderSpace.View)
                {
                    //  Facing if true, otherwise keep value
                    r.alignment = setFacing.Value ? ParticleSystemRenderSpace.Facing : r.alignment;

                    //  Don't allow roll if true, otherwise keep value
                    r.allowRoll = setRoll.Value ? false : r.allowRoll;

                    continue;
                }

                 //  Facing if true, otherwise keep value
                 r.alignment = setFacing.Value ? ParticleSystemRenderSpace.Facing : r.alignment;
                
                 //  Don't allow roll if true, otherwise keep value
                 r.allowRoll = setRoll.Value ? false : r.allowRoll;
            }
            LoggerInstance.Msg("**** Made " + particleRenderers.Length + " particles pretty ****");
        }
    }
}