using System;
using Enumerations;
using UnityEngine.Events;

namespace Utilities
{
  [Serializable] public class UnityIntTypeOfAmmoEvent : UnityEvent<int, TypeOfAmmo> {}
  
  [Serializable] public class UnityTypeOfAmmoEvent : UnityEvent<TypeOfAmmo> {}
}