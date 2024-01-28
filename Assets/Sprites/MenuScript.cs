using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public AudioClip clickSound,sitSound;
    public GameObject fade;
    public void ClickOnStart()
    {
        StartCoroutine(WaitBeforeStart());
    }

    public void ClickOnExit()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(clickSound);
        Application.Quit();
    }
    IEnumerator WaitBeforeStart()
    {
        Camera.main.GetComponent<AudioSource>().Stop();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(clickSound);
        fade.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        Camera.main.GetComponent<AudioSource>().PlayOneShot(sitSound);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Intro");
    }
}
