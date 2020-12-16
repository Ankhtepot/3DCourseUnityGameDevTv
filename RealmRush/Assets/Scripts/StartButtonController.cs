using UnityEngine;
using UnityEngine.Events;

//Fireball Games * * * PetrZavodny.com

public class StartButtonController : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] public UnityEvent OnClickedEvent;
    [SerializeField] private GameObject button;
#pragma warning restore 649

    /// <summary>
    /// From event
    /// </summary>
    public void OnClicked()
    {
        button.gameObject.SetActive(false);
        OnClickedEvent?.Invoke();
    }
}
