using UnityEngine;
using System.Collections;

/// <summary>
///     Classe que controla o progresso do game
/// </summary>
public class GameController : MonoBehaviour {

    #region SINGLETON
    private static GameController _instance;
    public static GameController Instance
    {
        get{
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<GameController>();
            return _instance;
        }
    }
    #endregion

    public static GameController controller;

    public int coins;
    public int health;
    public int population;

    public float seconds;


}
