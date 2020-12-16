using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

//Fireball Games * * * PetrZavodny.com

namespace UI
{
    public class MouseClickOverlay : MouseMoveOverlay, IPointerClickHandler
    {
#pragma warning disable 649
        [SerializeField] public UnityEvent OnClick;
#pragma warning restore 649

        public void OnPointerClick(PointerEventData eventData)
        {
            if (isMouseOver)
            {
                OnClick?.Invoke();
            }
        }
    }
}
