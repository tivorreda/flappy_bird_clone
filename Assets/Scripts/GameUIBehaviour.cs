using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI deadText;
    [SerializeField] private TextMeshProUGUI pauseText;

    private void Start()
    {
        GameController.Instance.OnResetHandler += OnResetListener;
        GameController.Instance.OnPauseHandler += OnPauseListener;
        GameController.Instance.OnBirdDeathHandler += OnBirdDeathListener;
    }

    private void OnPauseListener(bool isPaused)
    {
        if (!deadText.gameObject.activeInHierarchy || pauseText.gameObject.activeInHierarchy)
        {
            pauseText.gameObject.SetActive(isPaused);
        }
    }

    private void OnBirdDeathListener()
    {
        deadText.gameObject.SetActive(true);
    }

    private void OnResetListener()
    {
        deadText.gameObject.SetActive(false);
        pauseText.gameObject.SetActive(true);
    }
}
