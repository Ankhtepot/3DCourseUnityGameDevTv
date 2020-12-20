using System.Linq;
using UnityEngine;

namespace Utilities
{
    public static class AnimatorExtensions
    {
        public static bool HasParameter(this Animator animator, string parameter)
        {
            return animator.parameters.Any(animatorControllerParameter => animatorControllerParameter.name == parameter);
        } 
    }
}