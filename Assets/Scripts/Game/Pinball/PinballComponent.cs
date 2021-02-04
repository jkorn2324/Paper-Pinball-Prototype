using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball.Scripts.Game.Pinball
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PinballComponent : MonoBehaviour
    {
        [SerializeField]
        private Utils.References.Vector2Reference position;
        [SerializeField]
        private Utils.References.FloatReference holeCooldownTime;
        [SerializeField]
        private Utils.References.IntegerReference lives;
        [SerializeField]
        private Utils.GameEvent resetGameEvent;

        private float _cooldownTime = 0f;

        private Rigidbody2D _rigidbody;
        private Vector2 _originalPosition;

        private void Start()
        {
            this._originalPosition = this.transform.position;
            this._rigidbody = this.GetComponent<Rigidbody2D>();
        }


        private void OnEnable()
        {
            this.resetGameEvent += this.OnResetGame;
        }

        private void OnDisable()
        {
            this.resetGameEvent -= this.OnResetGame;
        }

        private void Update()
        {
            if(this._cooldownTime > 0f)
            {
                this._cooldownTime -= Time.deltaTime;
                if(this._cooldownTime <= 0f)
                {
                    this._cooldownTime = 0f;
                }
            }
            this.position.Value = this.transform.position;
        }

        public void Launch(Vector2 launchForce)
        {
            this._rigidbody.AddForce(launchForce);
        }

        private void OnResetGame()
        {
            Destroy(this.gameObject);
        }
        
        public void TransferToHole(PinballHole hole)
        {
            if(this._cooldownTime > 0f)
            {
                return;
            }

            this._rigidbody.AddForce(hole.Force);
            this._cooldownTime = this.holeCooldownTime.Value;
            this.transform.position = hole.transform.position;
        }

        /// <summary>
        /// Called when the pinball has entered a trigger.
        /// </summary>
        /// <param name="collision">The collision.</param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.name == "GutterCollider")
            {
                this.lives.Value--;
                Destroy(this.gameObject, 0.2f);
            }
        }
    }
}
