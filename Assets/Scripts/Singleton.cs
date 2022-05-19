using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;

    public static T Instance
    {
        get
        {
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
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = GetComponent<T>();
        DontDestroyOnLoad(gameObject);

        if (instance == null) return;
    }
}
