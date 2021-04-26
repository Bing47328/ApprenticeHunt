using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mudmelt : Enemy
{
    public Transform wayPoint1, wayPoint2;
    private Transform wayPointTarget;


    void Awake()
    {
        wayPointTarget = wayPoint1;
    }

    protected override void Move()
    {
        base.Move();

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
    }
}
