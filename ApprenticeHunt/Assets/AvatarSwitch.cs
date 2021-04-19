using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarSwitch : MonoBehaviour
{
    public Sprite sprite1, sprite2;
    public GameObject img;

    // Update is called once per frame
    void Update()
    {
        ChangeImg();
    }

    void ChangeImg()
    {
        if (GameObject.Find("Dialog Manager").GetComponent<Dialog>().index == 0)
        {
            img.GetComponent<Image>().sprite = sprite1;
        }
        else
        {
            img.GetComponent<Image>().sprite = sprite2;
        }
    }
}
