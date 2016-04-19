using UnityEngine;
using System.Collections;
using System.IO;

namespace YupiStudios.VilaSaudavel.Tiles.TileMap {

	/*
	 * Classe de dados do mapa de tiles
	 *
	 */
	[ExecuteInEditMode]
	[System.Serializable]
	public class TileMapData : MonoBehaviour {


		
		/////////////////////////////////////////////////
		// Enumera��o do estado de execu��o do mapa
		/////////////////////////////////////////////////
		public enum EWorldMapState
		{

			/*
			 * O mapa pode ser manipulado livremente
			 */
			Idle,


			/*
			 * Estado em que um objeto esta sendo posicionado
			 * (construido)
			 */
			PlacingObject, // force to place or destroy


			/*
			 * Estado em que o objeto esta sendo movido para
			 * outra posicao do mapa
			 */
			MovingObject,  // move to new tile or cancel

		}




		/////////////////////////////////////////////////
		// Tamanho maximo de tiles em um mesh
		// Caso altura ou largura ultrapasse de MAX_TILE_LENGTH
		// Serao gerados 2 ou mais meshes contendo altura e
		// largura <= MAX_TILE_LENGTH
		// Evitar objetos ultra high poly
		/////////////////////////////////////////////////
		private const int MAX_TILE_LENGTH = 50; 



		
		/////////////////////////////////////////////////
		// Configura��es gerais do mapa
		// Ex. Quantidade de tiles, altura, largura, etc.
		/////////////////////////////////////////////////
		public TileUtils.WrldSettings WorldSettings;


		


		/////////////////////////////////////////////////
		// Configura��es do material e Tileset/Tileatlas do mapa
		/////////////////////////////////////////////////
		public TileUtils.MatSettings MaterialSettings;





		/////////////////////////////////////////////////
		// Estado do mapa (ver enum EWorldMapState)
	 	// TODO: Remover esta variavel, e controlar
		// o mapa e seus estados externamente
		/////////////////////////////////////////////////
		public EWorldMapState CurrentState;





		/////////////////////////////////////////////////
		// Prefab para instanciar o(s) mesh(es) do mapa
		/////////////////////////////////////////////////
		public Transform TileMapMeshPrefab;





		/////////////////////////////////////////////////
		// Configura��es do material e Tileset/Tileatlas do mapa
		// Armazenamento feito na forma de matriz 
		// M(altura, largura), ou seja, M[y , x]
		/////////////////////////////////////////////////
		[SerializeField]
		private TileInfo[,] _tiles;

		public TileInfo [,] TileMap
		{
			get
			{
				if (_tiles == null)
					LoadStage();

				return _tiles;
			}
		}




		/// <summary>
		/// Metodo usado para edi��o do mapa, substituindo o	
		/// indice do tile que � usado para renderiza��o
		/// </summary>
		/// <param name="x">indice da coluna.</param>
		/// <param name="y">indice da linha.</param>
		/// <param name="num">indice no tileset do material</param>
		private void SetTileNum(int x, int y, int num)
		{
			TileMap [y, x].TileIndex = num;
		}





		/// <summary>
		/// Altera o tipo original do tile (Free, Blocked, etc.)
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="type">Type.</param>
		private void SetTileType(int x, int y, TileInfo.ETileType type)
		{
			TileMap [y, x].Type = type;
		}






		/// <summary>
		/// (Re)Cria a matriz de tiles
		/// 
		/// ps. Perde a matriz atual, se existir e a informa��o
		/// dos tiles. Ap�s o CreateMatrix() devem ser carregadas
		/// as informa�oes dos tiles
		/// </summary>
		private void CreateMatrix ()
		{
			/* Creating Tile Map */
			_tiles = new TileInfo[WorldSettings.WorldSizeY, WorldSettings.WorldSizeX];
			for (int i = 0; i < WorldSettings.WorldSizeY; ++i)
				for (int j = 0; j < WorldSettings.WorldSizeX; ++j)
					_tiles [i, j] = new TileInfo ();
		}






		/// <summary>
		/// Deletas os Meshes criados pelo mapa
		/// </summary>
		private void DeleteChildren()
		{
			for (int i = transform.childCount-1; i >= 0; --i) {
				if (transform.GetChild(i).gameObject.GetComponent<TileMapMesh>() != null)
				{
					DestroyImmediate(transform.GetChild(i).gameObject);
				}
			}
		}






		/// <summary>
		/// Instancia os meshes que formam o mapa de tiles.
		/// 
		/// Metodo recursivo, para cria��o de um ou varios meshes
		/// limitados por MAX_TILE_LENGTH
		/// </summary>
		/// 
		/// <param name="pos">Posicao no mundo para a criacao do tile.</param>
		/// <param name="sizeX">Largura em tiles</param>
		/// <param name="sizeY">Altura em tiles</param>
		/// <param name="stepX"> A coluna i do mesh refere-se a coluna i+stepX do mapa. </param>
		/// <param name="stepY">  A linha j do mesh refere-se a linha j+stepY do mapa. </param>
		public void InstantiateMap(Vector3 pos, int sizeX, int sizeY, int stepX, int stepY)
		{


			// testa se passa limite de altura e largura
			// faz chamada recursiva para instancia do resto
			if (sizeX > MAX_TILE_LENGTH) {
				InstantiateMap(pos + new Vector3(0.5f*WorldSettings.TileSizeX*MAX_TILE_LENGTH,0,0.5f*WorldSettings.TileSizeY*MAX_TILE_LENGTH), sizeX-MAX_TILE_LENGTH, sizeY, stepX+MAX_TILE_LENGTH, stepY);
				sizeX = MAX_TILE_LENGTH;
			}

			if (sizeY > MAX_TILE_LENGTH) {
				InstantiateMap(pos + new Vector3(0.5f*WorldSettings.TileSizeX*MAX_TILE_LENGTH,0,-0.5f*WorldSettings.TileSizeY*MAX_TILE_LENGTH), sizeX, sizeY-MAX_TILE_LENGTH, stepX, stepY+MAX_TILE_LENGTH);
				sizeY = MAX_TILE_LENGTH;
			}

			
			// Instancia prefab do mesh com tamanho limitado
			Transform obj = Instantiate (TileMapMeshPrefab, transform.position, transform.rotation) as Transform;
			obj.parent = gameObject.transform;
			
			// Configura informa��es do novo mesh
			TileMapMesh mapMesh = obj.gameObject.GetComponent<TileMapMesh> ();
			mapMesh.SetTileMapData( this );
			mapMesh.CreateObject (pos, sizeX, sizeY, stepX, stepY, MaterialSettings, TileObjectMesh.ETileRenderType.RenderTileIndex);

		}






		/// <summary>
		/// Verifica se o tileInfo esta livre para constru��o 
		/// </summary>
		/// <returns><c>true</c> if this tileInfo is free; otherwise, <c>false</c>.</returns>
		/// <param name="info">Info.</param>
		public bool IsTileFree (TileInfo tileInfo)
		{
			return !tileInfo.Occupied && tileInfo.Type == TileInfo.ETileType.Free;
		}






		/// <summary>
		/// Posiciona tileobject no mapa e seta ocupa��o de tiles.
		/// </summary>
		/// <returns><c>true</c>, if object was placed, <c>false</c> otherwise.</returns>
		/// <param name="tileObject">Tile object.</param>
		public bool PlaceObject (TileObject tileObject)
		{
			if ( !CanPlaceObject (tileObject.GetTileRect()) )
				return false;

			int minY = (int) tileObject.TilePosition.y;
			int maxY = (int) tileObject.TilePosition.y + (int) tileObject.ObjectTileSize.y;
			int minX = (int) tileObject.TilePosition.x;
			int maxX = (int) tileObject.TilePosition.x + (int) tileObject.ObjectTileSize.x;

			for (int j = minY; j < maxY; ++j)
			{
				for (int i = minX; i < maxX; ++i)
				{
					TileMap[j,i].SetOwner(tileObject);
				}
			}

			return true;
		}

		public void RemoveObject (TileObject tileObject)
		{
			int minY = (int) tileObject.LastPlacedPosition.y;
			int maxY = (int) tileObject.LastPlacedPosition.y + (int) tileObject.ObjectTileSize.y;
			int minX = (int) tileObject.LastPlacedPosition.x;
			int maxX = (int) tileObject.LastPlacedPosition.x + (int) tileObject.ObjectTileSize.x;
			
			for (int j = minY; j < maxY; ++j)
			{
				for (int i = minX; i < maxX; ++i)
				{
					if (TileExists(i,j))
						TileMap[j,i].ClearOwner();
				}
			}
		}





		/// <summary>
		/// Retorna se os tiles pertencentes ao objectTileRect podem ser ocupados.
		/// </summary>
		/// <returns><c>true</c> if this instance can place object the specified objectTileRect; otherwise, <c>false</c>.</returns>
		/// <param name="objectTileRect">Object tile rect.</param>
		public bool CanPlaceObject (Rect objectTileRect)
		{
			int minY = (int) objectTileRect.y;
			int maxY = (int) objectTileRect.y + (int) objectTileRect.height;
			int minX = (int) objectTileRect.x;
			int maxX = (int) objectTileRect.x + (int) objectTileRect.width;

			if (
				 (minY < 0) || (maxY >= WorldSettings.WorldSizeY) ||
				 (minX < 0) || (maxX >= WorldSettings.WorldSizeX)
				)
				return false;

			for (int j = minY; j < maxY; ++j)
				for (int i = minX; i < maxX; ++i)
					if (! IsTileFree (TileMap [j, i]))
						return false;

			return true;
		}






		/// <summary>
		/// Transforma��o TileToWorldPos.
		/// ver classe TileMapUtils.
		/// </summary>
		/// <returns>The to world position.</returns>
		/// <param name="tilePos">Tile position.</param>
		public Vector3 TileToWorldPos (Vector2 tilePos)
		{
			return TileUtils.TileToWorldPos (this, tilePos);
		}




		/// <summary>
		/// Testa se tile est� dentro dos limites do tamanho do mapa.
		/// </summary>
		/// <returns><c>true</c>, if tile exists, <c>false</c> otherwise.</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public bool TileExists(int x, int y)
		{
			return (x >= 0 && x < WorldSettings.WorldSizeX) && (y >= 0 && y < WorldSettings.WorldSizeY);
		}




		void Update (){

			if (Input.GetKeyDown (KeyCode.F))
				CreateWorldMap ();

		}



		/// <summary>
		/// Refaz o mapeamento UV dos meshes
		/// </summary>
		public void RedoUVs ()
		{
			for (int i = transform.childCount-1; i >= 0; --i) {

				TileMapMesh mesh = transform.GetChild(i).gameObject.GetComponent<TileMapMesh>();
				if (mesh != null)
				{
					mesh.RefreshUVs();
				}
			}
		}







		/// <summary>
		/// Recria e instancia todo o mapa
		/// </summary>
		public void CreateWorldMap ()
		{
			DeleteChildren ();

			CreateMatrix ();

			InstantiateMap (transform.position, WorldSettings.WorldSizeX, WorldSettings.WorldSizeY, 0, 0);
		}






		/// <summary>
		/// Transforma as informa�oes do mapa em um vetor de bytes
		/// </summary>
		public byte[] Serialize() {
			using (MemoryStream m = new MemoryStream()) {
				using (BinaryWriter writer = new BinaryWriter(m)) { 
					writer.Write(WorldSettings.WorldSizeY);
					writer.Write(WorldSettings.WorldSizeX);

					for (int i = 0; i < WorldSettings.WorldSizeY; ++i)
					{
						for (int j = 0; j < WorldSettings.WorldSizeX; ++j)
						{
							writer.Write(_tiles[i,j].TileIndex);
							writer.Write( (int) _tiles[i,j].Type);
						}
					}


				}
				return m.ToArray();
			}
		}




		/// <summary>
		/// Recria mundo segindo as informa�oes do vetor de bytes
		/// </summary>
		/// <param name="data">Data.</param>
		public void Desserialize(byte[] data) {
			using (MemoryStream m = new MemoryStream(data)) {
				using (BinaryReader reader = new BinaryReader(m)) {
					WorldSettings.WorldSizeY = reader.ReadInt32();
					WorldSettings.WorldSizeX = reader.ReadInt32();

					CreateWorldMap ();

					for (int i = 0; i < WorldSettings.WorldSizeY; ++i)
					{
						for (int j = 0; j < WorldSettings.WorldSizeX; ++j)
						{
							_tiles[i,j].TileIndex = reader.ReadInt32();
							_tiles[i,j].Type = (TileInfo.ETileType) reader.ReadInt32(); 
						}
					}

					RedoUVs();
				}
			}
		}






		/// <summary>
		/// Carrega informa��es do mapa.
		/// </summary>
		public void LoadStage()
		{
			TextAsset bindata= Resources.Load("Stage"+WorldSettings.StageId) as TextAsset;

			if (bindata != null) {
				byte [] byteArr = bindata.bytes;
				if (byteArr != null)
					Desserialize (byteArr);
			} else {
				CreateWorldMap ();
			}

			Resources.UnloadAsset (bindata);
		}

		// Salva informa��o em disco
		public void SaveToDisk()
		{
			byte [] byteArr = Serialize ();
			File.Delete (Application.dataPath + "/Resources/Stage"+WorldSettings.StageId+".bytes");
			File.WriteAllBytes(Application.dataPath + "/Resources/Stage"+WorldSettings.StageId+".bytes", byteArr);
		}


		void Awake() {
			CurrentState = EWorldMapState.Idle;
			if (PlayerPrefs.HasKey ("fase")) {
				if (PlayerPrefs.GetInt ("fase") < 2) {
					WorldSettings.StageId = PlayerPrefs.GetInt ("fase").ToString ();
					LoadStage ();
				} else {
					WorldSettings.StageId = "1";
					LoadStage ();
				}
			}
//			Debug.Log (WorldSettings.StageId);
			//LoadStage ();
		}

	}

}
