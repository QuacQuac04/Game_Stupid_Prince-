using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    private int score = 0;

    private int scorekey = 0;

    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private TextMeshProUGUI scoreKeyText;

    [SerializeField] private GameObject gameOverUi;

    [SerializeField] private GameObject gameLoadingUi;

    [SerializeField] private GameObject gameWinUi;

    //[SerializeField] private GameObject quickSand;

    private bool isGameOver = false;

    private bool isGameLoading = false;

    private bool isGameWin = false;




    // Start is called before the first frame update
    void Start()
    {
        UpdateScore();
        UpdateScoreKey();
        gameOverUi.SetActive(false); //Ẩn cái Panel khi game bắt đầu
        gameLoadingUi.SetActive(false); //Ẩn cái Panel khi game bắt đầu
        gameWinUi.SetActive(false); //Ẩn cái Panel khi game bắt đầu
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
     
    }

    //Thêm và cập nhật Coins

    public void AddScore(int points) //Tao mot public AddScore voi kieu du lieu chuyen vao la int points
    {
        if (!isGameOver && !isGameWin) 
        {
            //Cách 1 
            //score += points;
            //UpdateScore();
            //Cách 2
            GameDataManager.Instance.Score += points;
            UpdateScore();
        }
    }

    private void UpdateScore()
    {
        //Cách 1
        //scoreText.text = score.ToString();
        //Cách 2
        scoreText.text = GameDataManager.Instance.Score.ToString();
    }


    //Thêm và cập nhật key
    public void AddScoreKey(int keys)
    {
        if(!isGameOver && !isGameWin && !isGameLoading)
        {
            //Cách 1
            //scorekey += keys;
            //UpdateScoreKey();

            //Cách 2
            GameDataManager.Instance.ScoreKey += keys;
            UpdateScoreKey();
        }
    }

    public void UpdateScoreKey()
    {
        //Cách 1
        //scoreKeyText.text = scorekey.ToString();

        //Cách 2
        scoreKeyText.text = GameDataManager.Instance.ScoreKey.ToString();
    }

    //Game thua cuộc

    public void GameOver() 
    {
        //Cách 1
        //isGameOver = true; //Hiện gameover thành true
        //score = 0; //Reset lại điểm về thành 0
        //scorekey = 0; //Reset key về thành 0
        //Time.timeScale = 0; //Không cho người dùng ấn nút khi game thua
        //gameOverUi.SetActive(true); //Hiện cái Panel GameOver lên thành true

        //Cách 2
        isGameOver = true;
        GameDataManager.Instance.ResetAll(); // Reset từ GameDataManager
        UpdateScore();
        UpdateScoreKey();
        Time.timeScale = 0;
        gameOverUi.SetActive(true);
    }

    public void RestarGame(string sceneName)
    {
        //Cách 1
        //Debug.Log("Reset Game:" + sceneName);
        //isGameOver = false;
        //score = 0;
        //scorekey = 0;
        //UpdateScore();
        //UpdateScoreKey();
        //Time.timeScale = 1;
        //SceneManager.LoadScene(sceneName);

        //Cách 2
        Debug.Log("Reset Game: " + sceneName);
        isGameOver = false;
        GameDataManager.Instance.Score = 0;
        // GameDataManager.Instance.ScoreKey = không reset để giữ nguyên
        UpdateScore();
        UpdateScoreKey();
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
   

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void GameLoading(string sceneName)
    {
        isGameLoading = true;
        Time.timeScale = 1;
        UpdateScore();
        UpdateScoreKey();
        gameLoadingUi.SetActive(true);
        //SceneManager.LoadScene("Key2");

        StartCoroutine(LoadSceneAfterDelay(sceneName)); //Bắt đầu đợi 4s rồi mới mở sang màn hình level tiếp theo
    }

    private IEnumerator LoadSceneAfterDelay(string sceneName)
    {
        yield return new WaitForSecondsRealtime(4f); //Đợi 4s trong thời gian thực
        SceneManager.LoadScene(sceneName);
        Debug.Log("Đang load scene: " + sceneName);
    }


    public bool IsGameLoading()
    {
        return isGameLoading;
    }


    //Game Win
    public void GameWin()
    {
        ////Cách 1
        //isGameWin = true;
        //score = 0; //Reset lại điểm về thành 0
        //scorekey = 0; //Reset key về thành 0
        //Time.timeScale = 0; //Không cho người dùng ấn nút khi game thua
        //gameWinUi.SetActive(true); //Hiện cái Panel GameWin lên thành true

        //Cách 2
        isGameWin = true;
        GameDataManager.Instance.ResetAll();
        UpdateScore();
        UpdateScoreKey();
        Time.timeScale = 0;
        gameWinUi.SetActive(true);
    }

    public void Mainmenu()
    {
        SceneManager.LoadScene("Menu");
        Debug.Log("Main Menu");
    }
}
