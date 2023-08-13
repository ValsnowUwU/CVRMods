using MelonLoader;
using System.Linq;
using UnityEngine;

namespace BeautifyParticles
{
    public class Main : MelonMod
    {
        public MelonPreferences_Category preferencesCategory;
        public MelonPreferences_Entry<bool> autoBeautify;
        public MelonPreferences_Entry<bool> setFacing;
        public MelonPreferences_Entry<bool> setRoll;
        public MelonPreferences_Entry<bool> onlyView;

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
                UIManager.setupBTKUILib(this);
            }
            else
            {
                LoggerInstance.Warning("BTKUILib is recommended.");
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