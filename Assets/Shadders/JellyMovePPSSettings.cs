// Amplify Shader Editor - Visual Shader Editing Tool
// Copyright (c) Amplify Creations, Lda <info@amplify.pt>
#if UNITY_POST_PROCESSING_STACK_V2
using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess( typeof( JellyMovePPSRenderer ), PostProcessEvent.AfterStack, "JellyMove", true )]
public sealed class JellyMovePPSSettings : PostProcessEffectSettings
{
	[Tooltip( "flowmap" )]
	public TextureParameter _flowmap = new TextureParameter {  };
	[Tooltip( "Intensity" )]
	public FloatParameter _Intensity = new FloatParameter { value = 0.15f };
}

public sealed class JellyMovePPSRenderer : PostProcessEffectRenderer<JellyMovePPSSettings>
{
	public override void Render( PostProcessRenderContext context )
	{
		var sheet = context.propertySheets.Get( Shader.Find( "JellyMove" ) );
		if(settings._flowmap.value != null) sheet.properties.SetTexture( "_flowmap", settings._flowmap );
		sheet.properties.SetFloat( "_Intensity", settings._Intensity );
		context.command.BlitFullscreenTriangle( context.source, context.destination, sheet, 0 );
	}
}
#endif
