using UnityEngine;
using System.Collections;

public class UIInGameController : MonoBehaviour {

	//BUTTONS
	public GameObject uiFrontPanel;
	public GameObject uiBuildButton;
	public GameObject uiFinishBuildButton;
	public GameObject uiBackBuildButon;
	public GameObject buildBackground;
	public GameObject uiBuildingOptions;


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
	}

	public void ShowBuildingDescription(){
		//ativa o panel
		uiBuildingOptions.SetActive (true);
		//pega as informaçoes da cunstrucao
		//ativa a animaçao
		openBuildingOptions.SetBool ("ShowDescribe", true);
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


	public void ChangeSprite(GameObject obj){

	}

	public void ChooseStatisticsSubmenu(){
		Debug.Log ("Teste");
	}
	#endregion
}
