
using System.Collections.Generic;
using System;

public class Stat 
{
    public StatType Type { get; private set; }
    public float Value { get; private set; }
    public float BaseValue { get; private set; }

    public event Action<float> Changed;

    private List<(object source, float value)> _mods = new();

    public Stat (StatType type, float value, float baseValue)
    {
        Type = type;
        Value = value;
        BaseValue = baseValue;
    }

    public void AddModifier(object source, float value)
    {
        _mods.Add((source, value));
        Recalculate();
    }

    public void RemoveFrom(object source)
    {
        _mods.RemoveAll(m => m.source == source);
        Recalculate();
    }

    private void Recalculate()
    {
        float value = BaseValue;
        foreach (var mod in _mods)
        {
            value += mod.value;
        }
        Value = value;
        Changed?.Invoke(value);
    }
}

public enum StatType
{
    Health = 0,
    Stamina = 1,
    Armour = 2,
    Damage = 3,
    Speed = 4
}