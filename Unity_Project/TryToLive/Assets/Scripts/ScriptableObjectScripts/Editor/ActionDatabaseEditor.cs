using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ActionsDatabse))]
public class ActionDatabaseEditor : Editor {

    private ActionsDatabse _actionsDatabase;

    public override void OnInspectorGUI() {
        //base.OnInspectorGUI();

        _actionsDatabase = (ActionsDatabse)target;

        _actionsDatabase.actionData = EditorHelper.DrawExtendedList("Actions", _actionsDatabase.actionData, "Action", DrawActionData);


        if (GUI.changed)
            EditorUtility.SetDirty(target);
    }


    private ActionData DrawActionData(ActionData entry) {
        entry.actionName = EditorGUILayout.TextField("Action Name", entry.actionName);
        entry.dialogue = EditorGUILayout.TextField("Dialog", entry.dialogue);


        return entry;
    }

}
