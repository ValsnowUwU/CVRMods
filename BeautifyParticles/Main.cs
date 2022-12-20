/*
    I'm new to modding, so please be kind.
 */
using MelonLoader;
using System.Linq;
using UIExpansionKit.API;
using UnityEngine;

namespace BeautifyParticles
{
    public class Main : MelonMod
    {
        public override void OnApplicationStart() //Sets up UI Expansion Kit
        {
            var category = MelonPreferences.CreateCategory("VRParticles", "Beautify Particles");

            if (MelonHandler.Mods.Any(it => it.Info.Name == "UI Expansion Kit"))
            {
                MelonLogger.Msg("Adding UIExpansionKit buttons");
                var ui = ExpansionKitApi.GetSettingsCategory("VRParticles");
                ui.AddSimpleButton("Fix Particles", SetParticles); // Manually beautify particles.
            }
        }
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            SetParticles();
        }
        public void SetParticles() // Beautifies particles.
        {
            LoggerInstance.Msg("Making particles Pretty!");
            var ParticleRenderers = Resources.FindObjectsOfTypeAll<ParticleSystemRenderer>();
            if (ParticleRenderers.Length > 0)
            {
                for (int i = 0; i < ParticleRenderers.Length; i++)
                {
                    ParticleRenderers[i].alignment = ParticleSystemRenderSpace.Facing;
                    ParticleRenderers[i].allowRoll = false;
                }
                LoggerInstance.Msg("**** Made " + ParticleRenderers.Length + " particles pretty ****");
            }
            else
            {
                LoggerInstance.Msg("**** No particles found to beautify ****");
            }
            
        }
    }
}