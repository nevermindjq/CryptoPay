using System.Text.Json.Serialization;

#pragma warning disable CS1591
namespace CryptoPay.Types {
	/// <summary>
	///     Type of the price, can be <see cref="Crypto" /> or <see cref="Fiat" />.
	/// </summary>
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum CurrencyTypes {
		Crypto,
		Fiat
	}
}