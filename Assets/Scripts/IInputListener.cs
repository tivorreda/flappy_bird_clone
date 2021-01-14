using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputListener
{
    void OnPressed(Vector2 pointerPos);
    void OnReleased(Vector2 pointerPos, float pressedTime);
    void OnHold(Vector2 pointerPos);
}
