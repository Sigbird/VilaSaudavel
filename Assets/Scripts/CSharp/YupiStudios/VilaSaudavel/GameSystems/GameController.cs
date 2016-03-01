using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
///     Classe que controla o progresso do game
/// </summary>
public class GameController : MonoBehaviour {

    public static GameController controller;

    public int coins;
    public int health;
    public int population;


    void Awake()
    {
        if(controller == null)
        {
            controller = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(controller != this)
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/villageState.dat");

        VillageState state = new VillageState();
        state.coins = coins;
        state.health = health;
        state.population = population;

        bf.Serialize(file, state);
        file.Close();

    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/villageState.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "villageState.dat", FileMode.Open);
            VillageState state = (VillageState) bf.Deserialize(file);
            file.Close();

            coins = state.coins;
            health = state.health;
            population = state.population;
        }
    }

}

[Serializable]
class VillageState{
    public int coins;
    public int health;
    public int population;
}

