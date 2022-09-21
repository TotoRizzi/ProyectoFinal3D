// Amplify Shader Editor - Visual Shader Editing Tool
// Copyright (c) Amplify Creations, Lda <info@amplify.pt>
#if UNITY_POST_PROCESSING_STACK_V2
using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess( typeof( TvNoiseDistortPPSRenderer ), PostProcessEvent.AfterStack, "TvNoiseDistort", true )]
public sealed class TvNoiseDistortPPSSettings : PostProcessEffectSettings
{
	[Tooltip( "Intensity" )]
	public FloatParameter _Intensity = new FloatParameter { value = 0.15f };
	[Tooltip( "tv noise" )]
	public TextureParameter _tvnoise = new TextureParameter {  };
}

public sealed class TvNoiseDistortPPSRenderer : PostProcessEffectRenderer<TvNoiseDistortPPSSettings>
{
	public override void Render( PostProcessRenderContext context )
	{
		var sheet = context.propertySheets.Get( Shader.Find( "TvNoiseDistort" ) );
		sheet.properties.SetFloat( "_Intensity", settings._Intensity );
		if(settings._tvnoise.value != null) sheet.properties.SetTexture( "_tvnoise", settings._tvnoise );
		context.command.BlitFullscreenTriangle( context.source, context.destination, sheet, 0 );
	}
}
#endif
