using System;
namespace Gaugable.Forms.Plugin.Core
{
	/// <summary>
	/// Simple implementation for ISize
	/// </summary>
	internal class Size : ISize
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Gaugable.Forms.Plugin.Core.Size"/> class.
		/// </summary>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		internal Size(double width, double height)
		{
			this.Width = width;
			this.Height = height;
		}

		/// <summary>
		/// Gets the width of the gaugable. forms. plugin. core. IS ize..
		/// </summary>
		/// <value>The width of the gaugable. forms. plugin. core. IS ize..</value>
		double ISize.Width
		{
			get
			{
				return this.Width;
			}
		}

		/// <summary>
		/// Gets the height of the gaugable. forms. plugin. core. IS ize..
		/// </summary>
		/// <value>The height of the gaugable. forms. plugin. core. IS ize..</value>
		double ISize.Height
		{
			get
			{
				return this.Height;
			}
		}

		/// <summary>
		/// Gets or sets the width.
		/// </summary>
		/// <value>The width.</value>
		internal double Width { get; set; }

		/// <summary>
		/// Gets or sets the height.
		/// </summary>
		/// <value>The height.</value>
		internal double Height { get; set; }

		/// <summary>
		/// Create the specified width and height.
		/// </summary>
		/// <returns>The create.</returns>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		internal static Size Create(double width, double height)
		{
			return new Size(width, height);
		}
	}
}
