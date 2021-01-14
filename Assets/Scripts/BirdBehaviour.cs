using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BirdBehaviour : MonoBehaviour
{
    private const float gravity = 10f;
    private const float flapImpulse = 6f;

    private Vector3 velocity = Vector3.zero;
    private CharacterController charController;
   
    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();        
    }

    // Update is called once per frame
    void Update()
    {
        ApplyGravity();
        charController.Move(velocity * Time.deltaTime);
    }

    public void OnTap(Vector2 mousePos, float tapDuration)
    {
        ApplyTapImpulse();
    }

    private void ApplyGravity()
    {
        velocity -= Vector3.up * gravity * Time.deltaTime;
    }

    private void ApplyTapImpulse()
    {
        velocity = Vector3.up * flapImpulse;
    }
}
