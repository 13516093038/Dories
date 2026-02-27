using Unity.VisualScripting;
using UnityEngine;

namespace Dories.Runtime.ComponentizationUI.Utils
{
    public static class UnityTransformExternal
    {
        public static Component TryAddComponent<T>(this Transform transform) where T : Component
        {
            transform.TryGetComponent<T>( out T t);
            if (t == null)
            {
                t = transform.AddComponent<T>();
            }
            return t;
        }
    }
}