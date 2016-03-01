using UnityEngine;
using System.Collections;
using YupiStudios.VilaSaudavel.Tiles.TileMap;

namespace YupiStudios.VilaSaudavel.Tiles {


	/**
	 * Descreve Objeto que anda pelo mapa de tiles
	 * 
	 */
	public class TileMapWalker : MonoBehaviour {

		private const float WAIT_FOR_NEXT_TILE = 35.0f;


		///////////////////////////////////////////////////
		/// Enumeracao de comportamentos diferentes 
		/// do objeto.
		///////////////////////////////////////////////////
		[System.Serializable]
		public enum EWalkType
		{

			/*
			 * Anda na direçao, sem respeitar os tiles
			 * 
			 */
			Free,



			
			/**
			 * Passa por tiles ocupados, seguindo o menor
			 * caminho
			 * 
			 */
			GhostWalk

		}




		///////////////////////////////////////////////////
		/// Classe de dados do mapa
		///////////////////////////////////////////////////
		public TileMapData WorldMap;





		///////////////////////////////////////////////////
		/// Tipo de comportamento do objeto.
		///////////////////////////////////////////////////
		public EWalkType WalkType;





		
		///////////////////////////////////////////////////
		/// Tile de origem do agente
		///////////////////////////////////////////////////
		public Vector2 OriginTile;// { get; set; }





		///////////////////////////////////////////////////
		/// Range maximo em tiles a partir da posicao origem
		///////////////////////////////////////////////////
		public int LimitWalkRange;



		

		///////////////////////////////////////////////////
		/// Velocidade de movimentaçao do objeto
		///////////////////////////////////////////////////
		public float WalkSpeed;





		///////////////////////////////////////////////////
		/// Tipo do tile que o objeto busca andar
		///////////////////////////////////////////////////
		public int SearchTileIndex;

		



		///////////////////////////////////////////////////
		/// Posicao atual no mapa de tiles
		///////////////////////////////////////////////////
		public Vector2 _CurrentTilePos;
		public Vector2 CurrentTilePos
		{
			get
			{
				_CurrentTilePos = TileUtils.WorldPosToTile (WorldMap,transform.position);
				return TileUtils.WorldPosToTile (WorldMap,transform.position);
			}
		}
		


		///////////////////////////////////////////////////
		/// Posicao destino no mapa de tiles
		///////////////////////////////////////////////////
		private Vector2 _tileToGo;
		public Vector2 TileToGo { 
			get
			{
				return _tileToGo;
			}
			set
			{
				nextCenterToGo = TileUtils.TileToWorldPos(WorldMap, CurrentTilePos, true);
				_tileToGo = value;
				_WorldPosToGo = TileUtils.TileToWorldPos(WorldMap,_tileToGo,true);
			}
		}
		public Vector3 nextCenterToGo;



		///////////////////////////////////////////////////
		/// Posicao destino no mapa de tiles (em World Unity)
		///////////////////////////////////////////////////
		private Vector3 _WorldPosToGo;
		public Vector3 WorldPosToGo
		{
			get
			{
				return _WorldPosToGo;
			} 
			set 
			{
				TileToGo = TileUtils.WorldPosToTile(WorldMap, value);
			}
		}




		///////////////////////////////////////////////////
		/// Rotacoes do modelo 3D, para as respectivas
		/// direcoes.
		///////////////////////////////////////////////////
		public float angleCorrection;
		private Quaternion origAngle;




		public Animator Animator;
		private int animWalk;
		private int animIdle;
		private float waitForTime;


		/// <summary>
		/// Updates the free walk behaviour.
		/// </summary>
		private void UpdateFreeWalk()
		{
			transform.position =  Vector3.MoveTowards( transform.position, WorldPosToGo, WalkSpeed*Time.deltaTime );
		}




		private void ChanheDirection(Vector3 direction)
		{

			float x = direction.x;
			float y = direction.z;
			Vector2 ang1 = new Vector2 (direction.x, direction.z);
			Vector2.Angle (Vector2.zero, ang1);

			float ang = 360 - Mathf.Atan2 (y, x) * Mathf.Rad2Deg + angleCorrection;

			transform.rotation = origAngle * Quaternion.AngleAxis (ang, new Vector3 (0, 0.1f, 0.1f));		
		}


		
		/// <summary>
		/// Updates the ghost walk behaviour.
		/// </summary>
		private void UpdateGhostWalk()
		{
			if (TileToGo != CurrentTilePos) {

				Vector3 dist = (nextCenterToGo - transform.position);

				if (dist.magnitude != 0)
				{

					transform.position = Vector3.MoveTowards (transform.position, nextCenterToGo, WalkSpeed*Time.deltaTime);

				} else 
				{
					Vector2 currentTile = CurrentTilePos;
					Vector2 deltaTile = (TileToGo - currentTile);
					if ( Mathf.Abs ( deltaTile.y ) >= Mathf.Abs ( deltaTile.x ) )
					{
						deltaTile.x = 0;
						deltaTile.y = (deltaTile.y > 0) ? 1 : -1;
					} else
					{
						deltaTile.y = 0;
						if (deltaTile.x != 0)
							deltaTile.x = (deltaTile.x > 0) ? 1 : -1;
					}



					nextCenterToGo = TileUtils.TileToWorldPos(WorldMap,currentTile+deltaTile,true);
					ChanheDirection(nextCenterToGo - transform.position);
				}

			} else {


				transform.position = Vector3.MoveTowards (transform.position, WorldPosToGo, WalkSpeed*Time.deltaTime);

				Vector3 dist = (nextCenterToGo - transform.position);
				if (dist.magnitude != 0)
					ChanheDirection(nextCenterToGo - transform.position);

			}
		}


		private Vector2 GetNextTile()
		{
			int lX = (int)OriginTile.x - LimitWalkRange, hX = (int)OriginTile.x + LimitWalkRange + 1;
			int lY = (int)OriginTile.y - LimitWalkRange, hY = (int)OriginTile.y + LimitWalkRange + 1;

			lX = Mathf.Max (0, Mathf.Min (lX, (int)WorldMap.WorldSettings.WorldSizeX - 1));
			lY = Mathf.Max (0, Mathf.Min (lY, (int)WorldMap.WorldSettings.WorldSizeY - 1));
			hX = Mathf.Max (0, Mathf.Min (hX, (int)WorldMap.WorldSettings.WorldSizeX - 1));
			hY = Mathf.Max (0, Mathf.Min (hY, (int)WorldMap.WorldSettings.WorldSizeY - 1));

			return new Vector2 (
			                	Random.Range (lX, hX),
			            		Random.Range (lY, hY)
			);
		}

		
		void Start () 
		{
			if (WorldMap == null)
				WorldMap = TileUtils.WorldMap;


			origAngle = transform.rotation;

			OriginTile = CurrentTilePos;

			/* Iniciar andando aleatoriamente */
			TileToGo = GetNextTile();
			nextCenterToGo = TileUtils.TileToWorldPos(WorldMap, OriginTile, true);
			ChanheDirection(nextCenterToGo - transform.position);

			animWalk = Animator.StringToHash ("Walk");
			animIdle = Animator.StringToHash ("Idle");
			waitForTime = WAIT_FOR_NEXT_TILE;
		}


		void Update ()
		{
			
			if (WorldPosToGo != transform.position) {

				switch (WalkType) {
				case EWalkType.GhostWalk:
					UpdateGhostWalk ();
					break;
				default:
					UpdateFreeWalk ();
					break;
				}

			} else {
				if (waitForTime > 0)
				{
					waitForTime -= Time.deltaTime;
					Animator.Play(animIdle);
				}
				else			
				{
					Animator.Play(animWalk);
					waitForTime = WAIT_FOR_NEXT_TILE;
					TileToGo = GetNextTile(); // Andar aleatoriamente
				}
			}


		}

	}

}
