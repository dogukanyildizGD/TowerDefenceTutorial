using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurrentBluePrint
{
    public GameObject prefab;
    public int cost;

    public GameObject upgradePrefab;
    public int upgradeCost;

    public int GetSellAmount()
    {
        return cost / 2;
    }
}
