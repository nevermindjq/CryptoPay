using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CryptoPay.Types {
	/// <summary>
	///     All requests from Crypto Pay API has this JSON object.
	///     <para />
	///     You can verify use method <see cref="CryptoPayHelper.CheckSignature" /> the received update and the integrity of
	///     the received data by comparing the header parameter crypto-pay-api-signature
	///     and the hexadecimal representation of HMAC-SHA-256 signature used to sign the entire request body (unparsed JSON
	///     string)
	///     with a secret key that is SHA256 hash of your app's token.
	/// </summary>
	public sealed class Update {
		/// <summary>
		///     Non-unique update ID.
		/// </summary>
		[JsonRequired, JsonPropertyName("update_id"), JsonPropertyOrder(0)]
		public long UpdateId { get; set; }

		/// <summary>
		///     Webhook update type.Supported update types:
		///     <see cref="UpdateTypes.invoice_paid" /> – the update sent after an invoice is paid.
		/// </summary>
		[JsonRequired, JsonPropertyName("update_type"), JsonPropertyOrder(1)]
		public UpdateTypes UpdateType { get; set; }

		/// <summary>
		///     Date the request was sent in ISO 8601 format.
		/// </summary>
		[JsonRequired, JsonPropertyName("request_date"), JsonPropertyOrder(2)]
		public DateTime RequestDate { get; set; }

		/// <summary>
		///     Payload of the update.
		/// </summary>
		[JsonPropertyName("payload"), JsonPropertyOrder(3)]
		public Invoice Payload { get; set; }

		/// <summary>
		///     Serialize object to string.
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			return JsonSerializer.Serialize(
				this,
				new JsonSerializerOptions {
					DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
				}
			);
		}
	}
}