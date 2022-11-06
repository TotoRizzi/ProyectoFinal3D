// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "TrapGlow"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Size("Size", Range( 0 , 10)) = 1
		_TrapColorGlow("TrapColorGlow", Range( 0 , 1)) = 0
		_TrapTexture("TrapTexture", 2D) = "white" {}
		_Texture0("Texture 0", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _TrapColorGlow;
		uniform sampler2D _Texture0;
		uniform sampler2D _TrapTexture;
		uniform float4 _TrapTexture_ST;
		uniform float _Size;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TrapTexture = i.uv_texcoord * _TrapTexture_ST.xy + _TrapTexture_ST.zw;
			float2 temp_output_4_0_g4 = (( uv_TrapTexture / _Size )).xy;
			float2 temp_cast_0 = (uv_TrapTexture.y).xx;
			float2 temp_output_41_0_g4 = ( temp_cast_0 + 0.5 );
			float2 temp_output_17_0_g4 = float2( 0,2 );
			float mulTime22_g4 = _Time.y * 0.3;
			float temp_output_27_0_g4 = frac( mulTime22_g4 );
			float2 temp_output_11_0_g4 = ( temp_output_4_0_g4 + ( temp_output_41_0_g4 * temp_output_17_0_g4 * temp_output_27_0_g4 ) );
			float2 temp_output_12_0_g4 = ( temp_output_4_0_g4 + ( temp_output_41_0_g4 * temp_output_17_0_g4 * frac( ( mulTime22_g4 + 0.5 ) ) ) );
			float3 lerpResult9_g4 = lerp( UnpackNormal( tex2D( _Texture0, temp_output_11_0_g4 ) ) , UnpackNormal( tex2D( _Texture0, temp_output_12_0_g4 ) ) , ( abs( ( temp_output_27_0_g4 - 0.5 ) ) / 0.5 ));
			float4 tex2DNode70 = tex2D( _TrapTexture, uv_TrapTexture );
			float4 color43 = IsGammaSpace() ? float4(1,0,0,0.7019608) : float4(1,0,0,0.7019608);
			float4 temp_output_45_0 = ( ( float4( lerpResult9_g4 , 0.0 ) + tex2DNode70 ) * ( tex2DNode70 * color43 ) );
			float4 temp_output_49_0 = ( _TrapColorGlow * temp_output_45_0 );
			o.Albedo = temp_output_49_0.rgb;
			o.Emission = temp_output_49_0.rgb;
			o.Alpha = 1;
			clip( temp_output_45_0.r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
-35;389;1906;887;2573.218;277.6665;1.703195;True;False
Node;AmplifyShaderEditor.TexturePropertyNode;79;-1555.605,-92.22711;Inherit;True;Property;_Texture0;Texture 0;6;0;Create;True;0;0;0;False;0;False;None;5f97deaa83eb2fb40a25e1d603d4c23a;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.TextureCoordinatesNode;44;-1532.977,183.7263;Inherit;False;0;70;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;70;-1034.175,169.7969;Inherit;True;Property;_TrapTexture;TrapTexture;5;0;Create;True;0;0;0;False;0;False;-1;4dfdc23589a91d2498a67129684cb8ff;4dfdc23589a91d2498a67129684cb8ff;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;43;-968.6912,494.5768;Inherit;False;Constant;_Color0;Color 0;2;0;Create;True;0;0;0;False;0;False;1,0,0,0.7019608;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;76;-1183.682,-35.62866;Inherit;False;Flow;1;;4;acad10cc8145e1f4eb8042bebe2d9a42;2,50,1,51,1;5;5;SAMPLER2D;;False;2;FLOAT2;0,0;False;18;FLOAT2;0,0;False;17;FLOAT2;0,2;False;24;FLOAT;0.3;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;69;-728.2508,331.5749;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;80;-677.9729,87.78519;Inherit;True;2;2;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;45;-376.0255,327.1907;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;60;-768.2491,-42.94883;Inherit;False;Property;_TrapColorGlow;TrapColorGlow;4;0;Create;True;0;0;0;False;0;False;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;49;-352.7495,24.29898;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;295.7399,-2.622604E-06;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;TrapGlow;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Transparent;;Geometry;ForwardOnly;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;0;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;70;1;44;0
WireConnection;76;5;79;0
WireConnection;76;2;44;0
WireConnection;76;18;44;2
WireConnection;69;0;70;0
WireConnection;69;1;43;0
WireConnection;80;0;76;0
WireConnection;80;1;70;0
WireConnection;45;0;80;0
WireConnection;45;1;69;0
WireConnection;49;0;60;0
WireConnection;49;1;45;0
WireConnection;0;0;49;0
WireConnection;0;2;49;0
WireConnection;0;10;45;0
ASEEND*/
//CHKSM=1C4B918D6AE979AF02196FA4BD453BCE8AB36C40