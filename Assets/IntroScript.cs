using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    public GameObject text;
    public void Start()
    {
        StartCoroutine(IntroScene());
    }
    IEnumerator IntroScene()
    {
        yield return new WaitForSeconds(0.8f);
        text.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Game");
    }
}
