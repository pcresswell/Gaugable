using System;
namespace Gaugable.Forms.Plugin.Core
{
	/// <summary>
	/// Simple size interface.
	/// </summary>
	public interface ISize
	{
		/// <summary>
		/// Gets the width.
		/// </summary>
		/// <value>The width.</value>
		double Width { get; }

		/// <summary>
		/// Gets the height.
		/// </summary>
		/// <value>The height.</value>
		double Height { get; }
	}

}
