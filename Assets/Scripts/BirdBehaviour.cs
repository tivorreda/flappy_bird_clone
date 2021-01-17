using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BirdBehaviour : MonoBehaviour, IDamageable
{
    private const float gravity = 10f;
    private const float flapImpulse = 5f;
    private const float gravityModifier = 1.7f;

    private CharacterController charController;

    private Vector3 velocity = Vector3.zero;
    private Vector3 startPosition;

    [SerializeField] private BooleanValueData isBirdDead;
    [SerializeField] private BooleanValueData isGamePaused;

    [SerializeField] private GameObject viewHolderObject;

    private bool shouldMove;

    private InputManager inputManager = new InputManager();

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        charController = GetComponent<CharacterController>();

        inputManager.OnPointerUp += OnReleased;
        isGamePaused.AddOnValueChangeListener(OnPauseListened);
        isBirdDead.AddOnValueChangeListener(OnBirdDeadValueChangeListener);
    }

    // Update is called once per frame
    void Update()
    {
        inputManager.CheckInput();
        if (shouldMove)
        {
            ApplyGravity();
            charController.Move(velocity * Time.deltaTime);
        }
    }

    private void ApplyGravity()
    {
        // A trick for better gameplay, gravity is raised while bird is falling
        float gravityToApply = velocity.y < 0 ? gravity * gravityModifier : gravity;
        velocity -= Vector3.up * gravityToApply * Time.deltaTime;
    }

    private void ApplyTapImpulse()
    {
        if (shouldMove)
        {
            velocity = Vector3.up * flapImpulse;
        }
    }

    public void DoDamage()
    {
        shouldMove = false;
        isBirdDead.value = true;
        viewHolderObject.SetActive(false);
    }

    public void OnReleased(Vector2 pointerPos, float pressedTime)
    {
        ApplyTapImpulse();
    }

    public void OnPauseListened(bool isPaused)
    {
        shouldMove = !isPaused;
    }

    public void OnBirdDeadValueChangeListener(bool value)
    {
        if (!value)
        {
            shouldMove = false;
            transform.position = startPosition;
            velocity = Vector3.zero;
            viewHolderObject.SetActive(true);
        }
    }
}
