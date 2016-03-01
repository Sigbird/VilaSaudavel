using UnityEngine;
using System.Collections;


namespace YupiStudios.API.Language {

	/*
	 * Classe usada para internacionalizacao simples
	 * 
	 * Ps. Usar preferencialmente em projetos com poucos textos,
	 * pois mantem varios objetos, referentes as diversas linguas, 
	 * carregados na cena
	 */
	public class LanguageChooser : MonoBehaviour {

		[System.Serializable]
		public struct LanguageObject 
		{
			public SystemLanguage Language;
			public GameObject ObjectToActivate;
		}


		public LanguageObject[] languageObjects;
		
		public GameObject activateAsDefault; 

		void Awake () {

			SystemLanguage lang = Application.systemLanguage;
			
			bool found = false;
			
			foreach (LanguageObject l in languageObjects)
			{
				
				if (lang == l.Language)
				{
					found = true;
					if (l.ObjectToActivate)
					{					
						l.ObjectToActivate.SetActive(true);					
					}
					break;
				}
				
			}
			
			if (!found && activateAsDefault)
			{
				activateAsDefault.SetActive(true);
			}
		}

	}

}
