using UnityEngine;

/// <summary>
/// A static instance used for quick access of a script. Similar as a singleton but instead of destroying new instances, creating a new instance overides the current instance. 
/// </summary>
public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour {
    public static T Instance { get; private set; }  //Static Instance for referencing

    protected virtual void Awake() => Instance = this as T;

    protected virtual void OnApplicationQuit() {
        Instance = null;
        Destroy(gameObject);
    }
}

/// <summary>
/// A singleton destroys any new versions created and leaves the original instance intact. Not persistent meaning it will be destoyed through scene loads.
/// </summary>
public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour {
    protected override void Awake() {
        if (Instance != null)   //Initialize singleton
        {
            Destroy(gameObject);
        }
        else {
            base.Awake();
        }
    }
}

/// <summary>
/// A persistent singleton has the same functionalities of a singleton with the addition that the instance wont be destroyed through scene loads.
/// </summary>
public abstract class SingletonPersistent<T> : Singleton<T> where T : MonoBehaviour {
    protected override void Awake() {
        base.Awake();
        DontDestroyOnLoad(gameObject);  //Initialize persistent singleton
    }
}


