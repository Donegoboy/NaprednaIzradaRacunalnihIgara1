using System;
using UnityEngine;

public class Resource : Stat
{
    public float Max => Value;
    public float Current {  get; private set; }
    public event Action<float, float> CurrentChanged;
    public event Action Depleated;

    public Resource(StatType type, float value, float baseValue) : base(type, value, baseValue)
    {

    }

    public void Increase(float amount)
    {
        SetCurrent(Current + amount);
    }
    public void Decrease(float amount) 
    {
        SetCurrent(Current - amount);
    }
    public void Refill()
    {
        SetCurrent(Max);
    }
    public bool TryConsume(float amount)
    {
        if (Current < amount)
        {
            return false;
        }
        Decrease(amount);
        return true;
    }

    private void SetCurrent(float value)
    {
        float v = Mathf.Clamp(value, 0, Max);
        if (v == Current)
        {
            return;
        }
        Current = v;
        CurrentChanged?.Invoke(Current, Max);
        if (Current <= 0)
        {
            Depleated?.Invoke();
        }
    }
}


