using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Car", menuName = "ScriptableObjects/Car/Car", order = 1)]

[System.Serializable]
public class WheelObject
{
    [Header("Transform")]
    public Vector3 position;
    public Quaternion rotation;

    [Space]

    [Header("Info")]
    public bool canTurn;
    public bool isPowered;
}
public class CarInfo : ScriptableObject
{
    [Header("Stats")]
    [Range(0f, 10f)]
    public int speed;
    [Range(0f, 10f)]
    public int acceleration;
    [Space]
    [Header("customization")]
    public WheelObject[] wheels;
    public Color carColor;
}
