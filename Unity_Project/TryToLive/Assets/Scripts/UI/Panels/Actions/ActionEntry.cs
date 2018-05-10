using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionEntry : MonoBehaviour {

    public TextMeshProUGUI actionNameText;
    public string dialogue;

    private List<GameEffect> effects = new List<GameEffect>();


    public void Initialize(ActionData actionData) {
        actionNameText.text = actionData.actionName;
        dialogue = actionData.dialogue;
    }

    public void Initialize(ActionData actionData, GameEffect effect) {
        Initialize(actionData);
        effects.Add(effect);

        Debug.Log(effect + " has been set");
    }

    public void Initialize(ActionData actionData, List<GameEffect> effects = null) {
        Initialize(actionData);
        this.effects = effects;
    }

 


    public void OnClick() {
        StartCoroutine(ShowDialog());
    }


    private IEnumerator ShowDialog() {
        //dialog.gameObject.SetActive(true);
        DialoguePanel dialoguePanel = PanelManager.panelManager.GetPanelByType(BasePanel.PanelType.Dialogue) as DialoguePanel;

        dialoguePanel.OpenAndShow(dialogue);

        yield return new WaitForSeconds(3f);

        dialoguePanel.HideDialoge();

        if(effects != null) {
            ActivateAllEffects();
        }
        else {
            Debug.Log("Effects was null");
        }

        if(effects.Count > 0) {
            Debug.Log("more then 0 effects");
        }
        else {
            Debug.Log(" 0 effects");
        }

    }

    private void ActivateAllEffects() {

        Debug.Log("Activating all effects");

        int count = effects.Count;

        for (int i = 0; i < count; i++) {
            effects[i].Activate();
        }
    }

}
