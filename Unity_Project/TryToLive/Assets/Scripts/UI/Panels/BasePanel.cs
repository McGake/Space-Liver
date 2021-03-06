﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BasePanel : MonoBehaviour {

    public enum PanelType {
        InGameMenu = 0,
        Inventory = 1,
        Map = 2,
        Actions = 3,
        Dialogue = 4,
        O2 = 5,
    }

    [Header("Panel Type")]
    public PanelType panelType;

    [Header("Tweens")]
    public DOTweenAnimation enteranceTween;


    public bool IsOpen { get; private set; }

    protected PanelManager panelManager;


    public virtual void Initialize(PanelManager panelManager) {
        this.panelManager = panelManager;

        panelManager.allPanels.Add(this);
    }

    public void Toggle() {
        if (IsOpen)
            Close();
        else
            Open();
    }

    public virtual void Open() {
        if(enteranceTween != null)
            enteranceTween.DOPlayForward();

        IsOpen = true;
    }

    public virtual void Close() {
        if(enteranceTween != null)
            enteranceTween.DOPlayBackwards();

        IsOpen = false;

    }
}
