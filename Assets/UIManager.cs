using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image progressBar;

    private float progress;

    private void OnEnable()
    {
        EventManager.UpdateProgress += UpdateProgress;
    }

    private void OnDisable()
    {
        EventManager.UpdateProgress -= UpdateProgress;
    }

    private void UpdateProgress(float progress)
    {
        progressBar.DOFillAmount(progress, .3f);
    }

}
