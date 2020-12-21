using Enumerations;
using UnityEngine;

//Fireball Games * * * PetrZavodny.com

public class AmmoPickup : MonoBehaviour
{
#pragma warning disable 649
   [SerializeField] private TypeOfAmmo TypeOfAmmo;
   [SerializeField] private int ammoAmount = 10;
#pragma warning restore 649

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         Debug.Log($"Pickup triggered");
         if (other.GetComponent<Ammo>().AddAmmo(TypeOfAmmo, ammoAmount))
         {
            Debug.Log($"Pickup of type {TypeOfAmmo} added to player inventory.");
            Destroy(gameObject);
         }
      }
   }
}
