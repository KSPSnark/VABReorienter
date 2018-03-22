using UnityEngine;

namespace VABReorienter
{
    /// User settings for this mod, accessible through the settings menu for the game.
    /// 
    /// Invaluable advice about how to make this work can be found here:
    /// https://forum.kerbalspaceprogram.com/index.php?/topic/147576-modders-notes-for-ksp-12/
    class ReorienterSettings : GameParameters.CustomParameterNode
    {
        private static readonly Quaternion ROTATION_NORTH = Quaternion.Euler(0, 0, 0);
        private static readonly Quaternion ROTATION_EAST = Quaternion.Euler(0, 90, 0);

        /// <summary>
        /// Allows choosing the default orientation of a new ship in the VAB.
        /// </summary>
        public enum VABOrientation
        {
            /// <summary>
            /// Ship faces north.  This is the default in the stock game.
            /// </summary>
            North,
            /// <summary>
            /// Ship faces east.  This is what any sane person (defined as "someone
            /// who thinks like me") would want, and is why I wrote this mod in the
            /// first place.
            /// </summary>
            East
        };

        public override string Title => Reorienter.MOD_NAME;

        public override string DisplaySection => Reorienter.MOD_NAME;

        public override string Section => Reorienter.MOD_NAME;

        public override int SectionOrder => 1000; // put it (probably) last

        public override GameParameters.GameMode GameMode => GameParameters.GameMode.ANY;

        public override bool HasPresets => false;

        /// <summary>
        /// This field is the user setting that controls the default orientation
        /// of ships in the VAB.
        /// </summary>
        [GameParameters.CustomParameterUI("Default VAB orientation", toolTip = "Choose which direction a new ship faces in the VAB, by default.")]
        public VABOrientation DefaultVABOrientation = VABOrientation.East;

        /// <summary>
        /// Gets the default rotation to apply to vessels in the VAB.
        /// </summary>
        public static Quaternion VABRotation
        {
            get
            {
                switch (Instance.DefaultVABOrientation)
                {
                    case VABOrientation.North:
                        return ROTATION_NORTH;
                    case VABOrientation.East:
                        return ROTATION_EAST;
                    default:
                        // Should never happen, it means I've got a bug in this file ;-)
                        Debug.LogError("[" + Reorienter.MOD_NAME + "] unknown orientation " + Instance.DefaultVABOrientation);
                        return Quaternion.identity;
                }
            }
        }

        /// <summary>
        /// Gets a string description of the current VAB rotation, for logging purposes.
        /// </summary>
        public static string VABRotationLabel
        {
            get
            {
                return Instance.DefaultVABOrientation.ToString();
            }
        }

        private static ReorienterSettings Instance
        {
            get { return HighLogic.CurrentGame.Parameters.CustomParams<ReorienterSettings>();  }
        }
    }
}
