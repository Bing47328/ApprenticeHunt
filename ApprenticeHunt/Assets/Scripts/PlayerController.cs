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
    private Animator catAnimator;
    private string currentState;

    //Dialog Box
    public GameObject dialog;

    //Shoot
    public GameObject bullet;
    public Transform shotLocation;
    bool isShooting = false;

    //Cat
    public GameObject cat;
    public List<Transform> tailPositions;


    public GameObject Dead;
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

        if (currentHP < 0)
        {
            Death();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isShooting)
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
        ChangingAnimationState("Walk");

        MoveCat();
    }

    void MoveCat()
    {
        Vector3 lastPos = GameObject.Find("Tail Holder").transform.position;

        if (tailPositions.Count >= 1)
        {
            tailPositions.Last().position = lastPos;
            tailPositions.Insert(0, tailPositions.Last());
        }

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

    void Shoot()
    {
        isShooting = true;
        StartCoroutine(waitforAnim("Shoot"));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 spawnPos = new Vector2(5, 5);

        if (collision.tag == "Enemy")
            currentHP -= 15;

        if (collision.tag == "Cat")
        {
            Destroy(collision.gameObject);
            GameObject newCat = Instantiate(cat, spawnPos, Quaternion.identity) as GameObject;
            newCat.transform.parent = GameObject.Find("Tail Holder").transform;
            tailPositions.Add(newCat.transform);
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
        yield return new WaitForSeconds(.3f);
        isShooting = false;
    }

}
