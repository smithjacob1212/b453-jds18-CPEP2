using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class ColorCoolDown
{
    [SerializeField] private float pinkCooldownTime; // Pink cooldown time
    [SerializeField] private float cyanCooldownTime; // Cyan cooldown time
    [SerializeField] private float orangeCooldownTime; // Orange cooldown time

    // Time when the next color change for each color can occur.
    private float _nextPinkColorChangeTime; 
    private float _nextCyanColorChangeTime; 
    private float _nextOrangeColorChangeTime; 

    public bool IsPinkCoolingDown => Time.time < _nextPinkColorChangeTime; // Indicates whether the pink color is cooling down.
    public bool IsCyanCoolingDown => Time.time < _nextCyanColorChangeTime; // Indicates whether the cyan color is cooling down.
    public bool IsOrangeCoolingDown => Time.time < _nextOrangeColorChangeTime; // Indicates whether the orange color is cooling down.

    public void StartPinkCoolDown() => _nextPinkColorChangeTime = Time.time + pinkCooldownTime; // Sets the next color change time for pink color.
    public void StartCyanCoolDown() => _nextCyanColorChangeTime = Time.time + cyanCooldownTime; // Sets the next color change time for cyan color.
    public void StartOrangeCoolDown() => _nextOrangeColorChangeTime = Time.time + orangeCooldownTime; // Sets the next color change time for orange color.

}
