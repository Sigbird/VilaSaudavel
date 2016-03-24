using UnityEngine;
using System.Collections;

namespace YupiStudios.VilaSaudavel.Tiles.Buildings {

	public class TileBuildingInfo : MonoBehaviour {


		public bool contaminada;
		public float percentage;
		private bool month;
		private float timer;
		private float monthcont;
		public Animator notifications;



		public enum EBuildingType {

			SimpleHouse,
			HealthCenter, // posto de saude
			Hospital
		}


		public EBuildingType buildingType;


		public TileObject tileObject;

		void Update(){

			this.percentage = Manager.Pop;

			timer = timer + Time.deltaTime;

			if (timer >= 5) {
				monthcont++;
				if(this.buildingType == EBuildingType.SimpleHouse && tileObject.CurrentState == TileObject.ETileObjectState.Placed){
					Teste();
				}
				timer = 0;
			}
			
			if (monthcont >= 30) {
				monthcont = 0;
				month = true;
			}

			if (this.buildingType == EBuildingType.SimpleHouse && this.month == true) {
				notifications.SetTrigger("Paying");
				Manager.Cash = Manager.Cash + 120;
				this.month = false;
				// roda animaçao de dinheiro
			}
		}

		public void Teste(){
			
			float x = Random.value;
			
			if (x <= percentage / 100 && this.contaminada == false) {
				//Debug.Log ("contaminou");
				notifications.SetBool("Sick", true);
				Manager.Health = Manager.Health - 20;
				//GameObject.Find ("Main Camera").GetComponent<Teste> ().infeccão++;
				this.contaminada = true;
			}
			
		}

	}

}