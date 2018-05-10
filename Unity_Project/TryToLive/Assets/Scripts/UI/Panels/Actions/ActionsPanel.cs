using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActionsPanel : BasePanel {

    public ActionsDatabse actionsDatabase;
    public ActionEntry actionEntryTemplate;
    public RectTransform actionEntryHolder;

    private List<ActionEntry> actions = new List<ActionEntry>();

    public Sprite[] spriteButtons = new Sprite[4];

    public override void Initialize(PanelManager panelManager) {
        base.Initialize(panelManager);

        Open();

        PopulateActions();
    }

    void Update()
    {
        CheckInput();  
    }

    private void CheckInput()
    {
        if (Input.GetButtonDown("Action1"))
        {
            actions[0].OnClick();
        }
        else if (Input.GetButtonDown("Action2"))
        {
            Debug.Log("Action 2");
            actions[1].OnClick();
        }
        else if (Input.GetButtonDown("Action3"))
        {
            Debug.Log("Action 2");
            actions[2].OnClick();
        }
        else if (Input.GetButtonDown("Action4"))
        {
            actions[3].OnClick();
        }
    }

    public void PopulateActions() {
        for (int i = 0; i < 4; i++) {
            int randomEntry = Random.Range(0, actionsDatabase.actionData.Count);

            for(int x = 0; x < actions.Count; x++)
            {
                Debug.Log(actions[x].actionNameText.text + " " + actionsDatabase.actionData[randomEntry].actionName);
                if (actions[x].actionNameText.text == actionsDatabase.actionData[randomEntry].actionName)
                {
                    Debug.Log("messed up here");
                    randomEntry = Random.Range(0, actionsDatabase.actionData.Count);
                    x = 0;
                }
            }
            
            GameObject newEntry = Instantiate(actionEntryTemplate.gameObject) as GameObject;
            newEntry.transform.SetParent(actionEntryHolder, false);
            newEntry.GetComponent<Image>().sprite = spriteButtons[i];
            newEntry.SetActive(true);

            ActionEntry entryScript = newEntry.GetComponent<ActionEntry>();

            entryScript.Initialize(actionsDatabase.actionData[randomEntry]);
            actions.Add(entryScript);

        }
    }

}
