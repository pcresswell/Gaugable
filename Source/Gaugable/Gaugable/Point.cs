using System;
namespace Gaugable.Forms.Plugin.Core
{
	/// <summary>
	/// Simple Point implementation for IPoint.
	/// </summary>
	internal class Point : IPoint
	{
		internal Point(double x, double y)
		{
			this.X = x;
			this.Y = y;
		}

		double IPoint.X
		{
			get
			{
				return this.X;
			}
		}

		double IPoint.Y
		{
			get
			{
				return this.Y;
			}
		}

		internal double X { get; set; }
		internal double Y { get; set; }

	}
}
