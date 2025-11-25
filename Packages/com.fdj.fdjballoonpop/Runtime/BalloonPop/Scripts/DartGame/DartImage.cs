using UnityEngine;
using UnityEngine.UI;

namespace BalloonPop
{

    public class DartImage : MonoBehaviour
    {
        [SerializeField] private Image m_image;
        [SerializeField] private Image m_fill;

        [SerializeField] private Color m_enabledColor;
        [SerializeField] private Color m_disabledColor;

        private bool m_enabled = true;

        public bool Enabled
        {
            get { return m_enabled; }
        }

        public void Enable(bool enabled)
        {
            m_image.enabled = enabled;
            m_fill.enabled = enabled;

            m_enabled = enabled;
        }

        public void Show(bool show)
        {
            Debug.Log("Show Dart Image: " + show);

            m_image.enabled = true;
            m_image.color = show ? m_enabledColor : m_disabledColor;

            m_fill.enabled = show;
        }
    }

}