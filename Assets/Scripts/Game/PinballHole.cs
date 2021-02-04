using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball.Scripts.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PinballHole : MonoBehaviour
    {
        [SerializeField]
        private PinballHole connectedHole;

        [SerializeField]
        private Vector2 exitDirection;
        [SerializeField]
        private float exitForce;

        public Vector2 Force
            => this.exitDirection.normalized * this.exitForce;

        /// <summary>
        /// Called when the pinball hole has detected a trigger.
        /// </summary>
        /// <param name="collision">The collider 2d.</param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Pinball.PinballComponent component =
                collision.attachedRigidbody.GetComponent<Pinball.PinballComponent>();
            if(component != null)
            {
                component.TransferToHole(connectedHole);
            }
        }
    }
}
