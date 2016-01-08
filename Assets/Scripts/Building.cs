using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {

	private string name;
	private int cost;
	private int citizenPerMonth;
	private GameObject description;
	private bool available;
	private float spaceToOccupy;
	
	#region GETTERS and SETTERS
	public string Name{
		get{ return name;}
		set { this.name = value;}
	}

	public int Cost{
		get{ return cost;}
		set{ this.cost = value;}
	}

	public int CitizesPerMonth{
		get{ return citizenPerMonth;}
		set{ this.citizenPerMonth = value;}
	}

	public GameObject Description{
		get{ return description;}
		set{ this.description = value;}
	}

	public bool Available{
		get{ return available;}
		set{ this.available = value;}
	}

	public float SpaceToOccupy{
		get{ return spaceToOccupy;}
		set{ this.spaceToOccupy = value;}
	}
	#endregion


	void Start () {
	}

	void Update () {
	}

	void Build(){
	}

	void DestroyBuilding(){
	}
}
