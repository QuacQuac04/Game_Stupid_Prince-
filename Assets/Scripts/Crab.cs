using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grap : MonoBehaviour
{
    [SerializeField] private float speedGrab = 2f;
    [SerializeField] private float distanceGrab = 5f;
    [SerializeField] private float chaseDistance = 10f;
    [SerializeField] private float idleTime = 3f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackDuration = 1f;

    private Vector3 startPos;
    private Animator animator;
    private Transform player;
    private bool isChasing = false;
    private bool isAttacking = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        startPos = transform.position;
        StartCoroutine(IdleState());
    }

    void Update()
    {
        if (isChasing && !isAttacking)
        {
            ChasePlayer();
        }
    }

    private IEnumerator IdleState()
    {
        animator.SetBool("isCrabIdle", true);
        animator.SetBool("isCrabRun", false);
        animator.SetBool("isCrabAt1", false);
        yield return new WaitForSeconds(idleTime);
        animator.SetBool("isCrabIdle", false);
        StartChasing();
    }

    private void StartChasing()
    {
        isChasing = true;
        isAttacking = false;
        Flip();
    }

    private void ChasePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= chaseDistance)
        {
            animator.SetBool("isCrabRun", true);
            animator.SetBool("isCrabIdle", false);
            animator.SetBool("isCrabAt1", false);

            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speedGrab * Time.deltaTime;

            transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);

            if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            if (Vector3.Distance(transform.position, player.position) < distanceGrab)
            {
                StartCoroutine(AttackSequence());
            }
        }
        else
        {
            animator.SetBool("isCrabRun", false);
            isChasing = false;
            StartCoroutine(IdleState());
            Flip();
        }
    }

    private IEnumerator AttackSequence()
    {
        isAttacking = true;
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            animator.SetBool("isCrabRun", true);
            animator.SetBool("isCrabAt1", false);
            animator.SetBool("isCrabIdle", false);

            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speedGrab * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);

            if (direction.x > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else if (direction.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            animator.SetBool("isCrabRun", false);
            animator.SetBool("isCrabIdle", false);
            animator.SetBool("isCrabAt1", true);

            // Đợi hoàn thành animation tấn công
            yield return new WaitForSeconds(attackDuration);

            // Reset về trạng thái chase
            animator.SetBool("isCrabAt1", false);
            Debug.Log("Đang đánh player!");
        }

        isAttacking = false;
        StartChasing();
    }

    public void OnPlayerAttack()
    {
        animator.SetTrigger("isCrabHit");
        StopAllCoroutines();
        isAttacking = false;
        isChasing = false;
        Flip();
        StartCoroutine(IdleState());
    }

    void Flip()
    {
        Vector3 scaler = transform.localScale;
        scaler.x *= 1; // Sửa lại thành -1 để flip đúng hướng
        transform.localScale = scaler;
    }
}
