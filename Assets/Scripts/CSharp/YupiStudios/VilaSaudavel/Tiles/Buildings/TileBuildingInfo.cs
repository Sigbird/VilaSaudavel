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
		public SpriteRenderer renderer;
		

		//ESTATISTICAS
		public int saude;
		public int renda;
		public string descriçao;
		public string info;
		public string status;
		public Sprite ilustracao;



		public enum EBuildingType {

			SimpleHouse,
			HealthCenter, // posto de saude
			Hospital,
			Praca,
			Tratamento
		}


		public EBuildingType buildingType;


		public TileObject tileObject;

		void Start(){
			this.saude = 100;

			if ( this.buildingType == EBuildingType.SimpleHouse)
				this.renda = 120;
		}

		void Update(){

			this.percentage = Manager.Pop;

			if (tileObject.CurrentState == TileObject.ETileObjectState.Placed) {

				timer = timer + Time.deltaTime;

				if (timer >= 5) {
					monthcont++;
					if (this.buildingType == EBuildingType.SimpleHouse) {
						Teste ();
					}
					timer = 0;
				}
			
				if (monthcont >= 30) {
					monthcont = 0;
					month = true;
				}

				if (this.buildingType == EBuildingType.SimpleHouse && this.month == true) {
					Camera.main.GetComponent<AudioController> ().PlayState (AudioController.EAudioState.Payment);
					notifications.SetTrigger ("Paying");
					if (this.contaminada == true) {
						Manager.Cash = Manager.Cash + 60;
					} else {
						Manager.Cash = Manager.Cash + 120;
					}
					this.month = false;
					// roda animaçao de dinheiro
				}

			}
				


			if ( this.buildingType == EBuildingType.Tratamento){
				this.renda = 0;
				//this.saude = (int)Manager.Health;
				this.descriçao = "Estaçao de Tratamento";
				this.status = "Saudável";
				this.info = "'Estaçao para saneamento e manutençao da rede de esgoto da cidade'";
				}else if ( this.buildingType == EBuildingType.Praca){
					this.renda = 0;
					//this.saude = (int)Manager.Health;
					this.descriçao = "Praça";
					this.status = "Saudável";
					this.info = "'Lugar ideal para praticar sua atividade fisica e entrar em forma!'";
				}else if ( this.buildingType == EBuildingType.HealthCenter){
						this.renda = 0;
						//this.saude = (int)Manager.Health;
						this.descriçao = "Posto de Saude";
						this.status = "Saudável";
						this.info = "'Estamos prontos para atender todos os casos.'";
				}else if( this.buildingType == EBuildingType.Hospital){
						this.renda = 0;
						//this.saude = (int)Manager.Health;
						this.descriçao = "Hospital";
						this.status = "Saudável";
						this.info = "'Trataremos qualquer caso de saude que precisar.'";
					}else if ( this.buildingType == EBuildingType.SimpleHouse){
						//this.renda = 120;
						//this.saude = (int)Manager.Health;
						this.descriçao = "Casa";
						if(contaminada){
							this.saude = this.saude/2;
							this.status = "Contaminada";
							this.info = "'Estamos adoentados precisamos de ajuda de um agente de Saúde!'";
						}else{
							this.status = "Saudável";
							this.info = "'Este parece um bom lugar para viver mas precisamos de mais vizinhos.'";
						}
					}



			foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Habitant")) {
				
				if (Vector3.Distance (transform.position, obj.transform.position) <= 1 && obj.name != "Jaleco" && obj.name != "Rebeca" && this.buildingType == EBuildingType.Praca && obj.GetComponent<HabitantMovement>().obeso) {
					obj.GetComponent<HabitantMovement> ().obeso = false;
					StartCoroutine(ReceiveHabitant(obj));
				}

				if (Vector3.Distance (transform.position, obj.transform.position) <= 1 && obj.name != "Jaleco" && obj.name != "Rebeca" && this.buildingType == EBuildingType.SimpleHouse) {
					float x = Random.value;
					if ( x <= 1 && obj.GetComponent<HabitantMovement>().beenInHouse == false) {
						obj.GetComponent<HabitantMovement>().beenInHouse = true;
						StartCoroutine(ReceiveHabitant(obj));
					}
				}

			}

			foreach (GameObject obj in GameObject.FindGameObjectsWithTag("ManHole")) {
				
				if (Vector3.Distance (transform.position, obj.transform.position) <= 2.2f && this.buildingType == EBuildingType.Tratamento) {
					obj.GetComponent<SpriteRenderer> ().enabled = false;
				}
				
			}

			foreach (GameObject x in GameObject.FindGameObjectsWithTag("Jaleco")) {
				
				if(x != null && Vector3.Distance(x.transform.position, transform.position)< 2 && this.contaminada == true){
					this.contaminada = false;
					StartCoroutine(ReceiveHabitant(x));
					notifications.SetBool("Sick", false);
					Manager.Pop = Manager.Pop + 5;
				}
				
			}

		
		}
		public void Teste(){
			
			float x = Random.value;
			this.saude = 100 - (int)percentage ;
			if (x <= percentage / 100 && this.contaminada == false) {
				//Debug.Log ("contaminou");
				notifications.SetBool("Sick", true);
				Manager.Pop = Manager.Pop - 5;
				//GameObject.Find ("Main Camera").GetComponent<Teste> ().infeccão++;
				this.contaminada = true;
				StartCoroutine("Sickness");
				Camera.main.GetComponent<AudioController> ().PlayState (AudioController.EAudioState.Disease);
			}
			
		}

		IEnumerator Sickness(){
			if (contaminada) {
				renderer.color = Color.magenta;
				yield return new WaitForSeconds (1);
				renderer.color = Color.white;
				yield return new WaitForSeconds (1);
				StartCoroutine("Sickness");
			} 
		}



		IEnumerator ReceiveHabitant(GameObject Obj){

			Obj.GetComponent<HabitantMovement> ().speed = 0;
			Obj.GetComponent<HabitantMovement> ().renderer.enabled = false;
				yield return new WaitForSeconds (3);
			Obj.GetComponent<HabitantMovement> ().renderer.enabled = true;
			Obj.GetComponent<HabitantMovement> ().speed = 30;

		}

	}

}