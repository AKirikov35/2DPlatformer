using System;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField, Range(0.5f, 2f)] private float _range;
    [SerializeField, Range(5, 20)] private int _damage;

    public float Range { get; private set; }
    public int Damage { get; private set; }

    private void Awake()
    {
        Range = _range;
        Damage = _damage;
    }
}