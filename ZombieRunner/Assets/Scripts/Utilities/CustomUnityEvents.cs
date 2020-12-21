using System;
using Enumerations;
using UnityEngine.Events;

namespace Utilities
{
  [Serializable] public class UnityIntEvent : UnityEvent<int> {}
  
  [Serializable] public class UnityTypeOfAmmoEvent : UnityEvent<TypeOfAmmo> {}
}