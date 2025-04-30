using UnityEngine;
using UnityEngine.SceneManagement;

public class Even : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Game1");
        Debug.Log("Ban da an vo Playgame");
    }
}
