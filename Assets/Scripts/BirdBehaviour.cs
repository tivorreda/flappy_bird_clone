using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BirdBehaviour : MonoBehaviour, IInputListener
{
    private const float gravity = 10f;
    private const float flapImpulse = 6f;

    private Vector3 velocity = Vector3.zero;
    private CharacterController charController;

    [SerializeField]
    private InputManager inputManager;
   
    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
        if (inputManager != null)
        {
            inputManager.AddListener(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ApplyGravity();
        charController.Move(velocity * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        velocity -= Vector3.up * gravity * Time.deltaTime;
    }

    private void ApplyTapImpulse()
    {
        velocity = Vector3.up * flapImpulse;
    }

    public void OnPressed(Vector2 pointerPos)
    {
        //TODO
    }

    public void OnReleased(Vector2 pointerPos, float pressedTime)
    {
        ApplyTapImpulse();
    }

    public void OnHold(Vector2 pointerPos)
    {
        //TODO
    }
}
