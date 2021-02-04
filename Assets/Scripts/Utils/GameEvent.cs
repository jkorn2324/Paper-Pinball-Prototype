using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball.Scripts.Utils
{
    /// <summary>
    /// The Game Event implementation.
    /// </summary>
    [CreateAssetMenu(fileName = "Game Event", menuName = "Events/Game Event")]
    public class GameEvent : ScriptableObject
    {
        private event System.Action gameEvent
            = delegate { };

        /// <summary>
        /// Invokes the event.
        /// </summary>
        /// <param name="input">The input.</param>
        public virtual void Call()
        {
            this.gameEvent();
        }

        /// <summary>
        /// The addition operator overload function.
        /// </summary>
        /// <param name="e">The generic event we are adding to.</param>
        /// <param name="func">The function we are hooking up.</param>
        /// <returns>The generic event.</returns>
        public static GameEvent operator +(GameEvent e, System.Action @func)
        {
            e.gameEvent += func;
            return e;
        }

        /// <summary>
        /// The subtraction operator overload function.
        /// </summary>
        /// <param name="e">The generic event we are adding to.</param>
        /// <param name="func">The function we are hooking up.</param>
        /// <returns>The generic event.</returns>
        public static GameEvent operator -(GameEvent e, System.Action @func)
        {
            e.gameEvent -= func;
            return e;
        }
    }
}

