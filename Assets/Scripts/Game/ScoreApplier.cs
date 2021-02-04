using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball.Scripts.Game
{

    /// <summary>
    /// The applier to the score.
    /// </summary>
    [System.Serializable]
    public enum ScoreApplierType
    {
        TYPE_ADD,
        TYPE_MULTIPLY
    }

    [System.Serializable]
    public struct ScoreAdditive
    {
        [SerializeField]
        private int scoreAmount;

        /// <summary>
        /// Applies the score to the additive.
        /// </summary>
        /// <param name="reference">The integer reference.</param>
        public void ApplyScore(Utils.References.IntegerReference reference)
        {
            reference.Value += scoreAmount;
        }
    }

    [System.Serializable]
    public struct ScoreMultiplier
    {
        [SerializeField]
        private int scoreMultiplier;

        /// <summary>
        /// Applies the score to the additive.
        /// </summary>
        /// <param name="reference">The reference.</param>
        public void ApplyScore(Utils.References.IntegerReference reference)
        {
            reference.Value *= scoreMultiplier;
        }
    }

    [System.Serializable]
    public class ScoreReference
    {
        [SerializeField]
        private Utils.References.IntegerReference scoreReference;
        [SerializeField]
        private ScoreApplierType applierType;
        
        [SerializeField]
        private ScoreAdditive additive;
        [SerializeField]
        private ScoreMultiplier multiplier;

        /// <summary>
        /// Applies a score value to the amount.
        /// </summary>
        public void ApplyScore()
        {
            switch (this.applierType)
            {
                case ScoreApplierType.TYPE_ADD:
                    this.additive.ApplyScore(scoreReference);
                    break;
                case ScoreApplierType.TYPE_MULTIPLY:
                    this.additive.ApplyScore(scoreReference);
                    break;
            }
        }
    }
}
