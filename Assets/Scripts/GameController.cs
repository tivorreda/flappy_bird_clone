using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameControllerObject = new GameObject("Game Controller");
                instance = gameControllerObject.AddComponent<GameController>();
            }
            return instance;
        }
    }

    [SerializeField] private BooleanValueData isBirdDead;
    [SerializeField] private BooleanValueData isGamePaused;

    public event Action OnResetHandler;
   
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;

        if (instance != this)
            Destroy(this.gameObject);

        isBirdDead.value = false;
        isGamePaused.value = true;
    }

    private void Update()
    {
        // PLACEHOLDER BEHAVIOUR
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isGamePaused)
                UnpauseGame();
            else
                PauseGame();
        }

        // PLACEHOLDER BEHAVIOUR
        if (Input.GetKeyDown(KeyCode.R))
        {
            isBirdDead.value = false;
            isGamePaused.value = true;
            OnResetHandler?.Invoke();
        }

        // PLACEHOLDER BEHAVIOUR
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void PauseGame()
    {
        isGamePaused.value = true;
    }

    public void UnpauseGame()
    {
        isGamePaused.value = false;
    }
}
