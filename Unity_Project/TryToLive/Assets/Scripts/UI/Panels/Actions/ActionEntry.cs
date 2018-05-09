using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionEntry : MonoBehaviour {

    public TextMeshProUGUI actionNameText;
    public string dialogue;



    public void Initialize(ActionData actionData) {
        actionNameText.text = actionData.actionName;
        dialogue = actionData.dialogue;
    }


    public void OnClick() {
        StartCoroutine(ShowDialog());
    }


    private IEnumerator ShowDialog() {
        //dialog.gameObject.SetActive(true);
        DialoguePanel dialoguePanel = PanelManager.panelManager.GetPanelByType(BasePanel.PanelType.Dialogue) as DialoguePanel;

        dialoguePanel.OpenAndShow(dialogue);

        yield return new WaitForSeconds(2f);

        dialoguePanel.HideDialoge();
    }

}
