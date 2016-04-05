﻿using UnityEngine;
using System.Collections;

public class UIInGameController : MonoBehaviour {

	//BUTTONS
	public GameObject uiFrontPanel;
	public GameObject uiBuildButton;
	public GameObject uiFinishBuildButton;
	public GameObject uiBackBuildButon;
	public GameObject buildBackground;
	public GameObject uiBuildingOptions;
	public GameObject uiBuildingOptions2;
	public GameObject uiBuildingOptions3;
	public GameObject uiBuildingOptions4;
	public GameObject uiBuildingOptions5;
	public GameObject uiBuildingOptions6;
	public GameObject uiBuildingOptions7;
	public GameObject uiBuildingOptions8;
	public GameObject uiBuildingOptions9;


	//STATISTICS_UI


	//BACKGROUND
	public GameObject buildBG;

	//ANIMATIONS
	public Animator openBuildOptions;
	public Animator openBuildingOptions;


	void Start(){
		
	}

    #region BUILDMENU
    public void ActiveBuildMode(){
		uiBuildButton.SetActive(false);				//Desativa o botao do menu de contruir
		uiBackBuildButon.SetActive(true);			//Ativa o botao de voltar
		buildBackground.SetActive (true);			//Ativa o background
		buildBG.SetActive (true);					//Ativa o panel background (tela cinza de fundo)
		openBuildOptions.SetTrigger ("Pressed");	//Dispara o trigger da animaçao
	}

	public void DeactiveBuildMode(){
		uiBackBuildButon.SetActive(false);			//Desativa o botao de voltar
		uiBuildButton.SetActive(true);				//Ativa o botao do menu de construir
		uiFrontPanel.SetActive(true);				//Desativa painel frontal
		buildBackground.SetActive (false);			//Desativa o background
		buildBG.SetActive (false);					//Desativa o panel background (tela cinza de fundo)
		openBuildOptions.SetTrigger ("Pressed");	//Dispara o trigger da animaçao
		openBuildingOptions.SetBool ("ShowDescribe", false);
		uiBuildingOptions.SetActive (false);
		uiBuildingOptions2.SetActive (false);
		uiBuildingOptions3.SetActive (false);
		uiBuildingOptions4.SetActive (false);
		uiBuildingOptions5.SetActive (false);
		uiBuildingOptions6.SetActive (false);
		uiBuildingOptions7.SetActive (false);
		uiBuildingOptions8.SetActive (false);
		uiBuildingOptions9.SetActive (false);
	}

	//METODO PRECISA SER REVISTO
	public void ActiveBuildingOptions(){
		uiBackBuildButon.SetActive(true);			//Ativa o botao de voltar
		uiBuildButton.SetActive(false);				//Desativa botao do menu de construir
		uiFrontPanel.SetActive(false);				//Desativa painel frontal
		uiFinishBuildButton.SetActive(true);		//Ativa o botao de finalizar construcao
		buildBackground.SetActive (false);			//Desativa o background
		buildBG.SetActive (false);					//Desativa o panel background (tela cinza de fundo)
		openBuildOptions.SetTrigger ("Pressed");	//Dispara o trigger da animaçao
		openBuildingOptions.SetBool ("ShowDescribe", false);
		uiBuildingOptions.SetActive (false);
		uiBuildingOptions2.SetActive (false);
		uiBuildingOptions3.SetActive (false);
		uiBuildingOptions4.SetActive (false);
		uiBuildingOptions5.SetActive (false);
		uiBuildingOptions6.SetActive (false);
		uiBuildingOptions7.SetActive (false);
		uiBuildingOptions8.SetActive (false);
		uiBuildingOptions9.SetActive (false);
	}

	public void ShowBuildingDescription(int x){
		//ativa o panel
		switch (x) {
		case 1:
			uiBuildingOptions.SetActive (true);
			uiBuildingOptions2.SetActive (false);
			uiBuildingOptions3.SetActive (false);
			uiBuildingOptions3.SetActive (false);
			uiBuildingOptions4.SetActive (false);
			uiBuildingOptions5.SetActive (false);
			uiBuildingOptions6.SetActive (false);
			uiBuildingOptions7.SetActive (false);
			uiBuildingOptions8.SetActive (false);
			uiBuildingOptions9.SetActive (false);
			break;
		case 2:
			uiBuildingOptions.SetActive (false);
			uiBuildingOptions2.SetActive (true);
			uiBuildingOptions3.SetActive (false);
			uiBuildingOptions3.SetActive (false);
			uiBuildingOptions4.SetActive (false);
			uiBuildingOptions5.SetActive (false);
			uiBuildingOptions6.SetActive (false);
			uiBuildingOptions7.SetActive (false);
			uiBuildingOptions8.SetActive (false);
			uiBuildingOptions9.SetActive (false);
			break;
		case 3:
			uiBuildingOptions.SetActive (false);
			uiBuildingOptions2.SetActive (false);
			uiBuildingOptions3.SetActive (true);
			uiBuildingOptions4.SetActive (false);
			uiBuildingOptions5.SetActive (false);
			uiBuildingOptions6.SetActive (false);
			uiBuildingOptions7.SetActive (false);
			uiBuildingOptions8.SetActive (false);
			uiBuildingOptions9.SetActive (false);

			break;
		case 4:
			uiBuildingOptions.SetActive (false);
			uiBuildingOptions2.SetActive (false);
			uiBuildingOptions3.SetActive (false);
			uiBuildingOptions3.SetActive (false);
			uiBuildingOptions4.SetActive (true);
			uiBuildingOptions5.SetActive (false);
			uiBuildingOptions6.SetActive (false);
			uiBuildingOptions7.SetActive (false);
			uiBuildingOptions8.SetActive (false);
			uiBuildingOptions9.SetActive (false);
			break;
		case 5:
			uiBuildingOptions.SetActive (false);
			uiBuildingOptions2.SetActive (false);
			uiBuildingOptions3.SetActive (false);
			uiBuildingOptions3.SetActive (false);
			uiBuildingOptions4.SetActive (false);
			uiBuildingOptions5.SetActive (true);
			uiBuildingOptions6.SetActive (false);
			uiBuildingOptions7.SetActive (false);
			uiBuildingOptions8.SetActive (false);
			uiBuildingOptions9.SetActive (false);
			break;
		case 6:
			uiBuildingOptions.SetActive (false);
			uiBuildingOptions2.SetActive (false);
			uiBuildingOptions3.SetActive (false);
			uiBuildingOptions3.SetActive (false);
			uiBuildingOptions4.SetActive (false);
			uiBuildingOptions5.SetActive (false);
			uiBuildingOptions6.SetActive (true);
			uiBuildingOptions7.SetActive (false);
			uiBuildingOptions8.SetActive (false);
			uiBuildingOptions9.SetActive (false);
			break;
		case 7:
			uiBuildingOptions.SetActive (false);
			uiBuildingOptions2.SetActive (false);
			uiBuildingOptions3.SetActive (false);
			uiBuildingOptions3.SetActive (false);
			uiBuildingOptions4.SetActive (false);
			uiBuildingOptions5.SetActive (false);
			uiBuildingOptions6.SetActive (false);
			uiBuildingOptions7.SetActive (true);
			uiBuildingOptions8.SetActive (false);
			uiBuildingOptions9.SetActive (false);
			break;
		case 8:
			uiBuildingOptions.SetActive (false);
			uiBuildingOptions2.SetActive (false);
			uiBuildingOptions3.SetActive (false);
			uiBuildingOptions3.SetActive (false);
			uiBuildingOptions4.SetActive (false);
			uiBuildingOptions5.SetActive (false);
			uiBuildingOptions6.SetActive (false);
			uiBuildingOptions7.SetActive (false);
			uiBuildingOptions8.SetActive (true);
			uiBuildingOptions9.SetActive (false);
			break;
		case 9:
			uiBuildingOptions.SetActive (false);
			uiBuildingOptions2.SetActive (false);
			uiBuildingOptions3.SetActive (false);
			uiBuildingOptions3.SetActive (false);
			uiBuildingOptions4.SetActive (false);
			uiBuildingOptions5.SetActive (false);
			uiBuildingOptions6.SetActive (false);
			uiBuildingOptions7.SetActive (false);
			uiBuildingOptions8.SetActive (false);
			uiBuildingOptions9.SetActive (true);
			break;
		default:
			print ("uiBuildOption Unavaiable");
			break;
		
		}
		//pega as informaçoes da cunstrucao
		//ativa a animaçao
//		openBuildingOptions.SetBool ("ShowDescribe", true);
	}
    #endregion

    #region STATISTICS_UI
    public void ActiveStatisticsMode(){
		//Ativa o Panel Background (cinza)
		//Ativa anomaçao da janela
		ClickStatisticsOption ();			//Inicia com a opçao 'moedas' selecionada
		//
	}

	public void DeactiveStatisticsMode(){

	}

	public void ClickStatisticsOption(){
		//Muda o sprite do botao
		//Começa com o 'moedas'

	}


	public void ChangeVolume(float x){
	
		AudioListener.volume = x;

	}

	public void ChangeSprite(GameObject obj){

	}

	public void ChooseStatisticsSubmenu(){
		Debug.Log ("Teste");
	}
	#endregion
}
