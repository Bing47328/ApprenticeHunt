using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Health
    public float maxHP = 100f;
    public float currentHP;
    public Image health;
    public Image healthEffect;

    //Movement
    public float speed;
    private Rigidbody2D rb;
    private Vector3 change;
    bool faceR = false;

    //Animator
    private Animator animator;
    private string currentState;

    //Dialog Box
    public GameObject dialog;

    //Shoot
    public GameObject bullet;
    private float reload;
    public float startShot;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        currentHP = maxHP;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        reload = startShot;
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

        closeDialog();

        if (currentHP < 0)
        {
            Death();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
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

    void Death()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
            currentHP -= 15;

        if (collision.tag == "Cat")
            Destroy(collision.gameObject);
    }

    void closeDialog()
    {
        if (GameObject.Find("Dialog Manager").GetComponent<Dialog>().index % 2 == 0 && GameObject.Find("Dialog Manager").GetComponent<Dialog>().index != 0)
        {
            dialog.SetActive(false);
        }
    }

    void Shoot()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
        StartCoroutine(waitforAnim());
        ChangingAnimationState("Shoot");
    }

    IEnumerator waitforAnim()
    {
        yield return new WaitForSeconds(3);
    }
}
