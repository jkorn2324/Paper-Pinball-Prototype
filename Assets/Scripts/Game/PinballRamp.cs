using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball.Scripts.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PinballRamp : MonoBehaviour
    {
        [SerializeField]
        private Utils.References.Vector2Reference pinballPosition;
        [SerializeField]
        private ScoreReference score;

        [SerializeField]
        private GameObject rampColliderChild;
        [SerializeField]
        private Collider2D enterTrigger;
        [SerializeField]
        private Collider2D exitTrigger;
        [SerializeField]
        private GameObject rampBlockers;

        private Collider2D _currentCollider = null;

        private bool _pinballInside = false;

        private void Start()
        {
            if(this.rampColliderChild != null)
            {
                this.rampColliderChild.SetActive(false);
            }
        }

        /// <summary>
        /// Updates the ramp.
        /// </summary>
        private void Update()
        {
            if(this.enterTrigger == null || this.exitTrigger == null)
            {
                return;
            }

            if(this._pinballInside)
            {
                return;
            }

            Vector3 enterTriggerPos = this.enterTrigger.transform.position;
            bool active = enterTriggerPos.y < this.pinballPosition.Value.y;
            this.enterTrigger.gameObject.SetActive(active);
            this.exitTrigger.gameObject.SetActive(active);
            this.rampColliderChild.SetActive(active);
        }

        /// <summary>
        /// Called when the pinball has entered the trigger.
        /// </summary>
        /// <param name="collision">The collision.</param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(this._currentCollider == null)
            {
                this._currentCollider = (collision.IsTouching(this.enterTrigger) ? this.enterTrigger
                    : collision.IsTouching(this.exitTrigger) ? this.exitTrigger : null);
            }

            Pinball.PinballComponent component = 
                collision.attachedRigidbody.GetComponent<Pinball.PinballComponent>();

            if(component != null && this._currentCollider == this.enterTrigger && !this._pinballInside)
            {
                // TODO: Get position
                Debug.Log("Entering the ramp");
                Vector2 difference = this.transform.position - component.transform.position;
                if(difference.y >= 0f)
                {
                    return;
                }

                StartCoroutine(this.SetRampBlockersActive(false));
                this._pinballInside = true;
                this.rampColliderChild.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            Collider2D currentCollider = this._currentCollider;
            if(this._currentCollider != null)
            {
                this._currentCollider = null;
            }

            Pinball.PinballComponent component =
                collision.attachedRigidbody.GetComponent<Pinball.PinballComponent>();

            if(component != null && currentCollider == this.exitTrigger && this._pinballInside)
            {
                Debug.Log("Exiting the ramp");
                Vector2 difference = this.transform.position - component.transform.position;
                if(difference.y <= 0f)
                {
                    return;
                }

                StartCoroutine(this.SetRampBlockersActive(true, 3.0f));
                this.score.ApplyScore();
                this._pinballInside = false;
                this.rampColliderChild.SetActive(false);
            }
        }

        private IEnumerator SetRampBlockersActive(bool active, float timeToWait = 0.0f)
        {
            yield return new WaitForSeconds(timeToWait);
            
            if (this.rampBlockers != null)
            {
                this.rampBlockers.SetActive(active);
            }
        }
    }
}
