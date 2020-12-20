using System;
using UnityEngine.Events;

namespace Utilities
{
    public class CustomUnityEvents
    {
        [Serializable] public class UnityIntEvent : UnityEvent<int> {}
    }
}