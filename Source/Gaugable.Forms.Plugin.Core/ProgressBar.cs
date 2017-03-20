using System;
using SkiaSharp;
using System.Linq;
using System.Collections.Generic;
using SkiaSharp.Views.Forms;

namespace Gaugable.Forms.Plugin.Core
{
	/// <summary>
	/// The Progress Bar.
	/// </summary>
	internal class ProgressBar
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Gaugable.Forms.Plugin.Core.ProgressBar"/> class.
		/// </summary>
		internal ProgressBar()
		{
		}

		private float progress;

		/// <summary>
		/// Gets or sets the size of the container.
		/// </summary>
		/// <value>The size of the container.</value>
		internal ISize ContainerSize { get; set; }

		/// <summary>
		/// Gets or sets the progress bar color.
		/// </summary>
		/// <value>The color.</value>
		internal Xamarin.Forms.Color Color { get; set; }

		internal Gauge Gauge { get; set; }

		/// <summary>
		/// Gets or sets the progress.
		/// </summary>
		/// <value>The progress.</value>
		internal float Progress
		{
			get { return this.progress; }

			set
			{
				if (this.MaxProgress < value)
				{
					this.progress = this.MaxProgress;
				}
				else if (value < this.MinProgress)
				{
					this.progress = this.MinProgress;
				}
				else
				{
					this.progress = value;
				}

			}
		}

		/// <summary>
		/// Gets or sets the max progress value.
		/// </summary>
		/// <value>The max progress.</value>
		internal float MaxProgress { get; set; }

		/// <summary>
		/// Gets or sets the minimum progress value.
		/// </summary>
		/// <value>The minimum progress.</value>
		internal float MinProgress { get; set; }

		/// <summary>
		/// Gets the height of the progress bar. Currently hard set to one third of container height minus the scale height.
		/// </summary>
		/// <value>The height.</value>
		private float Height
		{
			get
			{
				return (float)(ContainerSize.Height * (1 - this.Gauge.GetScaleHeightAsPercent()) / 3);
			}
		}

		/// <summary>
		/// Gets the width of the progress bar.
		/// </summary>
		/// <value>The width.</value>
		private float Width
		{
			get
			{
				return ((float)((this.Progress - this.MinProgress) / (this.MaxProgress - this.MinProgress) * this.ContainerSize.Width));
			}
		}

		/// <summary>
		/// Draws the progress bar.
		/// </summary>
		/// <returns>The draw.</returns>
		/// <param name="canvas">Canvas.</param>
		/// <param name="paint">Paint.</param>
		internal void Draw(SKCanvas canvas, SKPaint paint)
		{
			// draw these at specific locations
			var progressBarRect = SKRect.Create(0, (float)(this.Height), this.Width, this.Height);

			// draw
			paint.Color = this.Color.ToSKColor();
			canvas.DrawRect(progressBarRect, paint);
		}

		/// <summary>
		/// Gets the width for the range values based on the range of the progress min and max values.
		/// That is, converts the progress values to the container visual dimensions.
		/// </summary>
		/// <returns>The width from range values.</returns>
		/// <param name="minValue">Minimum value.</param>
		/// <param name="maxValue">Max value.</param>
		internal double GetWidthFromRangeValues(int minValue, int maxValue)
		{
			// subtract the max from min and give as percentage of container width
			return this.ContainerSize.Width * ((maxValue - minValue) / (this.MaxProgress - this.MinProgress));
		}

		/// <summary>
		/// Gets the x value (in progress scale) scaled to the container size.
		/// </summary>
		/// <returns>The scaled x.</returns>
		/// <param name="minValue">Minimum value.</param>
		internal double GetScaledX(int minValue)
		{
			return (minValue * this.ContainerSize.Width) / (this.MaxProgress - this.MinProgress);
		}
	}
}
