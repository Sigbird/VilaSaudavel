Shader "YupiStudios/TileBased/TilePlacement" {
	
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}	
		_Color ("Multiplier", Color) = (1,1,1,1)	
	}
	
	SubShader {
	
		Tags {
			"Queue" = "Geometry"
			"RenderType" = "Opaque"
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
	        uniform fixed4 _Color;
	        
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
	        	
	            fixed4 c = tex2D(_MainTex, i.texcoord0) * _Color;
	            //c.rgb *= c.a;
	            return c;
	            
	        }
	        
	        ENDCG
            
		}
		
	} 
	FallBack "Sprites/Default"
}
