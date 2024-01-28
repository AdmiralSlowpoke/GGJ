using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsScripts : MonoBehaviour
{
    public Image avatarImage;
    public TextMeshProUGUI text;

    public Sprite wolf, neko, kakashi, anime1, neko2, anime2;

    public void Start()
    {
        StartCoroutine(CreditsScene());
    }

    IEnumerator CreditsScene()
    {
        avatarImage.sprite = wolf;
        text.text = "������� ���������, ��������";
        yield return new WaitForSeconds(4.5f);
        avatarImage.sprite = neko;
        text.text = "������ ����������, ��������";
        yield return new WaitForSeconds(4.5f);
        avatarImage.sprite = kakashi;
        text.text = "����� �������, ����������";
        yield return new WaitForSeconds(4.5f);
        avatarImage.sprite = anime1;
        text.text = "������� ��������, ����������";
        yield return new WaitForSeconds(4.5f);
        avatarImage.sprite = neko2;
        text.text = "������ ���������, �����������";
        yield return new WaitForSeconds(4.5f);
        avatarImage.sprite = anime2;
        text.text = "������ �����������, ���������";
        yield return new WaitForSeconds(4.5f);
        SceneManager.LoadScene("Menu");
    }
}
