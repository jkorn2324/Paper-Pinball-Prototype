using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball.Scripts.Game
{

    [System.Serializable]
    public struct PinballCameraValues
    {
        [SerializeField]
        private Transform minCameraPosition;
        [SerializeField]
        private Transform maxCameraPosition;

        public Vector2 MaxCameraPosition
            => (Vector2)this.maxCameraPosition.position;

        public Vector2 MinCameraPosition
            => (Vector2)this.minCameraPosition.position;
    }

    [System.Serializable]
    public struct PinballReferences
    {
        [SerializeField]
        private Utils.References.Vector2Reference pinballPosition;

        public Vector2 PinballPosition => this.pinballPosition.Value;
    }

    [RequireComponent(typeof(Camera), typeof(Rigidbody2D))]
    public class PinballCamera : MonoBehaviour
    {
        [SerializeField]
        private PinballCameraValues values;
        [SerializeField]
        private PinballReferences references;

        private Rigidbody2D _rigidbody;

        private void Start()
        {
            this._rigidbody = this.GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Vector2 currentPosition = this._rigidbody.position;
            currentPosition.y = Mathf.Clamp(this.references.PinballPosition.y,
                this.values.MinCameraPosition.y, this.values.MaxCameraPosition.y);
            this._rigidbody.MovePosition(currentPosition);
        }
    }
}
