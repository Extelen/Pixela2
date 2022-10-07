using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class PanelSetsBehaviour : Openable
    {
        //Variables
        [Header("References")]
        [SerializeField] private List<Panel> m_panels;

        //Methods
        /// <summary>
        /// Open all the panels at the same time.
        /// </summary>
        public override void Open()
        {
            gameObject.SetActive(true);
            m_panels.ForEach(c => c.Open());
        }
        /// <summary>
        /// Close all the panels at the same time.
        /// </summary>
        public override void Close()
        {
            Panel last = null;
            float minTime = Mathf.Infinity;

            foreach (Panel panel in m_panels)
            {
                if (panel.disableTime < minTime)
                {
                    minTime = panel.disableTime;
                    last = panel;
                }
            }

            foreach (Panel panel in m_panels)
            {
                if (panel == last) panel.Close(() => gameObject.SetActive(false));
                else panel.Close();
            }
        }

        /// <summary>
        /// Set the panels default states at the same time.
        /// </summary>
        public override void SetDefaulState(bool active)
        {
            m_panels.ForEach(c => c.SetDefaulState(active));
            gameObject.SetActive(active);
        }
    }
}
