using UnityEngine;
using System.Collections;
using YupiStudios.VilaSaudavel.Tiles.TileMap;

namespace YupiStudios.VilaSaudavel.Tiles {


	/**
	 * Classe estatica para metodos comuns que utilizam o mapa de tiles 
	 * 
	 */
	public class TileUtils {

		public static Vector2 TILE_NULL_POSITION = new Vector2(-1,-1);

		public static TileMapData WorldMap {
			get 
			{
				return FindWorldMap ();
			}
		}


		/////////////////////////////////////////////////
		// Estrutura de Configuraçao do Mapa
		/////////////////////////////////////////////////
		[System.Serializable]
		public struct WrldSettings
		{
			public string StageId;
			public int WorldSizeX;
			public int WorldSizeY;		
			public float TileSizeX;
			public float TileSizeY;
		}



		/////////////////////////////////////////////////
		// Enumeração do estado de execução do mapa
		/////////////////////////////////////////////////
		[System.Serializable]
		public struct MatSettings
		{
			public Material TileMaterial;
			public int TilesetSizeX;
			public int TilesetSizeY;
		}




		/// <summary>
		/// Busca TileMapData entre os GameObjects da cena
		/// </summary>
		private static TileMapData FindWorldMap()
		{
			return GameObject.FindObjectOfType<TileMapData> ();		
		}





		/// <summary>
		/// Transforma posicao em Tiles (x,y) para a posicao 3D em unity (WorldPos).
		/// </summary>
		/// <returns>The world position.</returns>
		/// <param name="data">Data.</param>
		/// <param name="tilePos">Tile position.</param>
		/// <param name="cellCenter">If set to <c>true</c>, returns cell center position, otherwise, the cell left position.</param>
		public static Vector3 TileToWorldPos (TileMapData data, Vector2 tilePos, bool cellCenter = false)
		{
			float x = tilePos.x;
			float z = tilePos.y;
			
			float newZ =  (x - z) * 0.5f;
			float newX =  (x + z) * 0.5f;

			if (cellCenter) {
				newX += 0.5f;
			}
			
			return new Vector3( newX * data.WorldSettings.TileSizeX + data.transform.position.x, data.transform.position.y, newZ * data.WorldSettings.TileSizeY + data.transform.position.z );
		}


		/// <summary>
		/// Dada uma posiçao no Mundo 3D (unity) retorna a coordenada em tiles.
		/// </summary>
		/// <returns>The tile position.</returns>
		/// <param name="data">Data.</param>
		/// <param name="worldPos">World position.</param>
		public static Vector2 WorldPosToTile (TileMapData data, Vector3 worldPos)
		{



			worldPos -= data.transform.position;



			//////////////////////////////////////////////////////////////////////////////
			// Equacao da reta que passa pelos centros dos tiles  da primeira linha 
			// r: AX + BY + C
			//
			// P1(0.5,0); P2 (1,0.5)
			// Y = mX + C; P1: 0.5 = m1 + C; P2: 1 = m1.5 + C 
			//
			// ... Resolvendo o Sistema de Equaçoes...
			// 1 - 1.5*0.5 = m1.5 - 1.5*m1 + C - 1.5*C => 0.25 = -0.5C => C = -0.5
			// 0.5 = m - 0.5 => m = 1
			// Y = m(1)X + c(-0.5) => reta r: A(1)X + B(-1)Y + C(-0.5) = 0
			// 
			
			
			// transforma para escala 1:1
			float x = (  worldPos.x / data.WorldSettings.TileSizeX );
			float z = (  worldPos.z / data.WorldSettings.TileSizeY ) ;
			Vector2 pOrig = new Vector2 (x, z);




			///////////////////////////////////////////////////////////////////////////////
			// Retas ortogonais => m_orth (90° m) = -1/m => m_orth = -1 => Y_orth = m_orth(-1)X + C_orth
			// Y_orth - z = -1 (X_orth - x)
			// Y = -X + (x + z)
			//


			/* Ponto de encontro */

			// Y = 1X - 0.5 => -X + (x+z) = X - 0.5 => -2X = - 0.5 - (x+z) => X  = (0.5 + x + z) / 2
			float C = x + z;
			float X = (0.5f + C) / 2.0f;
			float Y = - X + (C);
			Vector2 pMeeting = new Vector2 (X, Y);






				       /* Debug Only */
			/*	
				// Debug reta1
				float Y1 = -100 - 0.5f;
				float Y2 = 100 - 0.5f;
				Debug.DrawLine (new Vector3 (-100*data.WorldSettings.TileSizeX,0,Y1*data.WorldSettings.TileSizeY), new Vector3 (100*data.WorldSettings.TileSizeX,0,Y2*data.WorldSettings.TileSizeY), Color.red);
				// Debug reta2
				float Y3 = 100 + C;
				float Y4 = -100 + C;
				Debug.DrawLine (new Vector3 (-100*data.WorldSettings.TileSizeX,0,Y3*data.WorldSettings.TileSizeY), new Vector3 (100*data.WorldSettings.TileSizeX,0,Y4*data.WorldSettings.TileSizeY), Color.blue);
			*/





			//////////////////////////////////////////////////////////////
			// Pontos medio
			//
			//                    . (0.5,0.5)
			//
			//
			//              .             .  pMed1 -------------------------------( (1+0.5) / 2 , (+0.5 + 0) / 2 ) = (0.75f, 0.25f)
			//
			//                 (0.5,0)  
			// (0.0)  .           .             . (1,0)
			//
			//
			//             .              . 
			//
			//
			//                    . (0.5,-0.5)


			Vector2 center = new Vector2 (0.5f, 0);
			Vector2 pMed1 = new Vector2 (0.75f, 0.25f);
			float dist = 2 * Vector2.Distance (pMed1, center); // tamanho do lado do tile

			X = Mathf.FloorToInt (0.5f + Vector2.Distance (pMeeting, center) / dist);
			Y = Mathf.FloorToInt (0.5f + Vector2.Distance (pMeeting, pOrig) / dist);


			return new Vector2( X, Y );
		}



		/// <summary>
		/// Transforma posicao em Tiles (x,y) para a posicao 3D em unity (WorldPos).
		/// </summary>
		/// <returns>The world position.</returns>
		/// <param name="data">Data.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="cellCenter">If set to <c>true</c>, cell center position.</param>
		public static Vector3 TileToWorldPos (TileMapData data, int x, int y, bool cellCenter = false)
		{
			
			return TileToWorldPos( data, new Vector2(x,y), cellCenter );
		}





		/// <summary>
		/// Retorna a coordenada em tiles na posiçao que se encontra o mouse.
		/// </summary>
		/// <returns>The mouse tile position.</returns>
		/// <param name="Data">Data.</param>
		/// <param name="checkDistance">If set to <c>true</c> check distance.</param>
		public static Vector2 GetMouseTilePos(TileMapData Data, bool checkDistance = true)
		{
			return GetTileInScreenPos(Data, Input.mousePosition, checkDistance);
		}




		/// <summary>
		/// Retorna a coordenada em tiles pela coordenada em pixels.
		/// 
		/// Se checkDistance or true, retorna (-1,-1) caso distante do centro do tile.
		/// checkDistance esta sendo usado para evitar erros frequentes na pintura (Ediçao) do tilie.
		/// 
		/// </summary>
		/// <returns>The tile in screen position.</returns>
		/// <param name="Data">Data.</param>
		/// <param name="pos">Position (pixels).</param>
		/// <param name="checkDistance">If set to <c>true</c> check distance.</param>
		public static Vector2 GetTileInScreenPos(TileMapData Data, Vector2 pos, bool checkDistance = false)
		{
			Vector3 desloc = Data.transform.position;

			Ray r = Camera.main.ScreenPointToRay(pos);
			RaycastHit info;
			Physics.Raycast(r, out info, Mathf.Infinity);
			
			if (info.collider != null)
			{
				Vector3 p = info.point - desloc;


				/*float x = ( p.x / Data.WorldSettings.TileSizeX ) - 0.5f;
				float z = (  p.z / Data.WorldSettings.TileSizeY ) ;				
				z = Mathf.RoundToInt ( Mathf.Abs ( x - z  ) );
				x = Mathf.RoundToInt ( 2 * x - z  );*/

				Vector2 tile = WorldPosToTile(Data,new Vector3(p.x,0,p.z)); 
							
				if (checkDistance)
				{

					Vector3 center = TileToWorldPos(Data, tile, true);
					Vector2 centerDist;

					centerDist.x = Mathf.Abs(p.x-center.x) / Data.WorldSettings.TileSizeX;
					centerDist.y = Mathf.Abs(p.z-center.z) / Data.WorldSettings.TileSizeY;
					
					if (centerDist.x > 0.5f || centerDist.y > 0.5f )
						return TILE_NULL_POSITION;
					else
						return tile;
					
				} else {
					
					return tile;
					
				}
			}
			
			return TILE_NULL_POSITION;
		}

	}

}
