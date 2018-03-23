using UnityEngine;

namespace VABReorienter
{
    /// <summary>
    /// Sets the default orientation of ships in the VAB.
    ///
    /// If you're wondering "Why is this marked KSPAddon.Startup.EditorAny?  Don't you
    /// want it only in the VAB?" ...well, therein lies a tale.  It turns out that
    /// KSPAddon.Startup conveniently has the following three values in it:
    /// - EditorAny
    /// - EditorVAB
    /// - EditorSPH
    /// ...which seems perfect, right?  Just pick EditorVAB and we get the behavior we
    /// want, right?
    ///
    /// Well, you could easily be forgiven for thinking so... but you would be wrong.
    /// And the reason you would be wrong because all three of these enum values HAVE
    /// THE SAME VALUE AS EACH OTHER AND MEAN EXACTLY THE SAME THING. In effect, they're
    /// *all* EditorAny.  So, if we marked it "EditorVAB"... it would *purport* to run
    /// only in the VAB, but actually would also be running in the SPH as well because
    /// it's apparently not physically possible to do anything else.
    ///
    /// Therefore, I'll go ahead and mark this as EditorAny to avoid being misleading,
    /// and then add some kludge code to detect which editor it is and take appropriate
    /// action.
    /// </summary>
    [KSPAddon(KSPAddon.Startup.EditorAny, false)]
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
            if (CurrentFacility != EditorFacility.VAB) return;
            Debug.Log("[" + MOD_NAME + "] Setting VAB orientation to " + ReorienterSettings.VABRotationLabel);
            EditorLogic.fetch.vesselRotation = ReorienterSettings.VABRotation;
        }

        /// <summary>
        /// Get which editor facility we're in.
        /// </summary>
        private static EditorFacility CurrentFacility
        {
            get
            {
                // Yes, this is bizarrely byzantine and roundabout, but as far as I
                // can tell, it's actually the only way to tell what facility we're in.
                // Fortunately, the "ship" property of EditorLogic is populated and
                // non-null even when we enter the editor and there's nothing there yet.
                return EditorLogic.fetch.ship.shipFacility;
            }
        }
    }
}
