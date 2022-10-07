using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class PanelsManager : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private string startPanel = "";

        [Header("References")]
        [SerializeField] private UIContainer[] containers = new UIContainer[0];

        //Methods
        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        protected virtual void OnEnable()
        {
            foreach (UIContainer container in containers)
            {
                container.openable?.SetDefaulState(container.identifier == startPanel);
            }
        }

        /// <summary>
        /// Change the container using the identifier.
        /// </summary>
        /// <param name="identifier">The identifier to use</param>
        public virtual void ChangeContainer(string identifier)
        {
            foreach (UIContainer container in containers)
            {
                if (container.identifier == identifier) container.openable?.Open();
                else container.openable?.Close();
            }
        }
    }
}