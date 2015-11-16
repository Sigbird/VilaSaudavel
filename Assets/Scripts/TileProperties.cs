using UnityEngine;
using System.Collections;

public class TileProperties : MonoBehaviour {

	//bool isOcupped = false;

	public GameObject tree;

	public GameObject[] baseTiles;
	public GameObject[] secundaryTiles;
	
	public TileType _tileType;

	void Update(){
	}

	public void AddTree()
	{
		GameObject myGameObject = (GameObject)Instantiate (tree, this.transform.position, Quaternion.identity);
		myGameObject.transform.SetParent(this.transform);
	}
	
	public void RemoveElement(){}

	public void UpdateTile(TileType newTile)
	{
		Debug.Log ("Mudei para: " + newTile.ToString());
	}

	public void ChangeTile()
	{
		switch (_tileType) {
			case TileType.GRASS_DARK: UpdateTile(_tileType); break;
			case TileType.GRASS_LIGHT: UpdateTile(_tileType); break;
			case TileType.SAND_DARK: UpdateTile(_tileType); break;
			case TileType.SAND_LIGHT: UpdateTile(_tileType); break;
		}
	}
}
