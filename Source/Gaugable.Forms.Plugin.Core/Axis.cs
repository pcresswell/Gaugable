using System;
using SkiaSharp;
using Xamarin.Forms;
using SkiaSharp.Views.Forms;
using System.Collections.Generic;

namespace Gaugable.Forms.Plugin.Core
{
	/// <summary>
	/// The Horizontal axis.
	/// </summary>
	public class Axis : BindableObject
	{
		public Axis()
		{

		}

		/// <summary>
		/// Are major ticks enabled?
		/// </summary>
		/// <value><c>true</c> if major ticks; otherwise, <c>false</c>.</value>
		private bool MajorTicks
		{
			get
			{
				return this.Gauge.MajorTicks;
			}
		}

		/// <summary>
		/// Are minor ticks enabled?
		/// </summary>
		/// <value><c>true</c> if minor ticks; otherwise, <c>false</c>.</value>
		private bool MinorTicks
		{
			get
			{
				return this.Gauge.MinorTicks;
			}
		}

		/// <summary>
		/// Axis color.
		/// </summary>
		/// <value>The color.</value>
		internal Xamarin.Forms.Color Color
		{
			get { return this.Gauge.AxisColor; }
		}

		/// <summary>
		/// Gets or sets the gauge.
		/// </summary>
		/// <value>The gauge.</value>
		internal Gauge Gauge { get; set; }

		/// <summary>
		/// Gets the progress bar.
		/// </summary>
		/// <value>The progress bar.</value>
		private ProgressBar ProgressBar
		{
			get
			{
				return this.Gauge.ProgressBar;
			}
		}

		/// <summary>
		/// Gets the top left point for drawing purposes.
		/// </summary>
		/// <value>The top left point.</value>
		internal IPoint TopLeftPoint
		{
			get
			{
				return new Point(0, 0);
			}
		}

		/// <summary>
		/// Gets the major tick increment.
		/// </summary>
		/// <value>The major tick increment.</value>
		private float MajorTickIncrement
		{
			get
			{
				return this.Gauge.MajorTickIncrement;
			}
		}

		/// <summary>
		/// Gets the minor tick increment.
		/// </summary>
		/// <value>The minor tick increment.</value>
		private float MinorTickIncrement
		{
			get
			{
				return this.Gauge.MinorTickIncrement;
			}
		}

		/// <summary>
		/// Draws the range on the container.
		/// </summary>
		/// <returns>The draw.</returns>
		/// <param name="canvas">Canvas.</param>
		/// <param name="paint">Paint.</param>
		internal void Draw(SKCanvas canvas, SKPaint paint)
		{
			ISet<int> majorTicks = new HashSet<int>();

			this.DrawMajorTicks(majorTicks, canvas);
			this.DrawMinorTicks(majorTicks, canvas);
		}

		/// <summary>
		/// Gets the height.
		/// </summary>
		/// <value>The height.</value>
		private float Height
		{
			get { return (float)(this.ContainerSize.Height * Gauge.GetScaleHeightAsPercent()); }
		}

		/// <summary>
		/// Gets the width.
		/// </summary>
		/// <value>The width.</value>
		private float Width
		{
			get { return (float)(this.ContainerSize.Width); }
		}

		/// <summary>
		/// Gets the size of the container from the progress bar.
		/// </summary>
		/// <value>The size of the container.</value>
		private ISize ContainerSize { get { return this.ProgressBar.ContainerSize; } }

		/// <summary>
		/// Draws the major ticks.
		/// </summary>
		/// <param name="majorTicks">Major ticks.</param>
		/// <param name="canvas">Canvas.</param>
		private void DrawMajorTicks(ISet<int> majorTicks, SKCanvas canvas)
		{
			var pathStroke = new SKPaint
			{
				IsAntialias = true,
				Style = SKPaintStyle.Stroke,
				Color = this.Color.ToSKColor(),
				StrokeWidth = 5
			};

			for (int i = (int)Math.Max(this.ProgressBar.MinProgress, this.MajorTickIncrement); i < this.ProgressBar.MaxProgress; i = (int)(i + this.MajorTickIncrement))
			{
				// create a path
				int xPosition = (int)this.ProgressBar.GetScaledX(i);
				var path = new SKPath();
				path.MoveTo(xPosition, (float)this.TopLeftPoint.Y);
				path.LineTo(xPosition, ((float)(this.TopLeftPoint.Y + this.Height)));
				if (this.MajorTicks)
				{
					majorTicks.Add(i);
					canvas.DrawPath(path, pathStroke);
				}
			}
		}

		/// <summary>
		/// Draws the minor ticks.
		/// </summary>
		/// <param name="majorTicks">Major ticks.</param>
		/// <param name="canvas">Canvas.</param>
		private void DrawMinorTicks(ISet<int> majorTicks, SKCanvas canvas)
		{
			var pathStroke = new SKPaint
			{
				IsAntialias = true,
				Style = SKPaintStyle.Stroke,
				Color = this.Color.ToSKColor(),
				StrokeWidth = 1
			};

			if (this.MinorTicks)
			{
				for (int i = (int)Math.Max(this.ProgressBar.MinProgress, this.MinorTickIncrement); i < this.ProgressBar.MaxProgress; i = (int)(i + this.MinorTickIncrement))
				{
					// create a path
					int xPosition = (int)this.ProgressBar.GetScaledX(i);
					var path = new SKPath();
					path.MoveTo(xPosition, (float)this.TopLeftPoint.Y);
					path.LineTo(xPosition, ((float)(this.TopLeftPoint.Y + (this.Height / 2)))); // minor ticks are half the height.

					// only draw where there isn't a major tick.
					if (!majorTicks.Contains(i))
					{
						canvas.DrawPath(path, pathStroke);
					}

				}
			}
		}
	}

}
