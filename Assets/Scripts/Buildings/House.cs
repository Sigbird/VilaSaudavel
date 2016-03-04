using UnityEngine;
using System.Collections;

public class House : Building {

    public static int CITZENS = 2;
    private int GeneratedRisk;

    #region COINS_VARIABLES
    public int coinsToGen;
    public float coinsPerSec;
    #endregion

    public bool isAffected;

    void Start()
    {
        coinsToGen = 2;
        coinsPerSec = 1;
    }

    public void GenerateDisease() { }

    /*Gerar 2 moedas por segundos*/
    void GenerateCoins()
    {
        
    }
}
