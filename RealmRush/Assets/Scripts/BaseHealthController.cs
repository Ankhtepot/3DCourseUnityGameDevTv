using TMPro;
using UnityEngine;
using UnityEngine.Events;

//Fireball Games * * * PetrZavodny.com

public class BaseHealthController : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private TextMeshProUGUI baseHealthTMP;
    [SerializeField] private GameObject Body;
    [SerializeField] private int baseHealth;
    [SerializeField] private UnityEvent OnBaseHealthDepleted;
#pragma warning restore 649

    public void ReceiveDamage(EnemyInfo info)
    {
        baseHealth -= info ? info.DamageToBase : 1;
        UpdateBaseHealthTMP();

        if (baseHealth <= 0)
        {
            OnBaseHealthDepleted?.Invoke();
        }
    }

    public void SetHealth(int baseBaseHealth)
    {
        baseHealth = baseBaseHealth;
        UpdateBaseHealthTMP();
    }

    private void UpdateBaseHealthTMP()
    {
        baseHealthTMP.text = Mathf.Max(0, baseHealth).ToString();
    }

    public void Activate()
    {
        Body.SetActive(true);
    }
}
