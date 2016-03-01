using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour {

	public GameObject[] buttons;
	private GameObject selectdButton;
	private ButtonProperties currentButtonProp;

	void Start(){
		selectdButton = buttons [0];
		currentButtonProp = selectdButton.GetComponent<ButtonProperties> ();
		currentButtonProp._isSelected = true;
		currentButtonProp.ChangeSprite ();
		currentButtonProp.ActiveSubmenu ();
	}

	
	public void UpdateButtons(GameObject button){

		//COMO VERIFICAR QUAL E O BOTAO???????
		selectdButton = button;
		currentButtonProp = selectdButton.GetComponent<ButtonProperties> ();
		currentButtonProp._isSelected = true;
		currentButtonProp.ChangeSprite ();
		currentButtonProp.ActiveSubmenu ();

		for(int i = 0; i < buttons.Length; i++){
			if(!buttons[i].Equals(selectdButton)){
				ButtonProperties properties = buttons[i].GetComponent<ButtonProperties>();
				properties._isSelected = false;
				properties.ChangeSprite();
				properties.DeactiveSubmenu();
			}

			if(!selectdButton.Equals(buttons[i])){
				ButtonProperties properties = buttons[i].GetComponent<ButtonProperties>();
				properties._isSelected = false;
				properties.ChangeSprite();
				properties.DeactiveSubmenu();
			}
		}



	}

}
