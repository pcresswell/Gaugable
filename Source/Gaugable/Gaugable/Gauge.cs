using System;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms.Platform;
using System.Linq;
using System.Collections.Generic;

namespace Gaugable.Forms.Plugin.Core
{
	/// <summary>
	/// Gauge View control.
	/// </summary>
	public class Gauge : SKCanvasView
	{
		public Gauge()
		{
			this.PaintSurface += Handle_PaintSurface;
			this.ProgressBar = new ProgressBar();
		}

		private ProgressBar ProgressBar { get; set; }

		/// <summary>
		/// Progress Bar Color.
		/// </summary>
		public static readonly BindableProperty ColorProperty =
		  BindableProperty.Create(propertyName: nameof(Color),
								  returnType: typeof(Xamarin.Forms.Color),
									declaringType: typeof(Range),
								  defaultValue: Xamarin.Forms.Color.Gray, propertyChanged: (bindable, oldvalue, newvalue) =>
			{
				Gauge gauge = (Gauge)bindable;
				gauge.ProgressBar.Color = (Xamarin.Forms.Color)newvalue;
				gauge.InvalidateSurface();
			});

		/// <summary>
		/// Gets or sets the progress bar color.
		/// </summary>
		/// <value>The color.</value>
		public Xamarin.Forms.Color Color
		{
			get { return (Xamarin.Forms.Color)GetValue(ColorProperty); }
			set { SetValue(ColorProperty, value); }
		}

		/// <summary>
		/// The progress property.
		/// </summary>
		public static readonly BindableProperty ProgressProperty =
		  BindableProperty.Create(propertyName: nameof(Progress),
			  returnType: typeof(int),
			  declaringType: typeof(Gauge),
			  defaultValue: 0, propertyChanged: OnProgressPropertyChanged);

		/// <summary>
		/// Gets or sets the progress.
		/// </summary>
		/// <value>The progress.</value>
		public int Progress
		{
			get { return (int)GetValue(ProgressProperty); }
			set { SetValue(ProgressProperty, value); }
		}

		/// <summary>
		/// Calls OnProgressPropertyChanged Event on bound object.
		/// </summary>
		/// <param name="bindable">Bindable.</param>
		/// <param name="oldValue">Old value.</param>
		/// <param name="newValue">New value.</param>
		static void OnProgressPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			((Gauge)bindable).OnProgressPropertyChanged((int)oldValue, (int)newValue);
		}

		/// <summary>
		/// Called when the progress property changed
		/// </summary>
		/// <param name="oldValue">Old value.</param>
		/// <param name="newValue">New value.</param>
		private void OnProgressPropertyChanged(int oldValue, int newValue)
		{
			this.ProgressBar.Progress = newValue;
			this.InvalidateSurface();
		}

		/// <summary>
		/// The range definition property.
		/// </summary>
		public static readonly BindableProperty RangeDefinitionProperty = BindableProperty.Create("RangeDefinition", typeof(RangeDefinition), typeof(Gauge), null,
			validateValue: (bindable, value) => value != null, propertyChanged: (bindable, oldvalue, newvalue) =>
			{
				if (oldvalue != null)
					((RangeDefinition)oldvalue).ItemSizeChanged -= ((Gauge)bindable).OnRangeDefinitionChanged;
				if (newvalue != null)
					((RangeDefinition)newvalue).ItemSizeChanged += ((Gauge)bindable).OnRangeDefinitionChanged;
			}, defaultValueCreator: bindable =>
			{
				var colDef = new RangeDefinition();
				colDef.ItemSizeChanged += ((Gauge)bindable).OnRangeDefinitionChanged;
				return colDef;
			});

		/// <summary>
		/// Gets or sets the range definition.
		/// </summary>
		/// <value>The range definition.</value>
		public RangeDefinition RangeDefinition
		{
			get { return (RangeDefinition)GetValue(RangeDefinitionProperty); }
			set { SetValue(RangeDefinitionProperty, value); }
		}

		/// <summary>
		/// Called when RangeDefinition object is changed.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		private void OnRangeDefinitionChanged(object sender, EventArgs args)
		{
			foreach (var range in this.RangeDefinition)
			{
				if (range.ProgressBar != this.ProgressBar)
				{
					range.ProgressBar = this.ProgressBar;
				}
			}
		}

		/// <summary>
		/// Handles the painting of the control.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void Handle_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
		{
			var canvas = e.Surface.Canvas;
			var width = e.Info.Width;
			var height = e.Info.Height;

			using (var paint = new SKPaint())
			{
				paint.IsAntialias = true;
				canvas.Clear(this.BackgroundColor.ToSKColor());
				this.ProgressBar.ContainerSize = Size.Create(width, height);

				foreach (var range in this.RangeDefinition)
				{
					range.Draw(canvas, paint);
				}

				this.ProgressBar.Draw(canvas, paint);

				canvas.Save();
			}
		}
	}
}
