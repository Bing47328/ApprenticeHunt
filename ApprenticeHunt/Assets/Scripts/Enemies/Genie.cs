using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genie : Enemy
{
    private float shotRate = 2.1f;
    private float shotTimer;
    private string currentState;
    private AudioSource audioSource;


    public GameObject projectile;
    public Animator animator;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected override void Move()
    {
        //base.Move();//MARKER Give up the base Move Function!!
    }

    void ChangingAnimationState(string newState)
    {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }

    protected override void Attack()
    {
        base.Attack();

        shotTimer += Time.deltaTime;

        if (shotTimer > shotRate)
        {
            if (Vector2.Distance(transform.position, target.position) < distance)
            {
                ChangingAnimationState("Shoot");
                audioSource.Play();
                Instantiate(projectile, transform.position, Quaternion.identity);
                shotTimer = 0;
            }
        }
        else
        {
            ChangingAnimationState("Idle");
        }
    }
}
