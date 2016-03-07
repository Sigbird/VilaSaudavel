using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour {

	public GameObject[] buttons;
	private GameObject selectedButton;
	private ButtonProperties currentButtonProp;

	void Start(){
		selectedButton = buttons [0];
		currentButtonProp = selectedButton.GetComponent<ButtonProperties> ();
		currentButtonProp._isSelected = true;
		currentButtonProp.ChangeSprite ();
		currentButtonProp.ActiveSubmenu ();
	}

	
	public void UpdateButtons(GameObject button){

		selectedButton = button;
		currentButtonProp = selectedButton.GetComponent<ButtonProperties> ();
		currentButtonProp._isSelected = true;
		currentButtonProp.ChangeSprite ();
		currentButtonProp.ActiveSubmenu ();

		for(int i = 0; i < buttons.Length; i++){
			if(!buttons[i].Equals(selectedButton)){
				ButtonProperties properties = buttons[i].GetComponent<ButtonProperties>();
				properties._isSelected = false;
				properties.ChangeSprite();
				properties.DeactiveSubmenu();
			}

			if(!selectedButton.Equals(buttons[i])){
				ButtonProperties properties = buttons[i].GetComponent<ButtonProperties>();
				properties._isSelected = false;
				properties.ChangeSprite();
				properties.DeactiveSubmenu();
			}
		}



	}

}
