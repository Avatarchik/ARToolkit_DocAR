// Mr Gerallt G. Franke 2016

Shader "Custom/MRI_Image_OpacityMap" {
   Properties {
   // _AlphaColor ("Alpha Color", Color) = (0.4, 0.4, 0.4, 1.0)
    _MainTex ("Texture", 2D) = "black" {}
    //_AlphaMap ("Texture", 2D) = "black" {}
    //_Cutoff ("Alpha cutoff", Range(0,1)) = 0.2
    //threshold ("threshold", Color) = (0.6, 0.6, 0.6, 1.0)
   }
   Category {
	   Tags { 
	    	"RenderType" = "opaque" 
	    	"Queue"="Geometry+5" 

	    }
   		SubShader {
    		Pass {
    		//Cull Front
    		ZWrite Off

             GLSLPROGRAM
			 #version 150
             // 0.4, 0.4, 0.4
             const vec4 threshold = vec4(0.7, 0.7, 0.7, 1.0); // transparency threshold
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
             //uniform vec4 threshold;
             uniform sampler2D _MainTex;
             //uniform sampler2D _AlphaMap;
             void main() 
             {
             	vec4 c = texture2D(_MainTex, uv);
             	//vec4 alp = texture2D(_AlphaMap, c.rg);

             	//c = c * alp;

//             	// color replace function
//             	if(c.r > 0 && c.r < 0.2
//             	&& c.g > 0 && c.g < 0.2 
//             	&& c.b > 0 && c.b < 0.2
//             	){
//             		//c.r = c.r  + 0.01;
//             	}
//
////             	if(c.r > 0.2 && c.r < 0.6
////             	&& c.g > 0.2 && c.g < 0.6 
////             	&& c.b > 0.2 && c.b < 0.6
////             	){
////             		c.r = c.r + 0.4;
////             	}
//
//             	if(c.r > 0.6 && c.r < 0.8
//             	&& c.g > 0.6 && c.g < 0.8 
//             	&& c.b > 0.6 && c.b < 0.8
//             	){
//             		c.r = c.r + 0.2;
//             	}

             	gl_FragColor = c;

             	// transparency thresholding
                 if (c.r < threshold.x 
                 	&& c.g < threshold.y 
                 		&& c.b < threshold.z) 
                     discard;
             }
             #endif      
             ENDGLSL 
			}
		} 
	}
}