using UnityEngine;
using System.Collections;

namespace YupiStudios.API.Utils 
{

	public enum ERelativeOption
	{
		RELATIVE_TO_WIDTH,
		RELATIVE_TO_HEIGHT
	}

	[RequireComponent(typeof(GUIText))]
	public class FontRelativeToScreen : MonoBehaviour {

		public ERelativeOption relativeDim = ERelativeOption.RELATIVE_TO_HEIGHT;

		public int defaultValue = 80;
		public int defaultResolution = 1080; 

		public int maxCharPerLine = 0;

		private string previus_text;

		private string removeNextWord(ref string str)
		{
			str = str.TrimStart(new char[] {' '});

			int charCount = 0;

			if (str.Length == 0)
				return "";

			string word = "";
			while (charCount < str.Length)
			{
				if (str[charCount] == ' ')
				{
					break;
				}

				word += str[charCount++];
			}

			str = str.Substring(charCount);

			return word;
		}

		public void Process()
		{
			if (maxCharPerLine > 0)
			{
				string newString = "";
				string nextWord; 
				int qtd = maxCharPerLine;

				while (guiText.text.Length > 0)
				{
					

					string line = guiText.text;
					nextWord = removeNextWord(ref line);
					guiText.text = line;

					if (nextWord.Length <= qtd)
					{
						newString += nextWord + " ";
						qtd -= nextWord.Length;
					}
					else 
					{
						newString += "\n";
						newString += nextWord + " ";
						qtd = maxCharPerLine - nextWord.Length;
					}
					
				}
				guiText.text = newString;
			}
			
			if (relativeDim == ERelativeOption.RELATIVE_TO_WIDTH)
			{
				guiText.fontSize = (int) ( Screen.width * defaultValue/defaultResolution );
			}
			else
			{
				guiText.fontSize = (int) ( Screen.height * defaultValue/defaultResolution );
			}
		}
		
		void Start () {

			if (previus_text != guiText.text)
			{
				Process();
				previus_text = guiText.text;
			}
		}
	}

}