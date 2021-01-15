using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BirdBehaviour : MonoBehaviour, IDamageable
{
    private const float gravity = 10f;
    private const float flapImpulse = 6f;

    private CharacterController charController;

    private Vector3 velocity = Vector3.zero;
    private Vector3 startPosition;

    private bool shouldMove;

    public event Action OnReceiveDamage;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        charController = GetComponent<CharacterController>();

        InputManager.Instance.OnPointerUp += OnReleased;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMove)
        {
            ApplyGravity();
            charController.Move(velocity * Time.deltaTime);
        }
    }

    private void ApplyGravity()
    {
        velocity -= Vector3.up * gravity * Time.deltaTime;
    }

    private void ApplyTapImpulse()
    {
        if (shouldMove)
        {
            velocity = Vector3.up * flapImpulse;
        }
    }

    public void DoDamage(float damage)
    {
        shouldMove = false;
        OnReceiveDamage?.Invoke();
    }

    public void OnReleased(Vector2 pointerPos, float pressedTime)
    {
        ApplyTapImpulse();
    }

    public void OnPauseListened(bool isPaused)
    {
        shouldMove = !isPaused;
    }

    public void OnResetListened()
    {
        shouldMove = false;
        transform.position = startPosition;
        velocity = Vector3.zero;
    }
}
