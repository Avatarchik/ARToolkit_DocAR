Shader "Custom/LensMask_Shader" {
   Properties {
    _AlphaColor ("Alpha Color", Color) = (1.0, 0.0, 1.0, 1.0)
    _MainTex ("Texture", 2D) = "black" {}
    _Cutoff ("Alpha cutoff", Range(0,1)) = 0.2
   }
   Category {
	   Tags { 
	    	"RenderType" = "opaque" 
	    	"Queue"="Geometry+10" 

	    }
   		SubShader {
    		Pass {
             GLSLPROGRAM
             const vec3 threshhold = vec3(0.2, 0.2, 0.2); // transparency threshold

             #ifdef VERTEX
             out lowp vec2 uv;
             void main() 
             {
                 gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
                 uv = gl_MultiTexCoord0.xy;
             }
             #endif
      
             #ifdef FRAGMENT
             in vec2 uv;
             uniform sampler2D _MainTex;
             void main() 
             {
             	vec4 sampleColor = texture2D(_MainTex, uv);

             	gl_FragColor = sampleColor;

                 if (sampleColor.r < threshhold.x 
                 	&& sampleColor.g < threshhold.y 
                 		&& sampleColor.b < threshhold.z) 
                     discard;
             }
             #endif      
             ENDGLSL 
			}
		} 
	}
}