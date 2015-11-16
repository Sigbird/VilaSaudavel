using UnityEngine;
using System.Collections;

public class GenCenario : MonoBehaviour {

	public int width;
	public int heigth;
	
	public GameObject[] tiles;

	//public Sprite tree;

	void Start () {
		Create ();
	}

	void Update () {
	
	}

	public void Clear()
	{
		while (this.transform.childCount > 0)
			DestroyImmediate (this.transform.GetChild (0).gameObject);
	}
	                    

	public void Create()
	{
		for(int y = 0; y < heigth; y++){
			int index = (y & 1);						//Verifica se eh par ou impar
			float offsetx = (index == 1) ? 1f : 0f;

			for(int x = 0; x < width; x++){
				GameObject myGameObject = (GameObject) Instantiate(tiles[index], this.transform.position, Quaternion.identity);
				myGameObject.transform.SetParent(this.transform);
				myGameObject.transform.localPosition = new Vector2(x*2 + offsetx, y/2f);
			}
		}
	}
	

//	public void GenerateTree(){
//		for (int y = 0; y < heigth; y++) {
//			int index = (y & 1);
//			float offsetx = (index == 1) ? 1f : 0f;
//			
//			for (int x = 0; x < width; x++) {
//				GameObject myGameObject = new GameObject ("elemet");
//				Transform myTransform = myGameObject.transform;
//				SpriteRenderer myRenderer = myGameObject.AddComponent<SpriteRenderer> ();
//				myRenderer.sprite = tree;
//				myTransform.SetParent (this.transform);
//				
//				
//				myTransform.localPosition = new Vector2 (x * 2 + offsetx, y / 2f);
//			}
//		}
//	}

}
