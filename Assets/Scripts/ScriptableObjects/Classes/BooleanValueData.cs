using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new BooleanValue", menuName = "Values/Boolean")]
public class BooleanValueData : ScriptableObject
{
    [SerializeField] private bool _value;

    private Action<bool> OnValueChanged;

    public bool value
    {
        get { return _value; }
        set
        {
            _value = value;
            OnValueChanged?.Invoke(_value);
        }
    }


    public void AddOnValueChangeListener(Action<bool> onValueChangedListener)
    {
        OnValueChanged += onValueChangedListener;
        onValueChangedListener.Invoke(_value);
    }

    void OnValidate()
    {
        OnValueChanged?.Invoke(_value);
    }

}
