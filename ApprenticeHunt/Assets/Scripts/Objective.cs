using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public GameObject textBox;
    public bool key;
    public GameObject door;
    public GameObject exit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (key)
        {
            if (collision.tag == "Player")
            {
                Destroy(door);
                Destroy(gameObject);
            }
        }
        else if (!key)
        {
            if (collision.tag == "Player")
            {
                textBox.SetActive(true);
                exit.SetActive(true);
                Destroy(gameObject.GetComponent<BoxCollider2D>());

            }
        }
    }
}
