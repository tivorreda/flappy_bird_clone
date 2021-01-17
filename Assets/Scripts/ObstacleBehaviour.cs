using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    private const float speed = 3f;

    private Vector3 startPos;

    [SerializeField] private BooleanValueData isGamePaused;
    [SerializeField] private BooleanValueData isBirdDead;

    private bool shouldMove;

    private void Start()
    {
        startPos = transform.position;
        isGamePaused.AddOnValueChangeListener(OnIsGamePausedValueChanged);
        isBirdDead.AddOnValueChangeListener(OnIsBirdDeadValueChanged);
    }

    private void Update()
    {
        if(shouldMove)
            transform.position -= Vector3.right * speed * Time.deltaTime;
    }

    private void OnIsBirdDeadValueChanged(bool value)
    {
        if (!value)
        {
            transform.position = startPos;
        }
    }

    private void OnIsGamePausedValueChanged(bool value)
    {
        shouldMove = !value;
    }
}
