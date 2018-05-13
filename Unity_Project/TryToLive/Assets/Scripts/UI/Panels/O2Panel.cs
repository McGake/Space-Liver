using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class O2Panel : BasePanel {

    public float maxO2;
    public float currentO2;

    public Image o2Fill;

    private Timer o2Timer;

    private bool active = true;

    private void Start() {
        o2Timer = new Timer("", 0.1f, true, DecreaseO2, UpdateFillBar);
    }

    public override void Initialize(PanelManager panelManager) {
        base.Initialize(panelManager);

        Open();
    }

    public void VentO2() {
        o2Timer.ModifyDuration(-0.08f);
    }

    public void Change02Speed(float spChange = -.1f)
    {
        o2Timer.ModifyDuration(spChange);
    }

    private void Update() {
        if(active && o2Timer != null) {
            o2Timer.UpdateClock();
        }

        if (Input.GetKeyDown(KeyCode.V)) {
            VentO2();
        }
    }


    public void DecreaseO2() {
        currentO2 -= 1f;

        if(currentO2 <= 0) {
            active = false;
            EffectFactory.KillPlayer();
        }
    }

    public void UpdateFillBar() {
        float ratio = currentO2 / maxO2;

        o2Fill.fillAmount = ratio;
    }



}
