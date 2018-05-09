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

    public override void Initialize(PanelManager panelManager) {
        base.Initialize(panelManager);

        Open();

        PopulateActions();
    }

    public void PopulateActions() {
        for (int i = 0; i < 2; i++) {
            int randomEntry = Random.Range(0, actionsDatabase.actionData.Count);

            GameObject newEntry = Instantiate(actionEntryTemplate.gameObject) as GameObject;
            newEntry.transform.SetParent(actionEntryHolder, false);
            newEntry.SetActive(true);

            ActionEntry entryScript = newEntry.GetComponent<ActionEntry>();

            entryScript.Initialize(actionsDatabase.actionData[randomEntry]);


        }
    }

}
