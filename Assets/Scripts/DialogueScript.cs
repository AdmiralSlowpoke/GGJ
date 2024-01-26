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

    public void Start()
    {
        story = new Story(ink.text);
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0)&&story.canContinue)
        {
            string text = story.Continue();
            text = text.Trim();
            dialogueText.text = text;
            if (story.currentTags.Count > 0) handleTag(story.currentTags[0]);
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
    }
    void handleTag(string currentTag)
    {
        Debug.Log(currentTag);
        switch (currentTag)
        {
            case "speaker: Papetc":
                characterName.text = "Папич";
                break;
            case "speaker: Padge":
                characterName.text = "Пиджак";
                break;
            case "speaker: Shlepa":
                characterName.text = "Шлёпа";
                break;
            default:
                characterName.text = "Опять прогер чорт";
                break;
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
}
