using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball.Scripts.Game.Flippers
{

    [RequireComponent(typeof(Rigidbody2D), typeof(HingeJoint2D))]
    public class FlippersComponent : MonoBehaviour
    {
        [SerializeField]
        private KeyCode keyToPress;
        [SerializeField]
        private Utils.References.FloatReference rotationForce;
        [SerializeField]
        private Utils.References.FloatReference unwindForce;

        private HingeJoint2D _hingeJoint;

        private bool IsKeyPressed
            => Input.GetKey(this.keyToPress);

        private void Start()
        {
            this._hingeJoint = this.GetComponent<HingeJoint2D>();
        }

        private void FixedUpdate()
        {
            if(this.IsKeyPressed)
            {
                this.SetMotorSpeed(this.rotationForce.Value);
                return;
            }
            this.SetMotorSpeed(this.unwindForce.Value);
        }

        /// <summary>
        /// Flips the flipper.
        /// </summary>
        private void SetMotorSpeed(float motorSpeed)
        {
            JointMotor2D jointMotor = this._hingeJoint.motor;
            jointMotor.motorSpeed = motorSpeed;
            this._hingeJoint.motor = jointMotor;
        }
    }
}
