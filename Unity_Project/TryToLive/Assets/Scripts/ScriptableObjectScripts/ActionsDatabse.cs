using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions Database")]
public class ActionsDatabse : ScriptableObject {


    public List<ActionData> actionData = new List<ActionData>();





}


[System.Serializable]
public class ActionData {
    public string actionName;
    public string dialogue;
    public GameEffect.EffectType testSimpleEffect;
    public float delay = 3f;
}
