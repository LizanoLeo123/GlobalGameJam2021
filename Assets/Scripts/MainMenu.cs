using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator bgAnimator;

    public void playGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void quitGame() {
        Application.Quit();
    }

    public void StartGame()
    {
        bgAnimator.SetTrigger("Start");
        StartCoroutine(LoadMainScene());
    }

    IEnumerator LoadMainScene()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("LeoDev");
    }

}
