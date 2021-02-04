using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball.Scripts.Game.Bumper
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BumperComponent : MonoBehaviour
    {
        [SerializeField]
        private float bumperPower;
        [SerializeField]
        private ScoreReference score;

        /// <summary>
        /// The on collision enter function.
        /// </summary>
        /// <param name="collision">The collision.</param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Rigidbody2D otherRB = collision.rigidbody;
            Pinball.PinballComponent pinballComponent =
                otherRB?.GetComponent<Pinball.PinballComponent>();

            if(pinballComponent != null)
            {
                Vector2 currentDirection = otherRB.velocity.normalized;
                otherRB.AddForce(currentDirection * bumperPower);

                this.score.ApplyScore();
            }
        }
    }
}
