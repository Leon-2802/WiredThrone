Shader "Unlit/ToonShaderDark"
{
    Properties
    {
        _Albedo("Albedo", Color) = (1, 1, 1, 1)
        _Shades("Shades", Range(1, 20)) = 3
        _OutlineColor("OutlineColor", Color) = (0, 0, 0, 0)
        _OutlineSize("OutlineSize", float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Name "Toon"
            Tags {"LightMode" = "SRPDefaultUnlit"}

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
            };

            float4 _Albedo;
            float _Shades;

            //vertex shader
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            //fragment shader
            fixed4 frag (v2f i) : SV_Target
            {
                //calculate the cosine if the angle between the normal vector and the light direction 
                //The world space light direction is stored in _WorldSpaceLightPos0
                //The World space normal is stored in i.worldNormal
                //All what we have to do now is to normalize both vectors and calculate the dot product
                float cosineAngle = dot(normalize(i.worldNormal), normalize(_WorldSpaceLightPos0.xyz));

                //set the min to zero as result can be negative in cases where the light is behind the shaded point
                cosineAngle = max(0.0, cosineAngle);
                //Quatize the diffuse component
                cosineAngle = floor(cosineAngle * _Shades) / _Shades ;

                float4 ambient = _Albedo * 0.03;
                float4 color = ambient + _Albedo;

                return _Albedo * cosineAngle + ambient;
            }
            ENDCG 
        }

        //Outline rendering
        Pass
        {
            Name "Outline"
            Tags {"LightMode" = "Outline"}

            Cull Front

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            float4 _OutlineColor;
            float _OutlineSize;

            //vertex shader
            v2f vert (appdata v)
            {
                v2f o;
                //Translate the vertex along the normal vector, this will increase the size of the model
                o.vertex = UnityObjectToClipPos(v.vertex + (_OutlineSize*0.01) * v.normal);
                return o;
            }

            //fragment shader
            fixed4 frag (v2f i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDCG 
        }


        //Pass for Casting Shadows 
        Pass 
        {
            Name "CastShadow"
            Tags { "LightMode" = "ShadowCaster" }   

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_shadowcaster
            #include "UnityCG.cginc"

            struct v2f 
            { 
                V2F_SHADOW_CASTER;
            };

            v2f vert( appdata_base v )
            {
                v2f o;
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }

            float4 frag( v2f i ) : COLOR
            {
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
}