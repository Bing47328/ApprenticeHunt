using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activation : MonoBehaviour
{
    public GameObject dialog;
    public GameObject titleText;
    public GameObject startText;

    public int indexStop;

    private void Update()
    {
        closeDialog();
    }

    void closeDialog()
    {
        if (GameObject.Find("Dialog Manager").GetComponent<Dialog>().index == indexStop)
        {
            dialog.SetActive(false);
            titleText.SetActive(true);
            startText.SetActive(true);
        }
    }
}
