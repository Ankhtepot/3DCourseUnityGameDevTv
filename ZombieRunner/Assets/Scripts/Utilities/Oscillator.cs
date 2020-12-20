using UnityEngine;

//Fireball Games * * * PetrZavodny.com

namespace Utilities
{
    [DisallowMultipleComponent]
    public class Oscillator : MonoBehaviour
    {
#pragma warning disable 649
        [SerializeField] private Vector3 movementVector = new Vector3(10f, 10f, 10f);
        [Range(0.5f,20f)][SerializeField] private float period;
        [Header("ObservedField")] 
        [Range(0.1f,1)][SerializeField] private float movementFactor;

        private Vector3 startingPosition;
#pragma warning restore 649

        void Start()
        {
            initialize();
        }

        void Update()
        {
            float cycles = Time.time / period; //framerate independent

            const float tau = Mathf.PI * 2;
            float rawSinWave = Mathf.Sin(cycles * tau);
        
            movementFactor = rawSinWave / 2f + 0.5f;
            Vector3 offset = movementVector * movementFactor;
            transform.position = startingPosition + offset;
        }
    
        private void initialize()
        {
            startingPosition = transform.position - (movementVector / 2f);
        }
    }
}
