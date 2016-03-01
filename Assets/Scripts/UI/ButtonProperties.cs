using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonProperties : MonoBehaviour {

	#region VARIABLES
	public GameObject thisButton;
	public bool _isSelected;

	public GameObject myWindow;

	private Sprite normalSrpite;
	public Sprite clickedSprite;

	protected ButtonsController buttonsController;
	#endregion

	void Awake(){
		_isSelected = false;
		normalSrpite = thisButton.GetComponent<Image> ().sprite;
		buttonsController = this.gameObject.GetComponentInParent<ButtonsController> ();
	}
	
	public void OnClick(){
		//_isSelected = true;
		buttonsController.UpdateButtons (thisButton);
	}
	
	public void ChangeSprite(){
		if (_isSelected) {
			thisButton.GetComponent<Image> ().sprite = clickedSprite;
		} else {
			thisButton.GetComponent<Image>().sprite = normalSrpite;
		}
	}

	public void ActiveSubmenu(){
		if (_isSelected) {
			myWindow.SetActive (true);
		}
	}

	public void DeactiveSubmenu(){
		if (!_isSelected) {
			myWindow.SetActive (false);
		}
	}
}
