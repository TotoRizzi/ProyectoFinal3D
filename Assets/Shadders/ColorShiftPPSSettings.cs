// Amplify Shader Editor - Visual Shader Editing Tool
// Copyright (c) Amplify Creations, Lda <info@amplify.pt>
#if UNITY_POST_PROCESSING_STACK_V2
using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess( typeof( ColorShiftPPSRenderer ), PostProcessEvent.BeforeTransparent, "ColorShift", true )]
public sealed class ColorShiftPPSSettings : PostProcessEffectSettings
{
	[Tooltip( "Range" )]
	public FloatParameter _Range = new FloatParameter { value = 0f };
	[Tooltip( "Fuzz" )]
	public FloatParameter _Fuzz = new FloatParameter { value = 0f };
}

public sealed class ColorShiftPPSRenderer : PostProcessEffectRenderer<ColorShiftPPSSettings>
{
	public override void Render( PostProcessRenderContext context )
	{
		var sheet = context.propertySheets.Get( Shader.Find( "ColorShift" ) );
		sheet.properties.SetFloat( "_Range", settings._Range );
		sheet.properties.SetFloat( "_Fuzz", settings._Fuzz );
		context.command.BlitFullscreenTriangle( context.source, context.destination, sheet, 0 );
	}
}
#endif
