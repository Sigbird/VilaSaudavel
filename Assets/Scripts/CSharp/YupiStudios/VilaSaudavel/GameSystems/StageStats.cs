using UnityEngine;
using System.Collections;

namespace YupiStudios.VilaSaudavel.GameSystems {

	/*
	 * Classe contendo statisticas do .
	 * 
	 */
	public class StageStats : MonoBehaviour {



		////////////////////////////////////////////////
		/// Wealthy
		/// Dinheiro atual que pode ser usado pelo player.
		////////////////////////////////////////////////
		public float Coins { get; set; }
		public float StartCoins; // Stage Start Coins



		////////////////////////////////////////////////
		/// Status da saude da populaçao (em porcentagem)
		////////////////////////////////////////////////
		public float Health { get; set; }
		[Range(0,1)]
		public float StartHealth;// Stage Start Health



		////////////////////////////////////////////////
		/// Total da populaçao
		////////////////////////////////////////////////
		public int Population { get; set;}
		public int StartPopulation;// Stage Start Population



		////////////////////////////////////////////////
		/// Objeto com informaçoes das doenças da fase
		////////////////////////////////////////////////
		public DeseaseStats DeseaseStats;



		// Use this for initialization
		void Start () {
			Coins = StartCoins;
			Health = StartHealth;
			Population = StartPopulation;
		}

	}

}