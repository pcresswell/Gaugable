﻿using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Gaugable.Forms.Plugin.iOS;

namespace Sample.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();
			Gaugable.Forms.Plugin.iOS.GaugeRenderer.Init();

			LoadApplication(new App());
			var t = typeof(Gaugable.Forms.Plugin.Core.Gauge);

			return base.FinishedLaunching(app, options);
		}
	}
}