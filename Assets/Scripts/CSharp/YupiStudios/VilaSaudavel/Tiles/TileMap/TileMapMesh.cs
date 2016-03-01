using UnityEngine;
using System.Collections;
using System;

namespace YupiStudios.VilaSaudavel.Tiles.TileMap {

	/**
	 * Versao do TileObjectMesh com meshcollider
	 * 
	 */
	[ExecuteInEditMode]
	[RequireComponent(typeof(UnityEngine.MeshCollider))]
	public class TileMapMesh : TileObjectMesh {


		/////////////////////////////////////////////////
		// Collider do Mapa (usado em Ray Casts)
		/////////////////////////////////////////////////
		protected MeshCollider meshCollider;



		/// <summary>
		/// Inicializaçao de variaveis de acesso aos components do mesh.
		/// Adicionado o meshCollider ao base.SetComponents()
		/// </summary>
		protected override void SetComponents()
		{
			if (meshFilter == null) {
				meshFilter = GetComponent<MeshFilter> ();
				meshCollider = GetComponent<MeshCollider> ();
				meshRenderer = GetComponent<MeshRenderer> ();
			}
		}



		/// <summary>
		/// Creates the mesh. Tambem refenrencia o mesh que foi criado ao collider.
		/// </summary>
		/// <param name="position">Posicao (em world) para iniciar a criacao dos vertices.</param>
		/// <param name="sizeX">Numero de tiles em x (largura).</param>
		/// <param name="sizeY">Numero de tiles em y (altura).</param>
		/// <param name="worldSizeX">World size x.</param>
		/// <param name="worldSizeY">World size y.</param>
		protected override void createMesh(Vector3 position, int worldSizeX, int worldSizeY)
		{	
			base.createMesh(position,worldSizeX,worldSizeY);
			meshCollider.sharedMesh = meshFilter.sharedMesh;		
		}

	}
}