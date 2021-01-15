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

    [SerializeField] private GameObject birdObjectPrefab;

    private bool isGamePaused;
    private bool isBirdDead;

    private event Action<bool> _OnPauseHandler;
    public event Action<bool> OnPauseHandler
    {
        add
        {
            _OnPauseHandler += value;
            value?.Invoke(isGamePaused);
        }

        remove
        {
            _OnPauseHandler -= value;
        }
    }

    public event Action OnResetHandler;
    public event Action OnBirdDeathHandler;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;

        if (instance != this)
            Destroy(this.gameObject);

        isGamePaused = true;
        isBirdDead = false;

        InitBird();
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
            isBirdDead = false;
            isGamePaused = true;
            OnResetHandler?.Invoke();
        }

        // PLACEHOLDER BEHAVIOUR
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void InitBird()
    {
        BirdBehaviour bird = Instantiate(birdObjectPrefab, Vector3.zero, Quaternion.Euler(Vector3.zero), null).GetComponent<BirdBehaviour>();
        OnPauseHandler += bird.OnPauseListened;
        OnResetHandler += bird.OnResetListened;
        bird.OnReceiveDamage += OnBirdDeath;
    }

    public void PauseGame()
    {
        isGamePaused = true;
        _OnPauseHandler?.Invoke(isGamePaused);
    }

    public void UnpauseGame()
    {
        isGamePaused = false;
        _OnPauseHandler?.Invoke(isGamePaused);
    }

    public void OnBirdDeath()
    {
        isBirdDead = true;
        OnBirdDeathHandler?.Invoke();
    }
}
