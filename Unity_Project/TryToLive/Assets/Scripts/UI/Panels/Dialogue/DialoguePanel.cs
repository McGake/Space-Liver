using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialoguePanel : BasePanel {

    public TextMeshProUGUI dialogueText;


    public void ShowDialogue(string text) {
        dialogueText.text = text;
        dialogueText.gameObject.SetActive(true);
    }

    public void HideDialoge() {
        dialogueText.gameObject.SetActive(false);
    }

    public void OpenAndShow(string text) {
        Open();
        ShowDialogue(text);
    }


}
