using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball.Scripts.Game
{

    /// <summary>
    /// Handles score triggers.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class ScoreTrigger : MonoBehaviour
    {
        [SerializeField]
        private ScoreReference score;

        /// <summary>
        /// Called when the collision has been triggered.
        /// </summary>
        /// <param name="collision">The collision.</param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Pinball.PinballComponent component =
                collision.attachedRigidbody.GetComponent<Pinball.PinballComponent>();
            if(component != null)
            {
                this.score.ApplyScore();
            }
        }
    }
}
