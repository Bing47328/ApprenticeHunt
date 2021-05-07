using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Game()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Intro()
    {
        SceneManager.LoadScene("Intro");
    }

    void End()
    {
        SceneManager.LoadScene("End");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            End();
        }
    }
}
