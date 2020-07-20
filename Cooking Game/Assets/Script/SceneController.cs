using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    public GameObject[] panelImage;
    private bool isMainImage = true;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            backScene();
        }
    }

    // Start is called before the first frame update
    public void nextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void backScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void goToMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }


    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void NextImage()
    {
        isMainImage = !isMainImage;

        panelImage[0].SetActive(isMainImage);
        panelImage[1].SetActive(!isMainImage);
    }
}
