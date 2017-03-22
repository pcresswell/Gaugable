using System;
using SkiaSharp;
using Xamarin.Forms;
using SkiaSharp.Views.Forms;
using System.Collections.Generic;

namespace Gaugable.Forms.Plugin.Core
{
	public class Axis : BindableObject
	{
		public Axis()
		{

		}

		private bool MajorTicks
		{
			get
			{
				return this.Gauge.MajorTicks;
			}
		}

		private bool MinorTicks
		{
			get
			{
				return this.Gauge.MinorTicks;
			}
		}

		internal Xamarin.Forms.Color Color
		{
			get { return this.Gauge.AxisColor; }
		}

		internal Gauge Gauge { get; set; }

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

		private float MajorTickIncrement
		{
			get
			{
				return this.Gauge.MajorTickIncrement;
			}
		}

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


			// draw these at specific locations
			var rect = SKRect.Create((float)this.TopLeftPoint.X, (float)this.TopLeftPoint.Y, (float)this.Width, (float)this.Height);

			var pathStroke = new SKPaint
			{
				IsAntialias = true,
				Style = SKPaintStyle.Stroke,
				Color = this.Color.ToSKColor(),
				StrokeWidth = 5
			};

			ISet<int> majorTicks = new HashSet<int>();

			for (int i = (int)Math.Max(this.ProgressBar.MinProgress, this.MajorTickIncrement); i < this.ProgressBar.MaxProgress; i = (int)(i + this.MajorTickIncrement))
			{
				// create the paint for the path


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


			pathStroke = new SKPaint
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
					// create the paint for the path


					// create a path
					int xPosition = (int)this.ProgressBar.GetScaledX(i);
					var path = new SKPath();
					path.MoveTo(xPosition, (float)this.TopLeftPoint.Y);
					path.LineTo(xPosition, ((float)(this.TopLeftPoint.Y + (this.Height / 2))));
					if (!majorTicks.Contains(i))
					{
						canvas.DrawPath(path, pathStroke);
					}

				}
			}
			// draw

		}

		private float Height
		{
			get { return (float)(this.ContainerSize.Height * Gauge.GetScaleHeightAsPercent()); }
		}

		private float Width
		{
			get { return (float)(this.ContainerSize.Width); }
		}

		/// <summary>
		/// Gets the size of the container from the progress bar.
		/// </summary>
		/// <value>The size of the container.</value>
		private ISize ContainerSize { get { return this.ProgressBar.ContainerSize; } }

	}

}
