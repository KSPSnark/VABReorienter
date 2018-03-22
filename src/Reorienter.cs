using UnityEngine;

namespace VABReorienter
{
    /// <summary>
    /// Sets the default orientation of ships in the VAB.
    /// </summary>
    [KSPAddon(KSPAddon.Startup.EditorVAB, false)]
    public class Reorienter : MonoBehaviour
    {
        internal const string MOD_NAME = "VABReorienter";

        public void Awake()
        {
            GameEvents.onEditorStarted.Add(OnEditorStarted);
        }

        public void OnDestroy()
        {
            GameEvents.onEditorStarted.Remove(OnEditorStarted);
        }

        private void OnEditorStarted()
        {
            Debug.Log("[" + MOD_NAME + "] Setting VAB orientation to " + ReorienterSettings.VABRotationLabel);
            EditorLogic.fetch.vesselRotation = ReorienterSettings.VABRotation;
        }
    }
}
