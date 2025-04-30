using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask quickSandLayer;
    [SerializeField] private Transform quickSandCheck;

    private bool isGrounded;
    private bool isQuickSand;
    private Rigidbody2D rb;
    private Animator animator; //Animator not Animation
    private GameManager gameManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameManager = FindAnyObjectByType<GameManager>();

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager == null)
        {
            gameManager = FindAnyObjectByType<GameManager>();
            if (gameManager == null) return; // Chưa có GameManager thì skip luôn Update
        }

        if (gameManager.IsGameOver() || gameManager.IsGameLoading()) return;
        HandleMovement();
        HandleJump();
        UpdateAnimation();
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        if (moveInput > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    private void HandleJump()
    {
        // 1) Cập nhật va chạm
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
   

        // 2) Xử lý nhảy
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void UpdateAnimation()
    {
        bool isRunning = Mathf.Abs(rb.velocity.x) > 0.1f;
        //bool isJumping = Mathf.Abs(rb.velocity.y) > 0.1f;
        bool isJumping = !isGrounded;

        animator.SetBool("IsRunning", isRunning);
        animator.SetBool("IsJumping", isJumping);
    }
}
