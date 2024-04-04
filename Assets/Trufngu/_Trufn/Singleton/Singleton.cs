using UnityEngine;
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _ins;

    public static T Ins
    {
        get
        {
            if (_ins == null)
            {
                // Find singleton
                _ins = FindObjectOfType<T>();

                // Create new instance if one doesn't already exist.
                if (_ins == null)
                {
                    // Need to create a new GameObject to attach the singleton to.
                    var singletonObject = new GameObject();
                    _ins = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T) + " (Singleton)";

                }

            }
            return _ins;
        }
    }

}
