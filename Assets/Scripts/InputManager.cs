using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;

    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameControllerObject = new GameObject("InputManager");
                instance = gameControllerObject.AddComponent<InputManager>();
            }
            return instance;
        }
    }

    private Action checkInputAction;

    private Vector2 currentPointerPos = Vector2.zero;
    private Vector2 lastFramePos = Vector2.zero;

    private float lastTimePressed = 0f;

    public Action<Vector2> OnPointerPressed;
    public Action<Vector2> OnPointerHold;
    public Action<Vector2, Vector2> OnPointerMoved;
    public Action<Vector2, float> OnPointerUp;

    void Awake()
    {
        if (instance == null)
            instance = this;

        if (instance != this)
            Destroy(this.gameObject);

#if UNITY_ANDROID || UNITY_IOS
        checkInputAction = CheckTouchInput;
#endif
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_EDITOR_WIN || UNITY_EDITOR_OSX
        checkInputAction = CheckMouseInput;
#endif  
    }

    // Update is called once per frame
    void Update()
    {
        checkInputAction.Invoke();
    }

    public void CheckTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            currentPointerPos = touch.position;
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    PointerPress();
                    break;
                case TouchPhase.Stationary:
                    PointerMoved();
                    break;
                case TouchPhase.Moved:
                    PointerHold();
                    break;
                case TouchPhase.Ended:
                    PointerRelease();
                    break;
            }
            lastFramePos = touch.position;
        }
    }

    public void CheckMouseInput()
    {
        currentPointerPos = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            PointerPress();
        }
        else if (Input.GetMouseButton(0))
        {
            if (Vector2.Distance(currentPointerPos, lastFramePos) > 0f)
            {
                PointerHold();
            }
            else
            {
                PointerMoved();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            PointerRelease();
        }
        lastFramePos = Input.mousePosition;
    }

    private void PointerPress()
    {
        lastTimePressed = Time.time;
        OnPointerPressed?.Invoke(currentPointerPos);
    }

    private void PointerRelease()
    {
        float holdTime = Time.time - lastTimePressed;
        OnPointerUp?.Invoke(currentPointerPos, holdTime);
    }

    private void PointerHold()
    {
        OnPointerHold?.Invoke(currentPointerPos);
    }

    private void PointerMoved()
    {
        OnPointerMoved?.Invoke(lastFramePos, currentPointerPos);
    }
}
