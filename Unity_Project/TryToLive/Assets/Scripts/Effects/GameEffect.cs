using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameEffect {

    public enum EffectType {
        None = 0,
        SpawnObject = 1,
        KillPlayer = 2,
        VentAir = 3,
        HoldBreath = 4,

    }

    public EffectType effectType;

    public Action onCleanUp;
    public Action onActivate;
    public float delay;


    public GameEffect(EffectType effectType, float delay = 3f, Action onActivate = null, Action onCleanUp = null) {
        this.effectType = effectType;
        this.delay = delay;

        if (onActivate != null) {
            this.onActivate = onActivate;
        }

        if (onCleanUp != null) {
            this.onCleanUp = onCleanUp;
        }

    }

    public virtual void Activate() {
        Debug.Log("Activating an Effect");

        if (onActivate != null) {
            onActivate();
            Debug.Log("Activating a callback");
        }
        else {
            Debug.Log("Callback was null");
        }
    }




    public virtual void CleanUp() {
        if (onCleanUp != null) {
            onCleanUp();
        }
    }



}
