using UnityEngine;

//Fireball Games * * * PetrZavodny.com

namespace Utilities
{
    public class Rotator : MonoBehaviour
    {
#pragma warning disable 649
        [SerializeField] private float XSpeed = 0;
        [SerializeField] private float YSpeed = 100;
        [SerializeField] private float ZSpeed = 0;
#pragma warning restore 649

        void Update()
        {
            var rotation = transform.rotation;
            transform.Rotate( XSpeed * Time.deltaTime, YSpeed * Time.deltaTime, ZSpeed * Time.deltaTime, Space.Self);
        }
    }
}
