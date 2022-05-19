using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;

    private static bool isDestroyed;

    public static T Instance
    {
        get
        {
            if (isDestroyed) return null;
            if (instance != null) return instance;
            instance = FindObjectOfType<T>();
            if (instance != null) return instance;
            GameObject singletonObj = new GameObject();
            singletonObj.name = typeof(T).ToString();
            instance = singletonObj.AddComponent<T>();
            return instance;
        }
    }

    public virtual void Awake()
    {
        isDestroyed = false;
        
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = GetComponent<T>();
        DontDestroyOnLoad(gameObject);

        if (instance == null) return;
    }

    private void OnDestroy()
    {
        isDestroyed = true;
    }
}
