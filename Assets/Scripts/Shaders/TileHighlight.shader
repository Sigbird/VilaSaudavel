Shader "YupiStudios/TileBased/TileHighlight" {
	
	Properties {
		_MainTex ("Sprite Texture", 2D) = "white" {}
	}
	
	SubShader {
	
		Tags {
			"Queue"="Transparent-1"
			"RenderType"="Transparent" 
		}
	
		Cull Off
		ZWrite Off
		Lighting Off		
		Fog { Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha
	
		Pass {
			
			CGPROGRAM
			
	        #pragma vertex vert
	        #pragma fragment frag
	        
	        uniform sampler2D _MainTex;
	        
	        struct vertexInput {
	            float4 vertex : POSITION;
	            float4 texcoord0 : TEXCOORD0;
	        };

	        struct fragmentInput{
	            float4 position : SV_POSITION;
	            float4 texcoord0 : TEXCOORD0;
	        };

	        fragmentInput vert(vertexInput i) : POSITION {
	            fragmentInput o;
	            o.position = mul (UNITY_MATRIX_MVP, i.vertex);
	            o.texcoord0 = i.texcoord0;
	            return o;
	        }

	        fixed4 frag(fragmentInput i) : COLOR {
	        	
	            fixed4 c = tex2D(_MainTex, i.texcoord0);
	            c.a = 0.9;
	            return c;
	        }
	        
	        ENDCG
            
		}
		
	} 
	
		
	
}
