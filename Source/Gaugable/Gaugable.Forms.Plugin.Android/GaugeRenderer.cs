using System;
using Xamarin.Forms.Platform;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;
using Gaugable.Forms.Plugin.Core;
using Gaugable.Forms.Plugin.Android;
using SkiaSharp;
using SkiaSharp.Views.Forms;

[assembly: ExportRenderer(typeof(Gauge), typeof(GaugeRenderer))]
namespace Gaugable.Forms.Plugin.Android
{
	/// <summary>
	/// Gauge renderer.
	/// </summary>
	public class GaugeRenderer : SKCanvasViewRenderer
	{
		public GaugeRenderer()
		{

		}

		public new static void Init()
		{
			var d = DateTime.Now;
		}
	}
}
