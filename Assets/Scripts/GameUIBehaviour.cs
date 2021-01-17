using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI deadText;
    [SerializeField] private TextMeshProUGUI pauseText;

    [SerializeField] private BooleanValueData isBirdDead;
    [SerializeField] private BooleanValueData isGamePaused;

    private void Start()
    {
        isBirdDead.AddOnValueChangeListener(OnIsBirdDeadValueChangedListener);
        isGamePaused.AddOnValueChangeListener(OnIsGamePausedChangedListener);
    }

    private void OnBirdDeathListener()
    {
        deadText.gameObject.SetActive(true);
    }

    private void OnIsBirdDeadValueChangedListener(bool value)
    {
        deadText.gameObject.SetActive(value);
        if (!value)
            pauseText.gameObject.SetActive(true);
    }

    private void OnIsGamePausedChangedListener(bool value)
    {
        pauseText.gameObject.SetActive(value);
    }
}
