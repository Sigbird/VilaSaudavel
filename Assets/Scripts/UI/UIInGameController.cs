using UnityEngine;
using System.Collections;

public class UIInGameController : MonoBehaviour {

	//BUTTONS
	public GameObject uiBuildButton;
	public GameObject uiBackBuildButon;
	public GameObject buildBackground;
	//public GameObject uiBuildingOptions;

	//BACKGROUND
	public GameObject buildBG;

	//ANIMATIONS
	public Animator openBuildOptions;

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
		buildBackground.SetActive (false);			//Desativa o background
		buildBG.SetActive (false);					//Desativa o panel background (tela cinza de fundo)
		openBuildOptions.SetTrigger ("Pressed");	//Dispara o trigger da animaçao
	}

	void ShowBuildingOptions(){

	}
}
