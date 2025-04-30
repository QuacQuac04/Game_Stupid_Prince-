using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; private set; }

    public int Score { get; set; }
    public int ScoreKey { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ lại giữa các scene
        }
        else
        {
            Destroy(gameObject); // Tránh trùng lặp khi load lại scene
        }
    }

    public void ResetAll()
    {
        Score = 0;
        ScoreKey = 0;
    }
}
