using System.Collections;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class EndWaypointController : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private float disposeStep = 0.01f;
    private GameController gameController;
#pragma warning restore 649

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(DisposeEnemy(other.GetComponentInParent<EnemyInfo>()));
        }
    }

    private IEnumerator DisposeEnemy(EnemyInfo info)
    {
        while (info && info.transform.localScale.x > 0)
        {
            yield return new WaitForSeconds(disposeStep);
            if (info)
            {
                info.transform.localScale -= new Vector3(disposeStep, disposeStep, disposeStep);
            }
        }
        // print($"Disposing the enemy.");
        if (info)
        {
            gameController.EnemyReachedBase(info);
            Destroy(info.gameObject);
        }
    }
}
