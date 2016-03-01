using UnityEngine;
using System.Collections;

namespace YupiStudios.API.Utils {


	/*
	 * Executa um acao quando clica 
	 * em um objeto com um colllider 3D collider
	 */
	public class ActionOnClickCollider3D : MonoBehaviour {

		private Collider listenerRegion;
		public ActionObject action;

		void Awake()
		{
			listenerRegion = GetComponent<Collider> ();
		}


		public bool IsMouseOnBounds()
		{		
			if (listenerRegion != null)
			{
				Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hitInfo;
				
				if (listenerRegion.Raycast(r, out hitInfo, Mathf.Infinity))
				{
					return true;
				}

				return false;
			} else
			{
				Debug.LogWarning("No Collider Found In Object");
				return false;
			}
			
		}

		// Update is called once per frame
		void Update () {
			if ( Input.GetMouseButtonDown(0) && IsMouseOnBounds () ) {
				action.DoAction();
			}
		}
	}


}