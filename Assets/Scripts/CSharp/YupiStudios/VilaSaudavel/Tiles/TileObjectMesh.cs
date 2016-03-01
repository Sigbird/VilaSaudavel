using UnityEngine;
using System.Collections;
using YupiStudios.VilaSaudavel.Tiles.TileMap;

namespace YupiStudios.VilaSaudavel.Tiles {

	/*
	 * Mesh que renderiza os tiles em baixo de um objeto
	 * 
	 * Estes tiles estao sendo usados atualmente para posicionamento do objeto 
	 * Vermelho (Tile Obstruido/Ocupado) Verde (Nao Obstruido)
	 */
	[RequireComponent(typeof(UnityEngine.MeshFilter))]
	[RequireComponent(typeof(UnityEngine.MeshRenderer))]
	public class TileObjectMesh : MonoBehaviour {


		public enum ETileRenderType {
			
			RenderTileIndex,
			RenderTileBlockStatus,
			RenderTile0
			
		}


		protected TileMapData TileMapData;
		
		public TileUtils.MatSettings materialSettings;
		
		protected MeshFilter meshFilter;
		protected MeshRenderer meshRenderer;
		
		protected int worldSizeX, worldSizeY;
		protected int originalMapStepX, originalMapStepY;

		protected ETileRenderType renderType;


		/// <summary>
		/// Seta mapa de tiles que a malha estara associada (pertence)
		/// </summary>
		/// 
		/// <param name="data">Objeto contendo informaçoes do mapa de tiles</param>
		public void SetTileMapData (TileMapData data)
		{
			TileMapData = data;
		}




		/// <summary>
		/// Inicializaçao de variaveis de acesso aos components do mesh.
		/// </summary>
		protected virtual void SetComponents()
		{
			if (meshFilter == null) {
				meshFilter = GetComponent<MeshFilter> ();
				meshRenderer = GetComponent<MeshRenderer> ();
			}
		}




		/// <summary>
		/// Cria os vertices.
		/// </summary>
		/// 
		/// <returns>The vertices.</returns>
		/// 
		/// <param name="position">Posicao (em world) para iniciar a criacao dos vertices.</param>
		/// <param name="sizeX">Numero de tiles em x (largura).</param>
		/// <param name="sizeY">Numero de tiles em y (altura).</param>
		/// <param name="tileSizeX">Largura de um tile.</param>
		/// <param name="tileSizeZ">Altura de um tile.</param>
		protected Vector3[] createVertices(Vector3 position, int sizeX, int sizeY, float tileSizeX, float tileSizeZ)
		{
			int verticesSizeX = sizeX * 2;
			int verticesSizeY = sizeY * 2;
			
			Vector3[] vertices = new Vector3[verticesSizeX*verticesSizeY];
			
			for (int i = 0; i < verticesSizeY; ++i) {
				for (int j = 0; j < verticesSizeX; ++j) {
					
					
					float x = j - (j/2);
					float z = i - (i/2);
					
					float newZ =  (x - z) * 0.5f;
					float newX =  (x + z) * 0.5f; 
					
					int idx = i * verticesSizeX + j;
					
					vertices[idx] = new Vector3( newX*tileSizeX + position.x, position.y, newZ*tileSizeZ+ position.z );
				}
			}
			
			return vertices;
			
		}



		/// <summary>
		/// Cria os vetores de normais para os vertices.
		/// </summary>
		/// 
		/// <returns>The normals.</returns>
		/// 
		/// <param name="sizeX">Numero de tiles em x (largura).</param>
		/// <param name="sizeY">Numero de tiles em y (altura).</param>
		protected Vector3[] createNormals(int sizeX, int sizeY)
		{
			int size = sizeX * sizeY * 4;
			
			Vector3[] normals = new Vector3[size];
			
			for (int i = 0; i < size; ++i) {
				normals[i] = Vector3.up;
			}
			
			return normals;
			
		}




		/// <summary>
		/// Cria os triangulos que compoem a malha.
		/// ps. A ordem dos vertices nos triangulos importa
		/// </summary>
		/// 
		/// <returns>The triangles.</returns>
		/// 
		/// <param name="sizeX">Numero de tiles em x (largura).</param>
		/// <param name="sizeY">Numero de tiles em y (altura).</param>
		protected int[] createTriangles(int sizeX, int sizeY)
		{
			int stepVertexLine = sizeX*4;
			int stepVertex = sizeX*2;
			
			int numQuads = sizeX * sizeY;
			
			int[] tris = new int[numQuads*2*3];
			
			for (int i = 0; i < sizeY; ++i) {
				for (int j = 0; j < sizeX; ++j) {
					
					int vertexPos = i * stepVertexLine +  j * 2;
					
					tris[(i * sizeX + j)*6] = vertexPos;
					tris[(i * sizeX + j)*6+1] = vertexPos+stepVertex+1;
					tris[(i * sizeX + j)*6+2] = vertexPos+stepVertex;
					
					tris[(i * sizeX + j)*6+3] = vertexPos;
					tris[(i * sizeX + j)*6+4] = vertexPos+1;
					tris[(i * sizeX + j)*6+5] = vertexPos+stepVertex+1;
					
				}
			}
			
			return tris;
			
		}




		/// <summary>
		/// Gets the index of the UV.
		/// </summary>
		/// 
		/// <returns>
		/// O indice no tileset (do material) que 
		/// sera usado para criaçao do mapeamento UV	
		/// </returns>
		/// 
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		protected virtual int GetUVIndex(int x, int y)
		{
			return TileMapData.TileMap[y,x].TileIndex;
		}



		/// <summary>
		/// Gets the index of the UV.
		/// </summary>
		/// 
		/// <returns>
		/// O indice no tileset (do material) que 
		/// sera usado para criaçao do mapeamento UV	
		/// </returns>
		/// 
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		protected virtual int GetUVBStatus(int x, int y)
		{
			if (TileMapData.TileExists (x, y)) {
				TileInfo tile = TileMapData.TileMap [y, x];
				return (tile.Type == TileInfo.ETileType.Free && !tile.Occupied) ? 0 : 1;
			} else {
				return 0;
			}
		}


		protected int GetTileUV(int x, int y)
		{
			switch (renderType) {
			case ETileRenderType.RenderTileIndex:
				return GetUVIndex(x,y);
			case ETileRenderType.RenderTileBlockStatus:
				return GetUVBStatus(x,y);
			default:
				return 0;
			}
		}



		/// <summary>
		/// Cria o mapeamento UV dos vertices.
		/// </summary>
		/// <returns>The UVs.</returns>
		/// <param name="sizeX">Numero de tiles em x (largura).</param>
		/// <param name="sizeY">Numero de tiles em y (altura).</param>
		protected Vector2[] createUVs(int sizeX, int sizeY)
		{
			int verticesSizeX = sizeX * 2;
			int verticesSizeY = sizeY * 2;
			
			float tileSizeX = 1f / materialSettings.TilesetSizeX;
			float tileSizeY = 1f / materialSettings.TilesetSizeY;
			
			Vector2[] uvs = new Vector2[verticesSizeX*verticesSizeY];
			
			for (int i = 0; i < sizeY; ++i) {
				for (int j = 0; j < sizeX; ++j) {
					
					int step = i*sizeX*4;
					
					int idx1 = j*2;
					int idx2 = j*2+1;
					int idx3 = (sizeX + j) *2;
					int idx4 = (sizeX + j) *2+1;

					int tileNum = GetTileUV(originalMapStepX+j, originalMapStepY+i);

					float minTileX = (tileNum % materialSettings.TilesetSizeX)*tileSizeX;
					float minTileY = (tileNum / materialSettings.TilesetSizeX)*tileSizeY;
					
					uvs[step+idx1] = new Vector2(minTileX, minTileY + 0.5f*tileSizeY);
					uvs[step+idx2] = new Vector2(minTileX + 0.5f*tileSizeX, minTileY + tileSizeY);
					uvs[step+idx3] = new Vector2(minTileX + 0.5f*tileSizeX, minTileY);
					uvs[step+idx4] = new Vector2(minTileX + tileSizeX, minTileY + 0.5f*tileSizeY);
				}
			}
			
			return uvs;
			
		}






		/// <summary>
		/// Creates the mesh.
		/// </summary>
		/// <param name="position">Posicao (em world) para iniciar a criacao dos vertices.</param>
		/// <param name="sizeX">Numero de tiles em x (largura).</param>
		/// <param name="sizeY">Numero de tiles em y (altura).</param>
		protected virtual void createMesh(Vector3 position, int sizeX, int sizeY)
		{	
			Mesh mesh = new Mesh ();

			
			this.worldSizeX = sizeX;
			this.worldSizeY = sizeY;

			Vector3[] vertices = createVertices(position, sizeX, sizeY, TileMapData.WorldSettings.TileSizeX,TileMapData.WorldSettings.TileSizeY);
			Vector3[] normals = createNormals(sizeX,sizeY);
			Vector2[] uvs = createUVs(sizeX,sizeY);
			int [] tris = createTriangles(sizeX,sizeY);

			mesh.name = "ProceduralMesh";
			mesh.vertices = vertices;
			mesh.triangles = tris;
			mesh.normals = normals;
			mesh.uv = uvs;
			
			meshFilter.sharedMesh = mesh;
			meshRenderer.sharedMaterial = materialSettings.TileMaterial;
			
		}






		/// <summary>
		/// Altera a referencia dos tiles em relacao a matriz original de tiles
		/// 
		/// O tile (i,j) deste mesh obtera as informacoes* do tile (i+stepX,j+stepY) no mapa de tiles**
		/// 
		/// informacoes* = TileInfo
		/// mapa de tiles** = TileMap de TileMapData
		/// 
		/// </summary>
		/// <param name="originalMapStepX">Original map step x.</param>
		/// <param name="originalMapStepY">Original map step y.</param>
		public void SetMapOriginalStep(int originalMapStepX, int originalMapStepY)
		{
			this.originalMapStepX = originalMapStepX;
			this.originalMapStepY = originalMapStepY;
		}








		/// <summary>
		/// Cria o mesh correspondente da posicao (originalMapStepX, originalMapStepY) ate (originalMapStepX + meshSizeX, originalMapStepY + meshSizeY).
		/// </summary>
		/// <param name="position">Posicao (em world) para iniciar a criacao dos vertices.</param>
		/// <param name="meshSizeX">Numero de tiles em x (largura).</param>
		/// <param name="meshSizeY">Numero de tiles em y (altura).</param>
		/// <param name="originalMapStepX">Original map step x.</param>
		/// <param name="originalMapStepY">Original map step y.</param>
		/// <param name="m">M.</param>
		public void CreateObject (Vector3 position, int meshSizeX, int meshSizeY, int originalMapStepX, int originalMapStepY, TileUtils.MatSettings m, ETileRenderType renderType = ETileRenderType.RenderTileBlockStatus)
		{		
			SetComponents ();
			
			materialSettings = m;

			this.renderType = renderType;

			SetMapOriginalStep (originalMapStepX,originalMapStepY);

			createMesh (position, meshSizeX, meshSizeY);
		}





		/// <summary>
		/// Refaz (Refresh) mapeamento UV do objeto.
		/// </summary>
		public void RefreshUVs()
		{
			SetComponents ();
			meshFilter.sharedMesh.uv = createUVs (worldSizeX, worldSizeY);
		}






		void Awake() {
			SetComponents ();
		}
		
		// Use this for initialization
		void Start () {
		}
	}

}