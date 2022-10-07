using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public abstract class Openable : MonoBehaviour
    {
        //Properties
        public bool isOpen { get; protected set; }

        //Methods
        /// <summary>
        /// Set the initial state of the openable.
        /// </summary>
        /// <param name="active"> Start state </param>
        public abstract void SetDefaulState(bool active);

        /// <summary>
        /// Send to the child the open state.
        /// </summary>
        public abstract void Open();

        /// <summary>
        /// Send to the child the close state.
        /// </summary>
        public abstract void Close();

        /// <summary>
        /// Toggle between open and close.
        /// </summary>
        public void Toggle()
        {
            if (isOpen) Close();
            else Open();
        }
    }
}