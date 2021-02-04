using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pinball.Scripts.Game
{

    [RequireComponent(typeof(Canvas))]
    public class PinballUI : MonoBehaviour
    {
        [SerializeField]
        private Utils.References.IntegerReference score;
        [SerializeField]
        private Text scoreText;

        [SerializeField]
        private Utils.References.IntegerReference lives;
        [SerializeField]
        private Text livesText;

        private void Start()
        {
            this.OnLivesChanged(this.lives.Value);
        }

        private void OnEnable()
        {
            score.ChangedValueEvent += this.OnScoreChanged;
            lives.ChangedValueEvent += this.OnLivesChanged;
        }

        private void OnDisable()
        {
            score.ChangedValueEvent -= this.OnScoreChanged;
            lives.ChangedValueEvent -= this.OnLivesChanged;
        }

        /// <summary>
        /// Called when the score has been changed.
        /// </summary>
        /// <param name="newScore">The new score.</param>
        private void OnScoreChanged(int newScore)
        {
            if(this.scoreText != null)
            {
                this.scoreText.text = "Score: " + newScore;
            }
        }

        /// <summary>
        /// Called when the lives have changed.
        /// </summary>
        /// <param name="lives">The number of lives.</param>
        private void OnLivesChanged(int lives)
        {
            if(this.livesText != null)
            {
                this.livesText.text = "Lives: " + lives;
            }
        }
    }
}
