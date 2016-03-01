using UnityEngine;

namespace YupiStudios.VilaSaudavel.Tiles {

	/*
	 * Classe que agrupa as informa��es do tile no mapa.
	 * 
	 */
	[System.Serializable]
	public class TileInfo
	{


		/////////////////////////////////////////////////
		// Enumera��o dos tipos de tiles
		/////////////////////////////////////////////////
		public enum ETileType
		{
			
			/*
			 * Tile esta livre, que pode ser usado
			 * em constru��es
			 */
			Free,

			/*
			 * Bloqueado, n�o pode ser usado para constru��es
			 */ 
			Blocked 
		}



		/////////////////////////////////////////////////
		// Indice do tileset que ser� usado para renderiza��o
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
		// Tile est� ocupado por alguma constru��o
		/////////////////////////////////////////////////
		public bool Occupied { 

			get
			{
				return ObjectOwner != null;
			}
		}





		/////////////////////////////////////////////////
		// Indica objeto que est� ocupando o tile
		/////////////////////////////////////////////////
		[SerializeField]
		public TileObject ObjectOwner = null;




		/// <summary>
		/// Retorna uma string do tipo original do tile
		/// para fins de edi��o (alterar tipo do tile no
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
