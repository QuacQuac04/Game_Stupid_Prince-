using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broken_Sand : MonoBehaviour
{
    private bool isSteppedOn = false;
    private float timer = 0f;
    public float delay = 2f; // thời gian trễ 2 giây

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.isKinematic = true; // ban đầu block không rơi
    }

    void Update()
    {
        if (isSteppedOn)
        {
            timer += Time.deltaTime;
            if (timer >= delay)
            {
                // Cách 1: Cho block rơi xuống
                if (rb != null)
                    rb.isKinematic = false;

                // Cách 2: Hoặc phá huỷ block
                //Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isSteppedOn = true;
        }
    }
}
