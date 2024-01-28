using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    public TextMeshProUGUI dialogueText,characterName;
    public Image characterTalkingSprite;
    public Story story;
    public GameObject buttonLocation;
    public Button prefabButton;
    public GameObject endingScreen;
    public ArmReslingScript ARS;
    public AudioClip clickSound;
    public AudioClip backgroundMusic, reslingMusic;
    //public Animator Fade;
    [SerializeField]
    private List<TextAsset> inks;

    [SerializeField]
    private Sprite padge,papets,steve,pikachu,shlepa;
    [SerializeField]
    private Sprite badEnd, friendPapets, friendShlepa, friendGiga, friendSteve, jail;

    private int score = 0;
    private int currentActor = 0;
    private bool textRunning = false;
    public void Start()
    {
        LoadStory(inks[0]);
    }
    public void LoadStory(TextAsset textAsset)
    {
        StopAllCoroutines();
        story = new Story(textAsset.text);
        string text = story.Continue();
        text = text.Trim();
        StartCoroutine(AppearingText(text));
        if (story.currentTags.Count > 0) handleTag(story.currentTags);
        if (story.currentChoices.Count > 0)
        {
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                Button button = Instantiate(prefabButton) as Button;
                button.transform.SetParent(buttonLocation.transform);
                button.GetComponentInChildren<TextMeshProUGUI>().text = choice.text;
                button.onClick.AddListener(delegate
                {
                    OnClickChoiseButton(choice);
                });
            }
        }
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0)&&story.canContinue&&!textRunning)
        {
            string text = story.Continue();
            text = text.Trim();
            StartCoroutine(AppearingText(text));
            if (story.currentTags.Count > 0) handleTag(story.currentTags);
            if (story.currentChoices.Count > 0&& buttonLocation.transform.childCount==0)
            {
                for(int i = 0; i < story.currentChoices.Count; i++)
                {
                    Choice choice = story.currentChoices[i];
                    Button button = Instantiate(prefabButton) as Button;
                    button.transform.SetParent(buttonLocation.transform);
                    button.GetComponentInChildren<TextMeshProUGUI>().text = choice.text;
                    button.onClick.AddListener(delegate
                    {
                        OnClickChoiseButton(choice);
                    });
                }
            }
        }
        else if (Input.GetMouseButtonDown(0) && textRunning)
        {
            StopAllCoroutines();
            dialogueText.text = story.currentText.Trim();
            textRunning = false;
        }
    }
    public void PudgeWinsArmsresling()
    {
        story.ChooseChoiceIndex(0);
        dialogueText.text = story.currentText;
        characterName.text = "Сеньор Пиджак";
        StopAllCoroutines();
        buttonLocation.gameObject.SetActive(true);
        DeleteButtons();
        Camera.main.GetComponent<AudioSource>().Stop();
        Camera.main.GetComponent<AudioSource>().loop = true;
        Camera.main.GetComponent<AudioSource>().clip = backgroundMusic;
        Camera.main.GetComponent<AudioSource>().Play();
    }
    public void SteveWinsArmresling()
    {
        story.ChooseChoiceIndex(1);
        dialogueText.text = story.currentText;
        characterName.text = "Сеньор Пиджак";
        StopAllCoroutines();
        buttonLocation.gameObject.SetActive(true);
        DeleteButtons();
        Camera.main.GetComponent<AudioSource>().Stop();
        Camera.main.GetComponent<AudioSource>().loop = true;
        Camera.main.GetComponent<AudioSource>().clip = backgroundMusic;
        Camera.main.GetComponent<AudioSource>().Play();
    }
    void handleTag(List<string> currentTags)
    {
        string currentTag = "";
        string endingTag = "";
        foreach(string tag in currentTags)
        {
            if (tag.Contains("speaker"))
            {
                currentTag = tag;
            }
            else if (tag.Contains("концовка"))
            {
                endingTag = tag;
            }
            else if (tag.Contains("армрестлинг")) {

                buttonLocation.gameObject.SetActive(false);
                ARS.gameObject.SetActive(true);
                ARS.StartFight();
                Camera.main.GetComponent<AudioSource>().Stop();
                Camera.main.GetComponent<AudioSource>().PlayOneShot(reslingMusic);
            }
        }
        if (currentTag.Contains("speaker"))
        {
            switch (currentTag)
            {
                case "speaker: Papetc":
                    characterTalkingSprite.sprite = papets;
                    characterName.text = "Виталик Артас";
                    break;
                case "speaker: Padge":
                    characterName.text = "Сеньор Пиджак";
                    break;
                case "speaker: Shlepa":
                    characterTalkingSprite.sprite = shlepa;
                    characterName.text = "Шлёпа";
                    break;
                case "speaker: Giga":
                    characterTalkingSprite.sprite = pikachu;
                    characterName.text = "Гигачу";
                    break;
                case "speaker: Stuf":
                    characterTalkingSprite.sprite = steve;
                    characterName.text = "Стуф";
                    break;
                default:
                    characterName.text = "Опять прогер чорт";
                    break;
            }
        }
        if (endingTag.Contains("концовка"))
        {
            switch (endingTag)
            {
                case "концовка хорошая":
                    score += 1;
                    switch (currentActor)
                    {
                        case 0:
                            ShowEndingScreen(friendPapets);
                            break;
                        case 1:
                            ShowEndingScreen(friendSteve);
                            break;
                        case 2:
                            ShowEndingScreen(friendGiga);
                            break;
                        case 3:
                            StartCoroutine(FinalEnding());
                            break;
                    }
                    if (currentActor < 3)
                    {
                        currentActor += 1;
                        LoadStory(inks[currentActor]);
                    }
                    break;
                case "концовка плохая":
                    currentActor += 1;
                    LoadStory(inks[currentActor]);
                    break;
                case "концовка полиция":
                    ShowEndingScreen(jail);
                    currentActor += 1;
                    LoadStory(inks[currentActor]);
                    break;
            }
            if (currentActor == 3 && score == 0)
            {
                StartCoroutine(BadEnding());
            }
            else if (currentActor == 3 && score < 3)
            {
                StartCoroutine(NormalEnding());
            }
            else if(currentActor==3&&score==3)
            {
                LoadStory(inks[currentActor]);
            }
        }
    }
    IEnumerator NormalEnding()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Credits");
    }
    IEnumerator FinalEnding()
    {
        ShowEndingScreen(friendShlepa);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Credits");
    }
    IEnumerator BadEnding()
    {
        ShowEndingScreen(badEnd);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Credits");
    }
    public void ShowEndingScreen(Sprite sprite)
    {
        endingScreen.transform.GetChild(0).gameObject.SetActive(true);
        endingScreen.GetComponentInChildren<Image>().sprite = sprite;
        endingScreen.GetComponent<Animator>().Play("EndingAnim");
    }
    void OnClickChoiseButton(Choice choice)
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(clickSound);
        story.ChooseChoiceIndex(choice.index);
        DeleteButtons();
        dialogueText.text = choice.text;
        characterName.text = "Сеньор Пиджак";
    }
    void DeleteButtons()
    {
        foreach(Button btn in buttonLocation.transform.GetComponentsInChildren<Button>())
        {
            Destroy(btn.gameObject);
        }
    }
    IEnumerator AppearingText(string text)
    {
        string temp = "";
        textRunning = true;
        for(int i = 0; i < text.Length; i++)
        {
            temp += text[i];
            dialogueText.text = temp;
            yield return new WaitForSeconds(0.03f);
        }
        textRunning = false;
    }
}
