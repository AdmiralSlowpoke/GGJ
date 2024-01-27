using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    public TextMeshProUGUI dialogueText,characterName;
    public Image characterTalkingSprite;
    public Story story;
    public GameObject buttonLocation;
    public Button prefabButton;
    [SerializeField]
    private TextAsset ink = null;

    [SerializeField]
    private Sprite padge,papets,steve,pikachu,shlepa;

    private bool textRunning = false;
    public void Start()
    {
        story = new Story(ink.text);
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0)&&story.canContinue&&!textRunning)
        {
            string text = story.Continue();
            text = text.Trim();
            StartCoroutine(AppearingText(text));
            if (story.currentTags.Count > 0) handleTag(story.currentTags);
            if (story.currentChoices.Count > 0)
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
    void handleTag(List<string> currentTags)
    {
        string currentTag = "";
        foreach(string tag in currentTags)
        {
            if (tag.Contains("speaker") || tag.Contains("концовка"))
                currentTag = tag;
            else if (tag.Contains("sound")) Debug.Log(tag);
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
                    characterName.text = "Пиджак";
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
        else if (currentTag.Contains("концовка"))
        {
            /*switch (currentTag)
            {
                case ""
            }*/
        }
    }
    void OnClickChoiseButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        DeleteButtons();
        dialogueText.text = choice.text;
        characterName.text = "Пиджак";
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
