#pragma warning disable 0414

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI
{
    public class MouseMoveOverlay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
    {
#pragma warning disable 649
        [SerializeField] protected bool isMouseOver;
        [SerializeField] private UnityEvent MouseOver;
        [SerializeField] private UnityEvent MouseLeave;
#pragma warning restore 649

        public void SetMouseOver(bool isOver)
        {
            if (isOver)
            {
                OnPointerEnter(null);
            }
            else
            {
                OnPointerExit(null);
            }
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            isMouseOver = true;
            MouseOver.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isMouseOver = false;
            MouseLeave.Invoke();
        }
    }
}
