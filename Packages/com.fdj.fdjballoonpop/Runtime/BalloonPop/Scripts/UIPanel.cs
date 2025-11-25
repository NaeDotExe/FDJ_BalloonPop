using UnityEngine;

namespace BalloonPop
{

    [RequireComponent(typeof(CanvasGroup))]
    public class UIPanel : MonoBehaviour
    {
        protected CanvasGroup m_canvasGroup;

        #region Methods
        protected void Awake()
        {
            m_canvasGroup = GetComponent<CanvasGroup>();
        }

        public virtual void Show()
        {
            m_canvasGroup.alpha = 1f;
            m_canvasGroup.interactable = true;
            m_canvasGroup.blocksRaycasts = true;
            //gameObject.SetActive(true);
        }
        public virtual void Hide()
        {
            m_canvasGroup.alpha = 0f;
            m_canvasGroup.interactable = false;
            m_canvasGroup.blocksRaycasts = false;

            //gameObject.SetActive(false);
        }
        #endregion
    }
}
