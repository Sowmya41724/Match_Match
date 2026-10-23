using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    static MusicPlayer instance;

    void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}