using System.Collections.Generic;
using System.Text.Json.Serialization;

using CryptoPay.Requests.Base;
using CryptoPay.Types;

namespace CryptoPay.Requests {
	/// <summary>
	///     Use this class to create <see cref="Invoice" /> request.
	/// </summary>
	public sealed class CreateInvoiceRequest
			: ParameterlessRequest<Invoice>,
					IInvoice {
		#region Constructors

		/// <summary>
		///     Initializes a new request to create <see cref="Invoice" />
		/// </summary>
		/// <param name="amount">Amount of the invoice in float. For example: 125.50</param>
		/// <param name="currency_type">
		///     Optional. Type of the price, can be <see cref="CurrencyTypes.Crypto" /> or
		///     <see cref="CurrencyTypes.Fiat" />. Defaults to crypto.
		/// </param>
		/// <param name="asset">
		///     Currency code.
		///     <remarks>
		///         Due to the fact that the list of available currencies in the CryptoPay service is constantly changing,
		///         utilizing <see cref="Assets" /> becomes ineffective. However, you can resort to using Assets.BTC.ToString()
		///         instead.
		///     </remarks>
		/// </param>
		/// <param name="fiat">
		///     Optional. Required if currencyType is <see cref="CurrencyTypes.Fiat" />. Fiat currency code.
		///     Supported fiat currencies from <see cref="Assets" />
		/// </param>
		/// <param name="accepted_assets">
		///     Optional. List of cryptocurrency alphabetic codes. Assets which can be used to pay the invoice.
		///     Available only if currencyType is <see cref="CurrencyTypes.Fiat" />. Supported assets from <see cref="Assets" />.
		///     Defaults to all currencies.
		/// </param>
		/// <param name="description">
		///     Optional. Description for the invoice. User will see this description when they pay the
		///     invoice. Up to 1024 characters.
		/// </param>
		/// <param name="hidden_message">
		///     Optional. Text of the message that will be shown to a user after the invoice is paid. Up to
		///     2048 characters.
		/// </param>
		/// <param name="paid_btn_name">
		///     Optional. Name of the button that will be shown to a user after the invoice is paid.
		///     <see cref="PaidButtonNames" />
		/// </param>
		/// <param name="paid_btn_url">
		///     Optional. Required if <see cref="PaidButtonNames">paidBtnName</see> is used. URL to be opened when the button is
		///     pressed.
		///     You can set any success link (for example, a link to your bot). Starts with https or http.
		/// </param>
		/// <param name="payload">
		///     Optional.Any data you want to attach to the invoice (for example, user ID, payment ID, ect). Up
		///     to 4kb.
		/// </param>
		/// <param name="allow_comments">Optional. Allow a user to add a comment to the payment. Default is true.</param>
		/// <param name="allow_anonymous">Optional. Allow a user to pay the invoice anonymously. Default is <c>true</c>.</param>
		/// <param name="expires_in">
		///     You can set a payment time limit for the invoice in <b>seconds</b>. Values between 1-2678400
		///     are accepted.
		/// </param>
		public CreateInvoiceRequest(
			double amount,
			CurrencyTypes currency_type = CurrencyTypes.Crypto,
			string asset = default,
			string fiat = default,
			IEnumerable<string> accepted_assets = default,
			string description = default,
			string hidden_message = default,
			PaidButtonNames? paid_btn_name = default,
			string paid_btn_url = default,
			string payload = default,
			bool allow_comments = true,
			bool allow_anonymous = true,
			int expires_in = 2678400)
				: base("createInvoice") {
			CurrencyType = currency_type;
			Amount = amount;
			Asset = asset;
			Fiat = fiat;
			AcceptedAssets = accepted_assets;
			Description = description;
			HiddenMessage = hidden_message;
			PaidBtnName = paid_btn_name;
			PaidBtnUrl = paid_btn_url;
			Payload = payload;
			AllowComments = allow_comments;
			AllowAnonymous = allow_anonymous;
			ExpiresIn = expires_in;
		}

		#endregion

		#region Public Fields

		/// <inheritdoc />
		public string Asset { get; set; }

		/// <inheritdoc />
		public string Fiat { get; set; }

		/// <inheritdoc />
		[JsonRequired]
		public double Amount { get; set; }

		/// <inheritdoc />
		public string Description { get; set; }

		/// <inheritdoc />
		public string HiddenMessage { get; set; }

		/// <inheritdoc />
		public PaidButtonNames? PaidBtnName { get; set; }

		/// <inheritdoc />
		public string PaidBtnUrl { get; set; }

		/// <inheritdoc />
		public string Payload { get; set; }

		/// <inheritdoc />
		public bool? AllowComments { get; set; }

		/// <inheritdoc />
		[JsonRequired]
		public CurrencyTypes CurrencyType { get; set; }

		/// <inheritdoc />
		public IEnumerable<string> AcceptedAssets { get; set; }

		/// <summary>
		///     Optional. Allow a user to pay the invoice anonymously. Default is true.
		/// </summary>
		public bool? AllowAnonymous { get; set; }

		/// <summary>
		///     Optional. You can set a payment time limit for the invoice in seconds. Values between 1-2678400 are accepted.
		/// </summary>
		public int ExpiresIn { get; set; }

		#endregion
	}
}