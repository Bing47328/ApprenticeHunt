using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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
    public Transform shotLocation;
    bool isShooting = false;

    //Cats
    bool cat1ON = false;
    bool cat2ON = false;
    bool cat3ON = false;
    bool cat4ON = false;

    public GameObject Cat1;
    public GameObject Cat2;
    public GameObject Cat3;
    public GameObject Cat4;
    public Animator Cat1animator;
    public Animator Cat2animator;
    public Animator Cat3animator;
    public Animator Cat4animator;
    private int winCount = 0;

    //Death
    public GameObject Dead;
    public GameObject Exit;

    //Timer
    private float attackSpeed = 1f;
    private float canAttack;


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

        if (change != Vector3.zero && !isShooting)
        {
            MoveCharacter();
        }
        else if (isShooting)
        {
            change = Vector3.zero;
        }
        else
        {
            ChangingAnimationState("Idle");
        }

        if (change.x < 0 && faceR)
        {
            flip();
        }
        else if (change.x > 0 && !faceR)
        {
            flip();
        }

        DiplayHealthBar();

        closeDialog();


        if (Input.GetKeyDown(KeyCode.Space) && !isShooting)
        {
            Shoot();
        }

        if (winCount == 4)
        {
            Exit.SetActive(true);
        }
    }

    void ChangingAnimationState(string newState)
    {
        if (currentState == newState) return;
        animator.Play(newState);
        if(cat1ON)
        {
            Cat1animator.Play(newState);
        }
        if (cat2ON)
        {
            Cat2animator.Play(newState);
        }
        if (cat3ON)
        {
            Cat3animator.Play(newState);
        }
        if (cat4ON)
        {
            Cat4animator.Play(newState);
        }
        currentState = newState;
    }

    void MoveCharacter()
    {
        rb.MovePosition(transform.position + change * speed * Time.deltaTime);
        ChangingAnimationState("Walk");
    }

    void flip()
    {
        faceR = !faceR;
        transform.Rotate(0, 180, 0);
        GameObject.Find("HP Canvas").transform.Rotate(0, 180f, 0);

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
        Dead.SetActive(true);
        Destroy(gameObject);
    }
    public void TakeDMG(int dmgAmount)
    {
        //hurt.Play();
        currentHP -= dmgAmount;
        if (currentHP <= 0)
        {
            Death();
        }
    }

    void Shoot()
    {
        isShooting = true;
        StartCoroutine(waitforAnim("Shoot"));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 spawnPos = new Vector2(5, 5);

        if (collision.tag == "Cat 1")
        {
            Destroy(collision.gameObject);
            Cat1.SetActive(true);
            winCount++;
            cat1ON = true;
        }
        if (collision.tag == "Cat 2")
        {
            Destroy(collision.gameObject);
            Cat2.SetActive(true);
            winCount++;
            cat2ON = true;
        }
        if (collision.tag == "Cat 3")
        {
            Destroy(collision.gameObject);
            Cat3.SetActive(true);
            winCount++;
            cat3ON = true;
        }
        if (collision.tag == "Cat 4")
        {
            Destroy(collision.gameObject);
            Cat4.SetActive(true);
            winCount++;
            cat4ON = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (attackSpeed <= canAttack)
            {
                TakeDMG(15);
                canAttack = 0f;
            }
            else
            {
                canAttack += Time.deltaTime;
            }
        }
    }

    void closeDialog()
    {
        if (GameObject.Find("Dialog Manager").GetComponent<Dialog>().index % 2 == 0 && GameObject.Find("Dialog Manager").GetComponent<Dialog>().index != 0)
        {
            dialog.SetActive(false);
        }
    }

    IEnumerator waitforAnim(string anim)
    {
        ChangingAnimationState(anim);
        yield return new WaitForSeconds(.2f);
        Instantiate(bullet, shotLocation.position, shotLocation.rotation);
        isShooting = false;
    }

}
