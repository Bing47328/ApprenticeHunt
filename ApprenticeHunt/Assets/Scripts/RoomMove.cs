using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    public Vector2 cameraChange;
    public Vector3 playerChange;
    private CamMove cam;

    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;

    public bool changeBounds;
    public Vector2 newMax;
    public Vector2 newMin;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CamMove>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            cam.minPosition += cameraChange;
            cam.maxPosition += cameraChange;
            collision.transform.position += playerChange;
            if (changeBounds)
            {
                cam.maxPosition += newMax;
                cam.minPosition += newMin;
            }

            if (needText)
            {
                StartCoroutine(placeNameCoroutine());

            }
        }
    }

    private IEnumerator placeNameCoroutine()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }
}
