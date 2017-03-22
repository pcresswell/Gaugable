using System;
using SkiaSharp;
using Xamarin.Forms;
using SkiaSharp.Views.Forms;
namespace Gaugable.Forms.Plugin.Core
{
	/// <summary>
	/// Represents a range on the gauge.
	/// </summary>
	public class Range : BindableObject, IDefinition
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Gaugable.Forms.Plugin.Core.Range"/> class.
		/// </summary>
		public Range()
		{
		}

		/// <summary>
		/// The minimum property.
		/// </summary>
		public static readonly BindableProperty MinProperty =
		  BindableProperty.Create(propertyName: nameof(Min),
			  returnType: typeof(int),
			  declaringType: typeof(Range),
			  defaultValue: 0);

		/// <summary>
		/// Gets or sets the minimum.
		/// </summary>
		/// <value>The minimum.</value>
		public int Min
		{
			get { return (int)GetValue(MinProperty); }
			set { SetValue(MinProperty, value); }
		}

		/// <summary>
		/// The color property.
		/// </summary>
		public static readonly BindableProperty ColorProperty =
		  BindableProperty.Create(propertyName: nameof(Color),
								  returnType: typeof(Xamarin.Forms.Color),
			  					  declaringType: typeof(Range),
								  defaultValue: Xamarin.Forms.Color.Gray);

		/// <summary>
		/// Gets or sets the color.
		/// </summary>
		/// <value>The color.</value>
		public Xamarin.Forms.Color Color
		{
			get { return (Xamarin.Forms.Color)GetValue(ColorProperty); }
			set { SetValue(ColorProperty, value); }
		}

		/// <summary>
		/// The max property.
		/// </summary>
		public static readonly BindableProperty MaxProperty =
		  BindableProperty.Create(propertyName: nameof(Max),
			  returnType: typeof(int),
			  declaringType: typeof(Range),
			  defaultValue: 100);

		/// <summary>
		/// Occurs when size changed.
		/// </summary>
		public event EventHandler SizeChanged;

		/// <summary>
		/// Gets or sets the max value.
		/// </summary>
		/// <value>The max.</value>
		public int Max
		{
			get { return (int)GetValue(MaxProperty); }
			set { SetValue(MaxProperty, value); }
		}

		/// <summary>
		/// Parent progress bar.
		/// TODO: Should probably be in the constructor.
		/// </summary>
		/// <value>The progress bar.</value>
		internal Gauge Gauge { get; set; }

		private ProgressBar ProgressBar { get { return this.Gauge.ProgressBar; } }

		/// <summary>
		/// Gets the size of the container from the progress bar.
		/// </summary>
		/// <value>The size of the container.</value>
		internal ISize ContainerSize { get { return this.Gauge.ProgressBar.ContainerSize; } }

		/// <summary>
		/// Gets the top left point for drawing purposes.
		/// </summary>
		/// <value>The top left point.</value>
		internal IPoint TopLeftPoint
		{
			get { return new Point(this.ProgressBar.GetScaledX(this.Min), 0); }
		}

		/// <summary>
		/// Draws the range on the container.
		/// </summary>
		/// <returns>The draw.</returns>
		/// <param name="canvas">Canvas.</param>
		/// <param name="paint">Paint.</param>
		internal void Draw(SKCanvas canvas, SKPaint paint)
		{
			// draw these at specific locations
			var rect = SKRect.Create((float)this.TopLeftPoint.X, (float)this.TopLeftPoint.Y, (float)this.Width, (float)this.Height);

			// draw
			paint.Color = this.Color.ToSKColor();
			canvas.DrawRect(rect, paint);
		}

		/// <summary>
		/// Gets the height of the range in visual scale.
		/// </summary>
		/// <value>The height.</value>
		private double Height
		{
			get
			{
				return this.ContainerSize.Height;
			}
		}

		/// <summary>
		/// Gets the width of the range in visual scale.
		/// </summary>
		/// <value>The width.</value>
		private double Width
		{
			get
			{
				return this.ProgressBar.GetWidthFromRangeValues(this.Min, this.Max);
			}
		}
	}
}
