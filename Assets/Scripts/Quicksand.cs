using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quicksand : MonoBehaviour
{
    private bool isQuicksand = false;
    private float time = 0f;
    public float delay = 2f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.isKinematic = true;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isQuicksand) {

            time += Time.deltaTime;
            if (time >= delay) {
                if (rb != null) { 
                    rb.isKinematic = false;
                    //Time.timeScale = 2.5f;
                    //Destroy(gameObject);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isQuicksand = true;
        }
    }
}
