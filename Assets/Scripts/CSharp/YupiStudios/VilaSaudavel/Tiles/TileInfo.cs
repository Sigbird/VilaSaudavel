using UnityEngine;

namespace YupiStudios.VilaSaudavel.Tiles {

	/*
	 * Classe que agrupa as informações do tile no mapa.
	 * 
	 */
	[System.Serializable]
	public class TileInfo
	{


		/////////////////////////////////////////////////
		// Enumeração dos tipos de tiles
		/////////////////////////////////////////////////
		public enum ETileType
		{
			
			/*
			 * Tile esta livre, que pode ser usado
			 * em construções
			 */
			Free,

			/*
			 * Bloqueado, não pode ser usado para construções
			 */ 
			Blocked 
		}



		/////////////////////////////////////////////////
		// Indice do tileset que será usado para renderização
		// ex: Tile de grama (indice = 0) 
		//     Tile de estrada (indice = 2)
		/////////////////////////////////////////////////
		[SerializeField]
		public int TileIndex;
		


		
		
		/////////////////////////////////////////////////
		// Tipo original do tile (ver enum ETileType)
		/////////////////////////////////////////////////
		[SerializeField]
		public ETileType Type = ETileType.Free;




		/////////////////////////////////////////////////
		// Tile está ocupado por alguma construção
		/////////////////////////////////////////////////
		public bool Occupied { 

			get
			{
				return ObjectOwner != null;
			}
		}





		/////////////////////////////////////////////////
		// Indica objeto que está ocupando o tile
		/////////////////////////////////////////////////
		[SerializeField]
		public TileObject ObjectOwner = null;




		/// <summary>
		/// Retorna uma string do tipo original do tile
		/// para fins de edição (alterar tipo do tile no
		/// Editor do unity)
		/// </summary>
		/// <returns>The tile info.</returns>
		public string GetTileInfo()
		{
			if (Type == ETileType.Blocked)
				return "B";
			if (Occupied)
				return "O";
			return "F";
		}



		/// <summary>
		/// Seta novo owner do tile
		/// 
		/// ps. forca o set, mesmo se ja tive owner
		/// deve ser verificado se ja esta ocupado antes
		/// </summary>
		/// <param name="tileObject">Tile object.</param>
		public void SetOwner (TileObject tileObject)
		{
			ObjectOwner = tileObject;
		}



		/// <summary>
		/// Remove Status de Occupied
		/// </summary>
		public void ClearOwner ()
		{
			ObjectOwner = null;
		}

	}

}
