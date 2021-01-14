using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    private Action checkInputAction;
    private Vector2 currentPointerPos = Vector2.zero;

    private float lastTimePressed = 0f;

    private Action<Vector2> OnPointerPressed;
    private Action<Vector2> OnPointerHold;
    private Action<Vector2, float> OnPointerUp;

    void Awake()
    {
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
                case TouchPhase.Moved:
                    PointerHold();
                    break;
                case TouchPhase.Ended:
                    PointerRelease();
                    break;
            }
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
            PointerHold();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            PointerRelease();
        }
    }

    public void AddListener(IInputListener inputListener)
    {
        OnPointerHold += inputListener.OnHold;
        OnPointerPressed += inputListener.OnPressed;
        OnPointerUp += inputListener.OnReleased;
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
}
