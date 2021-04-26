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
    public float moveSpeed;
    private Transform target;
    private SpriteRenderer sr;

    public Transform wayPoint1, wayPoint2;
    private Transform wayPointTarget;
    private float distance;


    private void Awake()
    {
        currentHP = maxHP;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        wayPointTarget = wayPoint1;
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

    void Move()
    {
        if (Vector2.Distance(transform.position, target.position) > distance)
        {

            if (Vector2.Distance(transform.position, wayPoint1.position) < 0.01f)
            {
                wayPointTarget = wayPoint2;
            }
            if (Vector2.Distance(transform.position, wayPoint2.position) < 0.01f)
            {
                wayPointTarget = wayPoint1;
            }
            transform.position = Vector2.MoveTowards(transform.position, wayPointTarget.position, moveSpeed * Time.deltaTime);

        }
           // transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    void Death()
    {
        Destroy(gameObject);
    }

    void TurnDirection()
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
