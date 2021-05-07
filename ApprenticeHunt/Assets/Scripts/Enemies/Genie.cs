using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genie : Enemy
{
    private float shotRate = 2.1f;
    private float shotTimer;
    private string currentState;

    public GameObject projectile;
    public Animator animator;

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
            ChangingAnimationState("Shoot");
            Instantiate(projectile, transform.position, Quaternion.identity);
            shotTimer = 0;
        }
        else
        {
            ChangingAnimationState("Idle");
        }
    }
}
