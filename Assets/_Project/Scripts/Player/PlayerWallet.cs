using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    public int Gold { get; private set; }

    private void Awake()
    {
        Gold = 0;
    }

    public void GainGold(int value)
    {
        Gold += value;
    }
}