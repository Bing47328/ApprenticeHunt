using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float maxHP = 100f;
    public float currentHP;
    public Image health;
    public Image healthEffect;

    public float speed;
    private Rigidbody2D rb;
    private Vector3 change;
    bool faceR = false;

    private Animator animator;
    private string currentState;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        currentHP = maxHP;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if (change != Vector3.zero)
        {
            MoveCharacter();
            ChangingAnimationState("Walk");
        }
        else
        {
            ChangingAnimationState("Idle");
        }

        flip();

        DiplayHealthBar();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentHP -= 15;
        }

    }

    void ChangingAnimationState(string newState)
    {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }

    void MoveCharacter()
    {
        rb.MovePosition(transform.position + change * speed * Time.deltaTime);
    }

    void flip()
    {
        faceR = !faceR;
        if (change.x < 0 && faceR)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (change.x > 0 && !faceR)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    protected virtual void DiplayHealthBar()
    {
        health.fillAmount = currentHP / maxHP;

        if (healthEffect.fillAmount > health.fillAmount)
        {
            healthEffect.fillAmount -= 0.005f;
        }
        else
        {
            healthEffect.fillAmount = health.fillAmount;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            currentHP -= 15;
    }
}
