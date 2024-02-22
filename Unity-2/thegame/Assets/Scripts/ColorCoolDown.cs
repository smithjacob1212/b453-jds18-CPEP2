using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class ColorCoolDown
{
    [SerializeField] private float pinkCooldownTime;
    [SerializeField] private float cyanCooldownTime;
    [SerializeField] private float orangeCooldownTime;
    private float _nextPinkColorChangeTime;
    private float _nextCyanColorChangeTime;
    private float _nextOrangeColorChangeTime;

    public bool IsPinkCoolingDown => Time.time < _nextPinkColorChangeTime;
    public bool IsCyanCoolingDown => Time.time < _nextCyanColorChangeTime;
    public bool IsOrangeCoolingDown => Time.time < _nextOrangeColorChangeTime;

    public void StartPinkCoolDown() => _nextPinkColorChangeTime = Time.time + pinkCooldownTime;
    public void StartCyanCoolDown() => _nextCyanColorChangeTime = Time.time + cyanCooldownTime;
    public void StartOrangeCoolDown() => _nextOrangeColorChangeTime = Time.time + orangeCooldownTime;

}
