using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField, Range(0.5f, 2f)] private float _range = 1.5f;
    [SerializeField, Range(5, 20)] private int _damage = 10;

    public float Range => _range;
    public int Damage => _damage;
}