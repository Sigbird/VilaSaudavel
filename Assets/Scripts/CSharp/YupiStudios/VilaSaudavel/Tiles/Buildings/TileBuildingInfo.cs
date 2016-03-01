using UnityEngine;
using System.Collections;

namespace YupiStudios.VilaSaudavel.Tiles.Buildings {

	public class TileBuildingInfo : MonoBehaviour {


		public enum EBuildingType {

			SimpleHouse,
			HealthCenter, // posto de saude
			Hospital
		}


		public EBuildingType buildingType;


		public TileObject tileObject;

	}


}