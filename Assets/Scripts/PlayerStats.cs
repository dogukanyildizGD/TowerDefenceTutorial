using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 400;

    public static int Live = 20;
    public int startLive;

    public static int Rounds;

    private void Start()
    {
        Money = startMoney;
        Live = startLive;

        Rounds = 0;
    }
}
