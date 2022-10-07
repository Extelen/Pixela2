using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    [System.Serializable]
    public struct UIContainer
    {
        //Variables
        [SerializeField] private string m_identifier;
        [SerializeField] private Openable m_openable;

        public string identifier => m_identifier;
        public Openable openable => m_openable;

        //Methods
        public UIContainer(string identifier, Openable openable)
        {
            m_identifier = identifier;
            m_openable = openable;
        }
    }
}