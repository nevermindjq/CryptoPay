using System;

using CryptoPay.Types;

// ReSharper disable once CheckNamespace
namespace CryptoPay {
	/// <summary>
	///     Helper class for working with assets in the CryptoPay.
	/// </summary>
	public static class AssetsHelper {
		/// <summary>
		///     Tries to parse a string representation of an asset into its corresponding Assets enum value.
		/// </summary>
		/// <param name="asset_as_text">The string representation of the asset.</param>
		/// <returns>The parsed Assets enum value if successful, otherwise <see cref="Assets.Unknown" />.</returns>
		public static Assets TryParse(string asset_as_text) {
			return Enum.TryParse<Assets>(asset_as_text, true, out var asset) ? asset : Assets.Unknown;
		}
	}
}