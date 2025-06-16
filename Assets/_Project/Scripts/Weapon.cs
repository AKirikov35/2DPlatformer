using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [field: SerializeField, Range(0.5f, 2f)] public float Range { get; private set; }

    [field: SerializeField, Range(5, 20)] public int Damage { get; private set; }
}