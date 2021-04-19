using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public GameObject textBox;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            textBox.SetActive(true);

            Destroy(gameObject.GetComponent<BoxCollider2D>());
        }
    }
}
