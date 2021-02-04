using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball.Scripts.Game.Launcher
{

    [System.Serializable]
    public struct LauncherReferences
    {
        #region fields

        [SerializeField]
        private Utils.References.FloatReference pullbackTime;
        [SerializeField]
        private Utils.References.FloatReference maxLaunchCooldown;
        [SerializeField]
        private Utils.References.FloatReference maxLaunchForce;
        [SerializeField]
        private Utils.References.FloatReference maxLaunchSpeed;

        #endregion

        #region properties

        public float PullBackTime => this.pullbackTime.Value;

        public float MaxLaunchSpeed => this.maxLaunchSpeed.Value;

        public float MaxLaunchForce => this.maxLaunchForce.Value;

        public float MaxLaunchCooldownTime => this.maxLaunchCooldown.Value;

        #endregion
    }

    [System.Serializable]
    public struct LauncherValues
    {
        [SerializeField]
        public KeyCode keyToLaunch;
        [SerializeField]
        public float minDistance;
    }

    // Contains the data of the previous launch.
    public struct PrevLaunchData
    {
        public float time;
        public float heldDownPercentage;
    }

    /// <summary>
    /// The Pong Launcher component.
    /// </summary>
    [RequireComponent(typeof(SpringJoint2D), typeof(Rigidbody2D))]
    public class PongLauncher : MonoBehaviour
    {
        #region fields
        
        [SerializeField]
        private LauncherReferences references;
        [SerializeField]
        private LauncherValues values;

        private float _currentTimeHeldDown = 0f;
        private float _currentLaunchSpeed = 0f;

        private PrevLaunchData _prevLaunchData;

        private SpringJoint2D _springJoint;
        private Rigidbody2D _rigidbody;

        #endregion

        #region properties

        private float HeldDownPercentage
            => Mathf.Clamp(this._currentTimeHeldDown / this.references.PullBackTime, 0.0f, 1.0f);

        #endregion

        #region methods

        private void Start()
        {
            this._springJoint = this.GetComponent<SpringJoint2D>();
            this._rigidbody = this.GetComponent<Rigidbody2D>();

            this._prevLaunchData = new PrevLaunchData();
            this._prevLaunchData.heldDownPercentage = 0.0f;
            this._prevLaunchData.time = 0.0f;
        }

        private void Update()
        {
            if(Input.GetKeyDown(this.values.keyToLaunch) || Input.GetKey(this.values.keyToLaunch))
            {
                this._currentTimeHeldDown += Time.deltaTime;
            }
            else if (Input.GetKeyUp(this.values.keyToLaunch))
            {
                this._currentLaunchSpeed = Mathf.Lerp(0.0f, this.references.MaxLaunchSpeed, this.HeldDownPercentage);
                this._prevLaunchData.heldDownPercentage = this.HeldDownPercentage;
                this._prevLaunchData.time = Time.time;
                this._currentTimeHeldDown = 0.0f;
            }
        }

        private void FixedUpdate()
        {
            if(this._currentLaunchSpeed != 0.0f)
            {
                this._rigidbody.AddForce(Vector2.up * this.references.MaxLaunchForce);
                this._springJoint.distance = 1.0f;
                this._currentLaunchSpeed = 0.0f;
            }

            if(this._currentTimeHeldDown != 0.0f)
            {
                float currentDistance = Mathf.Lerp(1.0f, this.values.minDistance, this.HeldDownPercentage);
                this._springJoint.distance = currentDistance;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Pinball.PinballComponent pinballComponent =
                collision.rigidbody.GetComponent<Pinball.PinballComponent>();
            if(pinballComponent != null)
            {
                if((Time.time - this._prevLaunchData.time) > this.references.MaxLaunchCooldownTime)
                {
                    return;
                }

                this._rigidbody.velocity = Vector2.zero;
                float currentForce = this.references.MaxLaunchForce * this._prevLaunchData.heldDownPercentage;
                pinballComponent.Launch(Vector2.up * currentForce);
            }
        }

        #endregion
    }
}
