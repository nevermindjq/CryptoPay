using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using CryptoPay.Converters;
using CryptoPay.Types.Abstractions;
using CryptoPay.Types.Currency;

namespace CryptoPay.Types.Update {
	/// <inheritdoc />
	public sealed class Invoice : IInvoice {
		#region Supported

		/// <summary>
		///     Unique ID for this invoice.
		/// </summary>
		[JsonPropertyName("invoice_id")]
        public long InvoiceId { get; set; }

		/// <summary>
		///     Hash of the invoice.
		/// </summary>
        [JsonPropertyName("hash")]
        public string Hash { get; set; }

		/// <inheritdoc />
        [JsonPropertyName("currency_type")]
        public CurrencyTypes CurrencyType { get; set; }

		/// <inheritdoc />
        [JsonPropertyName("asset")]
        public string Asset { get; set; }

		/// <inheritdoc />
        [JsonPropertyName("amount"), JsonConverter(typeof(JsonStringToDoubleConverter))]
        public double Amount { get; set; }

		/// <summary>
		///     Optional. Cryptocurrency alphabetic code for which the invoice was paid.
		///     Available only if <see cref="CurrencyType" /> is <see cref="CurrencyTypes.fiat" /> and status is
		///     <see cref="Statuses.paid" />.
		/// </summary>
        [JsonPropertyName("paid_asset")]
        public string PaidAsset { get; set; }

		/// <summary>
		///     Optional. Amount of the invoice for which the invoice was paid.
		///     Available only if <see cref="CurrencyType" /> is <see cref="CurrencyTypes.fiat" /> and status is
		///     <see cref="Statuses.paid" />.
		/// </summary>
        [JsonPropertyName("paid_amount"), JsonConverter(typeof(JsonStringToDoubleConverter))]
        public double PaidAmount { get; set; }

		/// <summary>
		///     Optional. Asset of service fees charged when the invoice was paid.
		///     Available only if status is <see cref="Statuses.paid" />.
		/// </summary>
        [JsonPropertyName("fee_asset")]
        public string FeeAsset { get; set; }

		/// <summary>
		///     Optional. Amount of service fees charged when the invoice was paid.
		///     Available only if status is <see cref="Statuses.paid" />.
		/// </summary>
        [JsonPropertyName("fee_amount"), JsonConverter(typeof(JsonStringToDoubleConverter))]
        public double FeeAmount { get; set; }

		/// <summary>
		///     Optional. Amount of charged service fees.
		/// </summary>
        [JsonPropertyName("fee")]
        public string Fee { get; set; }

		/// <summary>
		///		TODO add documentation
		/// </summary>
        [JsonPropertyName("fee_in_usd"), JsonConverter(typeof(JsonStringToDoubleConverter))]
        public double FeeInUsd { get; set; }

		/// <summary>
		///     URL should be presented to the user to pay the invoice.
		/// </summary>
        [JsonPropertyName("pay_url")]
        public string PayUrl { get; set; }

		/// <summary>
		///     URL should be provided to the user to pay the invoice.
		/// </summary>
        [JsonPropertyName("bot_invoice_url")]
        public string BotInvoiceUrl { get; set; }
		
		/// <summary>
		///		Use this URL to pay an invoice to the Telegram Mini App version.
		/// </summary>
        [JsonPropertyName("mini_app_invoice_url")]
        public string MiniAppInvoiceUrl { get; set; }
		
		/// <summary>
		///		Use this URL to pay an invoice to the Web version of Crypto Bot.
		/// </summary>
        [JsonPropertyName("web_app_invoice_url")]
        public string WebAppInvoiceUrl { get; set; }

		/// <summary>
		///     Status of the invoice, can be either “active”, “paid” or “expired”.
		/// </summary>
        [JsonPropertyName("status")]
        public Statuses Status { get; set; }

		/// <summary>
		///     Date the invoice was created in ISO 8601 format.
		/// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

		/// <inheritdoc />
        [JsonPropertyName("allow_comments")]
        public bool? AllowComments { get; set; }

		/// <summary>
		///     True, if the user can pay the invoice anonymously.
		/// </summary>
        [JsonPropertyName("allow_anonymous")]
        public bool AllowAnonymous { get; set; }

		/// <summary>
		///     Optional. Date the invoice expires in Unix time.
		/// </summary>
        [JsonPropertyName("expiration_date")]
        public DateTime ExpirationDate { get; set; }

		/// <summary>
		///     Optional. Price of the asset in USD. Available only if status is <see cref="Statuses.paid" />.
		/// </summary>
        [JsonPropertyName("paid_usd_rate"), JsonConverter(typeof(JsonStringToDoubleConverter))]
        public double PaidUsdRate { get; set; }

		/// <summary>
		///     Optional. Price of the asset in USD at the time the invoice was paid.
		/// </summary>
        [JsonPropertyName("usd_rate"), JsonConverter(typeof(JsonStringToDoubleConverter))]
        public double UsdRate { get; set; }

		/// <summary>
		///     Optional. Date the invoice was paid in Unix time.
		/// </summary>
        [JsonPropertyName("paid_at")]
        public DateTime PaidAt { get; set; }

		/// <summary>
		///     True, if the invoice was paid anonymously.
		/// </summary>
        [JsonPropertyName("paid_anonymously")]
        public bool PaidAnonymously { get; set; }

		/// <inheritdoc />
        [JsonPropertyName("payload")]
        public string Payload { get; set; }

		#endregion

		#region Unsupported

		/// <summary>
		///     Optional. Comment to the payment from the user.
		/// </summary>
		[JsonPropertyName("comment")]
		public string Comment { get; set; }
		
		/// <summary>
		///     Optional. Fiat currency code. Available only if the value of the field <see cref="CurrencyType" /> is
		///     <see cref="CurrencyTypes.fiat" />.
		///     Currently one of fiat from <see cref="Assets" />.
		/// </summary>
		[JsonPropertyName("fiat")]
		public string Fiat { get; set; }
		
		/// <summary>
		///     Optional. The rate of the paid_asset valued in the fiat currency.
		///     Available only if the value of the field <see cref="CurrencyType" /> is <see cref="CurrencyTypes.fiat" /> and the
		///     value of the field status is <see cref="Statuses.paid" />.
		/// </summary>
		[JsonPropertyName("paid_fiat_rate"), JsonConverter(typeof(JsonStringToDoubleConverter))]
		public double PaidFiatRate { get; set; }
		
		/// <inheritdoc />
		[JsonPropertyName("description")]
		public string Description { get; set; }
		
		/// <inheritdoc />
		[JsonPropertyName("hidden_message")]
		public string HiddenMessage { get; set; }
		
		/// <inheritdoc />
		[JsonPropertyName("paid_btn_name")]
		public PaidButtonNames? PaidBtnName { get; set; }
		
		/// <inheritdoc />
		[JsonPropertyName("paid_btn_url")]
		public string PaidBtnUrl { get; set; }
		
		/// <inheritdoc />
		[JsonPropertyName("accepted_assets")]
		public IEnumerable<string> AcceptedAssets { get; set; }

		#endregion
	}
}