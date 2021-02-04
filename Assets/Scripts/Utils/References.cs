using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Pinball.Scripts.Utils.References
{
    /// <summary>
    /// The Abstract Reference class implementation.
    /// </summary>
    /// <typeparam name="T">The type of the reference.</typeparam>
    public abstract class GenericReference<T>
    {
        #region fields

        [SerializeField]
        [Tooltip("Determines whether or not the reference is a constant.")]
        private bool constant;
        [SerializeField]
        [Tooltip("The constant value if the given reference is a constant value.")]
        protected T constantValue;

        #endregion

        #region properties

        /// <summary>
        /// Gets the reference value.
        /// </summary>
        protected abstract T ReferenceValue
        {
            set;
            get;
        }

        /// <summary>
        /// The value property.
        /// </summary>
        public T Value
        {
            get
            {
                return this.constant ? this.constantValue : this.ReferenceValue;
            }
            set
            {
                this.ReferenceValue = value;
            }
        }

        /// <summary>
        /// Determines whether the reference is a constant reference.
        /// </summary>
        protected bool Constant
            => this.constant;

        #endregion

        #region methods

        /// <summary>
        /// Resets the reference.
        /// </summary>
        abstract public void Reset();

        /// <summary>
        /// Determines if the value does change.
        /// </summary>
        /// <param name="prev">The previous value</param>
        /// <param name="next">The next value</param>
        /// <returns>True if the value changed, false otherwise.</returns>
        protected virtual bool DidChange(T prev, T next)
        {
            return !prev.Equals(next);
        }

        #endregion
    }

    /// <summary>
    /// The Float Reference Definition.
    /// </summary>
    [System.Serializable]
    public class FloatReference : GenericReference<float>
    {
        #region fields

        [SerializeField]
        private Variables.FloatVariable variable;

        #endregion

        #region properties

        public event System.Action<float> ChangedValueEvent
        {
            add
            {
                if (this.variable != null)
                {
                    this.variable.ChangedValueEvent += value;
                }
            }
            remove
            {
                if (this.variable != null)
                {
                    this.variable.ChangedValueEvent -= value;
                }
            }
        }

        /// <summary>
        /// The Reference value definition.
        /// </summary>
        protected override float ReferenceValue
        {
            get => variable != null ? variable.Value : this.constantValue;
            set
            {
                if (this.variable != null)
                {
                    variable.Value = value;
                }
            }
        }

        #endregion

        #region methods

        public override void Reset()
        {
            if (this.Constant)
            {
                return;
            }

            this.variable?.Reset();
        }

        #endregion
    }

    /// <summary>
    /// The Integer Reference implementation.
    /// </summary>
    [System.Serializable]
    public class IntegerReference : GenericReference<int>
    {

        #region fields

        [SerializeField]
        private Variables.IntegerVariable variable;

        #endregion

        #region properties

        public event System.Action<int> ChangedValueEvent
        {
            add
            {
                if (this.variable != null)
                {
                    this.variable.ChangedValueEvent += value;
                }
            }
            remove
            {
                if (this.variable != null)
                {
                    this.variable.ChangedValueEvent -= value;
                }
            }
        }

        protected override int ReferenceValue
        {
            get => variable != null ? variable.Value : this.constantValue;
            set
            {
                if (this.variable != null)
                {
                    variable.Value = value;
                }
            }
        }

        #endregion

        #region methods

        public override void Reset()
        {
            if (this.Constant)
            {
                return;
            }

            this.variable?.Reset();
        }

        #endregion
    }

    /// <summary>
    /// The boolean reference class.
    /// </summary>
    [System.Serializable]
    public class BooleanReference : GenericReference<bool>
    {
        #region fields

        [SerializeField]
        private Variables.BooleanVariable variable;

        #endregion

        #region properties

        public event System.Action<bool> ChangedValueEvent
        {
            add
            {
                if (this.variable != null)
                {
                    this.variable.ChangedValueEvent += value;
                }
            }
            remove
            {
                if (this.variable != null)
                {
                    this.variable.ChangedValueEvent -= value;
                }
            }
        }

        protected override bool ReferenceValue
        {
            get => this.variable != null ? this.variable.Value : this.constantValue;
            set
            {
                if (this.variable != null)
                {
                    variable.Value = value;
                }
            }
        }

        #endregion

        #region methods

        public override void Reset()
        {
            if (this.Constant)
            {
                return;
            }

            this.variable?.Reset();
        }

        #endregion
    }

    /// <summary>
    /// The string reference.
    /// </summary>
    [System.Serializable]
    public class StringReference : GenericReference<string>
    {

        #region fields

        [SerializeField]
        private Variables.StringVariable variable;

        #endregion

        #region properties

        public event System.Action<string> ChangedValueEvent
        {
            add
            {
                if (this.variable != null)
                {
                    this.variable.ChangedValueEvent += value;
                }
            }
            remove
            {
                if (this.variable != null)
                {
                    this.variable.ChangedValueEvent -= value;
                }
            }
        }

        protected override string ReferenceValue
        {
            get => variable != null ? variable.Value : this.constantValue;
            set
            {
                if (this.variable != null)
                {
                    variable.Value = value;
                }
            }
        }

        #endregion

        #region methods

        public override void Reset()
        {
            if (this.Constant)
            {
                return;
            }

            this.variable?.Reset();
        }

        #endregion
    }

    /// <summary>
    /// The Vector2 Reference.
    /// </summary>
    [System.Serializable]
    public class Vector2Reference : GenericReference<Vector2>
    {
        #region fields

        [SerializeField]
        private Variables.Vector2Variable variable;

        #endregion

        #region properties

        public event System.Action<Vector2> ChangedValueEvent
        {
            add
            {
                if (this.variable != null)
                {
                    this.variable.ChangedValueEvent += value;
                }
            }
            remove
            {
                if (this.variable != null)
                {
                    this.variable.ChangedValueEvent -= value;
                }
            }
        }

        protected override Vector2 ReferenceValue
        {
            get => variable != null ? variable.Value : this.constantValue;
            set
            {
                if (this.variable != null)
                {
                    variable.Value = value;
                }
            }
        }

        #endregion

        #region methods

        public override void Reset()
        {
            if (this.Constant)
            {
                return;
            }

            this.variable?.Reset();
        }

        #endregion
    }
}
