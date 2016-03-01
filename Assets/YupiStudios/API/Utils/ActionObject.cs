using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Reflection;


namespace YupiStudios.API.Utils {


	/*
	 * Enumeracao do tipo de acao
	 */
	public enum EActionType
	{
		None, // Vazio
		Quit, // Encerra a aplicaçao
		SetTimeScale, // Muda timeScale do jogo
		CallEvent, // Evento Nativo Unity
		LoadScene // Jump para uma nova cena
	}

	/*
	 * Objeto util para armazenar uma açao
	 * a ser realizada, configuravel via 
	 * inspector.
	 */
	[System.Serializable]
	public class ActionObject {



		//////////////////////////////////
		/// Tipo da açao (ver EActionType)
		////////////////////////////////////
		public EActionType actionType;



		//////////////////////////////////
		/// Valor do timescale 
		/// acao do tipo SetTimeScale
		////////////////////////////////////
		public float timeScaleValue;




		//////////////////////////////////
		/// Cena a ser carregada para a acao  
		/// LoadScene
		////////////////////////////////////
		public string scene;




		//////////////////////////////////
		/// Evento Unity para ser chamado 
		/// pelo tipo CallEvent
		////////////////////////////////////
		public UnityEvent gameEvent;





		private void CallEvent()
		{
			if (gameEvent != null)
				gameEvent.Invoke ();
		}


		/// <summary>
		/// Executa a acao
		/// </summary>
		public void DoAction()
		{
			switch (actionType)
			{
			case EActionType.Quit:
				Application.Quit();
				break;
			case EActionType.SetTimeScale:
				Time.timeScale = timeScaleValue;
				break;
			case EActionType.LoadScene:
				Application.LoadLevel(scene);
				break;
			case EActionType.CallEvent:
				CallEvent();
				break;
			default:
				break;
			}
		}

	}


}