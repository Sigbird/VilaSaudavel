using UnityEngine;
using System.Collections;

namespace YupiStudios.VilaSaudavel.Tiles.Buildings {

	public class TileBuildingInfo : MonoBehaviour {

		private bool month;
		private float timer;
		private float monthcont;

		public enum EBuildingType {

			SimpleHouse,
			HealthCenter, // posto de saude
			Hospital
		}


		public EBuildingType buildingType;


		public TileObject tileObject;

		void Update(){

			timer = timer + Time.deltaTime;

			if (timer >= 5) {
				monthcont++;
				timer = 0;
			}
			
			if (monthcont >= 30) {
				monthcont = 0;
				month = true;
			}

			if (this.buildingType == EBuildingType.SimpleHouse && this.month == true) {
				Manager.Cash = Manager.Cash + 20;
				this.month = false;
				// roda animaçao de dinheiro
			}
		}

	}

}