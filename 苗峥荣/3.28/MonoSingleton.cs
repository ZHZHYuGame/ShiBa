using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public bool global = true;
        
    private static T _instance;
        
    public static T Instance
    {
        get
        {
            if (_instance is null)
            {
                _instance = FindObjectOfType<T>();
            }
            return _instance;
        }

    }

    private void Awake()
    {
        Debug.LogWarningFormat("{0}[{1}] Awake", typeof(T), GetInstanceID());
        if (global)
        {
            if (_instance != null && _instance != gameObject.GetComponent<T>())
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            _instance = gameObject.GetComponent<T>();
        }
        OnAwake();
    }

    private void OnAwake() {}
}