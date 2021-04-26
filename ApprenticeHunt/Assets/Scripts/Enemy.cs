using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // Health Bar
    public float maxHP = 100f;
    public float currentHP;
    public Image health;
    public Image healthEffect;

    // Movement
    [SerializeField]protected private float moveSpeed = 1;
    protected private Transform target;
    private SpriteRenderer sr;
    [SerializeField]protected private float distance = 4;


    private void Start()
    {
        currentHP = maxHP;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        DiplayHealthBar();
        Move();
        TurnDirection();

        if (currentHP <= 0)
        {
            Death();
        }
    }

    protected virtual void Move()
    {
        if (Vector2.Distance(transform.position, target.position) < distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
           
    }

    void Death()
    {
        Destroy(gameObject);
    }

    protected virtual void TurnDirection()
    {
        if (transform.position.x > target.position.x)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
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
        if (collision.tag == "Projectile")
            currentHP -= 15;

    }
}
