using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class TimedDestroy : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private float destroyTime;
#pragma warning restore 649

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
