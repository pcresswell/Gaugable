using System;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;

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
			this.ProgressBar = new ProgressBar() { Gauge = this };
			this.HorizontalAxis = new Axis() { Gauge = this };
		}

		/// <summary>
		/// Gets the progress bar.
		/// </summary>
		/// <value>The progress bar.</value>
		internal ProgressBar ProgressBar
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the horizontal axis.
		/// </summary>
		/// <value>The horizontal axis.</value>
		internal Axis HorizontalAxis
		{
			get;
			set;
		}

		/// <summary>
		/// The minor ticks property.
		/// </summary>
		public static readonly BindableProperty MinorTicksProperty =
			BindableProperty.Create(propertyName: nameof(MinorTicks),
								  returnType: typeof(bool),
			  declaringType: typeof(Gauge),
									defaultValue: false, propertyChanged: (bindable, oldValue, newValue) =>
									  {
										  Gauge g = (Gauge)bindable;
										  g.InvalidateSurface();
									  });

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:Gaugable.Forms.Plugin.Core.Gauge"/> minor ticks.
		/// </summary>
		/// <value><c>true</c> if minor ticks; otherwise, <c>false</c>.</value>
		public bool MinorTicks
		{
			get { return (bool)GetValue(MinorTicksProperty); }
			set { SetValue(MinorTicksProperty, value); }
		}

		/// <summary>
		/// The major tick increment property.
		/// </summary>
		public static readonly BindableProperty MinorTickIncrementProperty =
			BindableProperty.Create(propertyName: nameof(MinorTickIncrement),
								  	returnType: typeof(float),
			  						declaringType: typeof(Gauge),
			  						defaultValue: 5f, propertyChanged: (bindable, oldValue, newValue) =>
									   {
										   Gauge g = (Gauge)bindable;
										   g.InvalidateSurface();
									   });

		/// <summary>
		/// Gets or sets the minor tick increment.
		/// </summary>
		/// <value>The minor tick increment.</value>
		public float MinorTickIncrement
		{
			get { return (float)GetValue(MinorTickIncrementProperty); }
			set { SetValue(MinorTickIncrementProperty, value); }
		}

		/// <summary>
		/// The major tick increment property.
		/// </summary>
		public static readonly BindableProperty MajorTickIncrementProperty =
			BindableProperty.Create(propertyName: nameof(MajorTickIncrement),
								 	 	returnType: typeof(float),
			  							declaringType: typeof(Gauge),
										defaultValue: 30f,
										propertyChanged: (bindable, oldValue, newValue) =>
									  {
										  Gauge g = (Gauge)bindable;
										  g.InvalidateSurface();
									  });

		/// <summary>
		/// Gets or sets the major tick increment.
		/// </summary>
		/// <value>The major tick increment.</value>
		public float MajorTickIncrement
		{
			get { return (float)GetValue(MajorTickIncrementProperty); }
			set { SetValue(MajorTickIncrementProperty, value); }
		}

		/// <summary>
		/// The major ticks property.
		/// </summary>
		public static readonly BindableProperty MajorTicksProperty =
			BindableProperty.Create(propertyName: nameof(MajorTicks),
								  	returnType: typeof(bool),
								  	declaringType: typeof(Gauge),
								  	defaultValue: false,
								  	propertyChanged: (bindable, oldValue, newValue) =>
									  {
										  Gauge g = (Gauge)bindable;
										  g.InvalidateSurface();
									  });

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:Gaugable.Forms.Plugin.Core.Gauge"/> major ticks.
		/// </summary>
		/// <value><c>true</c> if major ticks; otherwise, <c>false</c>.</value>
		public bool MajorTicks
		{
			get { return (bool)GetValue(MajorTicksProperty); }
			set { SetValue(MajorTicksProperty, value); }
		}

		/// <summary>
		/// The minimum property.
		/// </summary>
		public static readonly BindableProperty MinProgressProperty =
		  BindableProperty.Create(propertyName: nameof(MinProgress),
									returnType: typeof(int),
									declaringType: typeof(Gauge),
									defaultValue: 0,
								  propertyChanged: (bindable, oldValue, newValue) =>
								  {
									  Gauge g = (Gauge)bindable;
									  g.InvalidateSurface();
								  });

		/// <summary>
		/// Gets or sets the minimum.
		/// </summary>
		/// <value>The minimum.</value>
		public int MinProgress
		{
			get { return (int)GetValue(MinProgressProperty); }
			set { SetValue(MinProgressProperty, value); }
		}

		/// <summary>
		/// The minimum property.
		/// </summary>
		public static readonly BindableProperty MaxProgressProperty =
		  BindableProperty.Create(propertyName: nameof(MaxProgress),
			  						returnType: typeof(int),
			  						declaringType: typeof(Gauge),
								   defaultValue: 100,
								   propertyChanged: (bindable, oldValue, newValue) =>
								   {
									   Gauge g = (Gauge)bindable;
									   g.InvalidateSurface();
								   });

		/// <summary>
		/// Gets or sets the maximum progress value.
		/// </summary>
		/// <value>The maximum.</value>
		public int MaxProgress
		{
			get { return (int)GetValue(MaxProgressProperty); }
			set { SetValue(MaxProgressProperty, value); }
		}

		/// <summary>
		/// Progress Bar Color.
		/// </summary>
		public static readonly BindableProperty AxisColorProperty =
		  BindableProperty.Create(propertyName: nameof(AxisColor),
								  returnType: typeof(Xamarin.Forms.Color),
								  declaringType: typeof(Gauge),
								  defaultValue: Xamarin.Forms.Color.Gray,
								  propertyChanged: (bindable, oldvalue, newvalue) =>
			{
				Gauge gauge = (Gauge)bindable;
				gauge.InvalidateSurface();
			});

		/// <summary>
		/// Gets or sets the axis color.
		/// </summary>
		/// <value>The color.</value>
		public Xamarin.Forms.Color AxisColor
		{
			get { return (Xamarin.Forms.Color)GetValue(AxisColorProperty); }
			set { SetValue(AxisColorProperty, value); }
		}

		/// <summary>
		/// Progress Bar Color.
		/// </summary>
		public static readonly BindableProperty ColorProperty =
		  BindableProperty.Create(propertyName: nameof(Color),
								  returnType: typeof(Xamarin.Forms.Color),
								  declaringType: typeof(Gauge),
								  defaultValue: Xamarin.Forms.Color.Gray,
								  propertyChanged: (bindable, oldvalue, newvalue) =>
									{
										Gauge gauge = (Gauge)bindable;
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
		/// Gets the Axis height as percent of container. 
		/// Currently set to 0.1 if Major or Minor ticks are enabled.
		/// </summary>
		/// <returns>The axis height as percent.</returns>
		internal float GetScaleHeightAsPercent()
		{
			if (this.MajorTicks || this.MinorTicks)
				return GaugeLayoutConstants.SCALE_HEIGHT_PERCENT;

			return 0f;
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
				if (range.Gauge != this)
				{
					range.Gauge = this;
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

				this.HorizontalAxis.Draw(canvas, paint);
				this.ProgressBar.Draw(canvas, paint);

				canvas.Save();
			}
		}
	}
}
