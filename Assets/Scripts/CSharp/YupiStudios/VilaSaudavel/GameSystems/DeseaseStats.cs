using UnityEngine;
using System.Collections;


namespace YupiStudios.VilaSaudavel.GameSystems {

	/*
	 * Statisticas de doencas
	 * 
	 */
	public class DeseaseStats : MonoBehaviour {

		/*Permite Dengue na fase*/
		public bool AllowDengue;

		/*Permite Obesidade na fase*/
		public bool AllowFatness;



		public float DenguePerSec { get; set; }
		public float FatnessPerSec { get; set; }

	}


}