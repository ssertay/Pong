using UnityEngine;

// Singleton to keep track of data across scenes.
public class StaticGameData : MonoBehaviour
{
    public static GameObject Instance;

    public int Player1Score = 0;
    public int Player2Score = 0;
    public bool isLocalMulti = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = gameObject;
        }

        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
