using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball.Scripts.Utils.Variables
{
    /// <summary>
    /// The Abstract Variable Scriptable Object definition.
    /// </summary>
    /// <typeparam name="T">The type of the generic variable.</typeparam>
    public abstract class GenericVariable<T> : ScriptableObject
    {
        #region fields

        [SerializeField]
        private T value;
        [SerializeField]
        private T originalValue;

        public event System.Action<T> ChangedValueEvent
            = delegate { };

        #endregion

        #region properties

        public T Value
        {
            set
            {
                if (!this.value.Equals(value))
                {
                    this.ChangedValueEvent(value);
                }
                this.value = value;
            }
            get => this.value;
        }

        #endregion

        #region methods

        private void OnEnable()
        {
            this.value = this.originalValue;
        }

        /// <summary>
        /// Resets the value.
        /// </summary>
        public virtual void Reset()
        {
            this.value = originalValue;
        }

        #endregion
    }
}
