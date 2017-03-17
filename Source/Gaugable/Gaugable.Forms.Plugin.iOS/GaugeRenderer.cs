using System;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;
using System.Diagnostics;
using Foundation;
using CoreAnimation;
using CoreGraphics;
using Xamarin.Forms;
using Gaugable.Forms.Plugin.Core;
using Gaugable.Forms.Plugin.iOS;
using UIKit;
using RectangleF = CoreGraphics.CGRect;
using SizeF = CoreGraphics.CGSize;
using SkiaSharp;
using SkiaSharp.Views.Forms;



[assembly: ExportRenderer(typeof(Gauge), typeof(GaugeRenderer))]
namespace Gaugable.Forms.Plugin.iOS
{
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
