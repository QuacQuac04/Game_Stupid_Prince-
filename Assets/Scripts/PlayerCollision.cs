using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameManager gameManager;
    private BoxCollider2D boxCollider;
    private Player player;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>(); //Lay tham chieu nhung scripts co ten la "GameManager"
        boxCollider = GetComponent<BoxCollider2D>();

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            gameManager.AddScore(1);
            Debug.Log("Hit Coin");
        }
        else if (collision.CompareTag("Trap"))
        {
            gameManager.GameOver();
            Debug.Log("Dau qua");
        }
        else if (collision.CompareTag("Enemy"))
        {
            gameManager.GameOver();
            Debug.Log("Enemy danh tui");
        }
        else if (collision.CompareTag("Key") || collision.CompareTag("Key2") || collision.CompareTag("Key3"))
        {
            Destroy(collision.gameObject);
            gameManager.AddScoreKey(1);

            string nextScene = "";
            if (collision.CompareTag("Key"))
            {
                nextScene = "Game2";
            }else if (collision.CompareTag("Key2"))
            {
                nextScene = "Game3";
            }else if (collision.CompareTag("Key3"))
            {
                nextScene = "Game4";
            }

            //Phù hợp với 2 map
            //string nextScene = collision.CompareTag("Key") ? "Game2" : "Game3";

            gameManager.GameLoading(nextScene);
            Debug.Log("Da nhat duoc key");
        }
        else if (collision.CompareTag("DropMap"))
        {
            gameManager.GameOver();
            Debug.Log("Nguoi choi da roi khoi map");
        }
        else if (collision.CompareTag("Enemy_Crab"))
        {
            gameManager.GameOver();
            Debug.Log("Con cua no danh toi");
        }
        //else if (collision.CompareTag("Quicksand"))
        //{
        //    boxCollider.isTrigger = true;
        //    gameManager.GameOver();
        //    Debug.Log("Dang di chuyen tren cat lun");
        //}
        else if (collision.CompareTag("End"))
        {
            gameManager.GameWin();
            Debug.Log("Victory");
        }
    }
}
