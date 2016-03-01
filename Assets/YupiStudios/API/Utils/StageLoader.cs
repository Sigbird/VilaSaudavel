using UnityEngine;
using System;
using System.Collections;

namespace YupiStudios.API.Utils {

	/*
	 * Carrega uma cena no start do objeto
	 */
	[Obsolete("Deprecated class, please use ActionOnStart instead.")]
	public class StageLoader : MonoBehaviour {

		public string sceneToLoad;
		
		// Update is called once per frame
		void Start () {
			Application.LoadLevel(sceneToLoad);
		}
	}

}