
using System;
using UnityEngine;

namespace YupiStudios.API.Utils
{

	/*
	 * Diferentes tipos de Input suportados 
	 * pela classe InputType
	 */
	public enum EInputType 
	{
		Any,
		Click,
		Escape,
		KeyCode
	}

	public enum EMouseButton
	{
		Left,
		Right,
		Middle
	}


	/*
	 * Classe estilo Listener,que manipula diferentes 
	 * tipos de Inputs do usuario.
	 */
	[System.Serializable]
	public class InputType
	{

		public EInputType inputType;
		public KeyCode keyCode;
		public EMouseButton mouseButton;


		private bool TestAny()
		{
			return Input.anyKeyDown;
		}

		private bool TestEscape()
		{
			return Input.GetKeyDown(KeyCode.Escape);
		}

		private bool TestClick()
		{
			switch (mouseButton)
			{
			case EMouseButton.Right:
				return Input.GetMouseButtonDown(1);
			case EMouseButton.Middle:
				return Input.GetMouseButtonDown(2);
			default:
				return Input.GetMouseButtonDown(0);
			}
		}

		private bool TestKey()
		{
			return Input.GetKeyDown(keyCode);
		}

		/// <summary>
		/// Tests se o input escolhido foi acionado.
		/// </summary>
		/// <returns><c>true</c>, if input is happening, <c>false</c> otherwise.</returns>
		public bool TestInput()
		{
			switch(inputType)
			{
			case EInputType.Click:
				return TestClick();
			case EInputType.KeyCode:
				return TestKey();
			case EInputType.Escape:
				return TestEscape();
			default:
				return TestAny();
			}
		}
		
	}
}

