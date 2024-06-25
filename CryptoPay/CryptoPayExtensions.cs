using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using CryptoPay.Extensions;
using CryptoPay.Requests;
using CryptoPay.Types;
using CryptoPay.Types.Check;
using CryptoPay.Types.Currency;
using CryptoPay.Types.Transfer;
using CryptoPay.Types.Update;

namespace CryptoPay {
	/// <summary>
	///     Crypto Pay Allowed methods.
	/// </summary>
	public static class CryptoPayExtensions {
		/// <summary>
		///     Use this method to test your app's authentication token.
		/// </summary>
		/// <param name="crypto_pay_client_client">
		///     <see cref="CryptoPayClient" />
		/// </param>
		/// <param name="cancellation_token">
		///     A cancellation token that can be used by other objects or threads to receive notice of
		///     cancellation.
		/// </param>
		/// <returns>Returns basic information about the bot in form of a <see cref="CryptoPayApplication" /> object.</returns>
		public static async Task<CryptoPayApplication> GetMeAsync(
			this ICryptoPayClient crypto_pay_client_client,
			CancellationToken cancellation_token = default) {
			return await crypto_pay_client_client
						 .MakeRequestAsync(new GetMeRequest(), cancellation_token)
						 .ConfigureAwait(false);
		}

		/// <summary>
		///     Use this method to create a new invoice. On success, returns an object of the created <see cref="Invoice" />.
		/// </summary>
		/// <param name="crypto_pay_client_client">
		///     <see cref="CryptoPayClient" />
		/// </param>
		/// <param name="amount">Amount of the invoice in float. For example: 125.50.</param>
		/// <param name="currency_type">
		///     Optional. Type of the price, can be <see cref="CurrencyTypes.crypto" /> or
		///     <see cref="CurrencyTypes.fiat" />. Defaults to <see cref="CurrencyTypes.crypto" />.
		/// </param>
		/// <param name="asset">
		///     Optional. Required if currencyType is <see cref="CurrencyTypes.crypto" />. Cryptocurrency alphabetic code.
		///     <remarks>
		///         Due to the fact that the list of available currencies in the CryptoPay service is constantly changing,
		///         utilizing <see cref="Assets" /> becomes ineffective. However, you can resort to using Assets.BTC.ToString()
		///         instead.
		///     </remarks>
		/// </param>
		/// <param name="fiats">Optional. Required if currencyType is <see cref="CurrencyTypes.fiat" />. Fiat currency code.</param>
		/// <param name="accepted_assets">
		///     Optional. List of cryptocurrency alphabetic codes. Assets which can be used to pay the invoice.
		///     Available only if currencyType is <see cref="CurrencyTypes.fiat" />. Supported assets from <see cref="Assets" />.
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
		///     Optional. You can set a payment time limit for the invoice in seconds. Values between 1-2678400
		///     are accepted.
		/// </param>
		/// <param name="cancellation_token">
		///     A cancellation token that can be used by other objects or threads to receive notice of
		///     cancellation.
		/// </param>
		/// <returns>
		///     <see cref="Invoice" />
		/// </returns>
		/// <exception cref="RequestException">This exception can be thrown.</exception>
		public static async Task<Invoice> CreateInvoiceAsync(
			this ICryptoPayClient crypto_pay_client_client,
			double amount,
			CurrencyTypes currency_type = CurrencyTypes.crypto,
			string asset = default,
			string fiats = default,
			IEnumerable<string> accepted_assets = default,
			string description = default,
			string hidden_message = default,
			PaidButtonNames? paid_btn_name = default,
			string paid_btn_url = default,
			string payload = default,
			bool allow_comments = true,
			bool allow_anonymous = true,
			int expires_in = 2678400,
			CancellationToken cancellation_token = default) {
			return await crypto_pay_client_client
						 .MakeRequestAsync(
							 new CreateInvoiceRequest(
								 amount,
								 currency_type,
								 asset,
								 fiats,
								 accepted_assets,
								 description,
								 hidden_message,
								 paid_btn_name,
								 paid_btn_url,
								 payload,
								 allow_comments,
								 allow_anonymous,
								 expires_in
							 ),
							 cancellation_token
						 )
						 .ConfigureAwait(false);
		}

		/// <summary>
		///     Use this method to get a balance of your app. Returns array of <see cref="Balance" />.
		/// </summary>
		/// <param name="crypto_pay_client_client">
		///     <see cref="CryptoPayClient" />
		/// </param>
		/// <param name="cancellation_token">
		///     A cancellation token that can be used by other objects or threads to receive notice of
		///     cancellation.
		/// </param>
		/// <returns>List of <see cref="Balance" /></returns>
		/// <exception cref="RequestException">This exception can be thrown.</exception>
		public static async Task<List<Balance>> GetBalanceAsync(
			this ICryptoPayClient crypto_pay_client_client,
			CancellationToken cancellation_token = default) {
			return await crypto_pay_client_client
						 .MakeRequestAsync(new GetBalanceRequest(), cancellation_token)
						 .ConfigureAwait(false);
		}

		/// <summary>
		///     Use this method to get exchange rates of supported currencies. Returns array of <see cref="ExchangeRate" />>
		/// </summary>
		/// <param name="crypto_pay_client_client">
		///     <see cref="CryptoPayClient" />
		/// </param>
		/// <param name="cancellation_token">
		///     A cancellation token that can be used by other objects or threads to receive notice of
		///     cancellation.
		/// </param>
		/// <returns>List of <see cref="ExchangeRate" /></returns>
		/// <exception cref="RequestException">This exception can be thrown.</exception>
		public static async Task<List<ExchangeRate>> GetExchangeRatesAsync(
			this ICryptoPayClient crypto_pay_client_client,
			CancellationToken cancellation_token = default) {
			return await crypto_pay_client_client
						 .MakeRequestAsync(new GetExchangeRatesRequest(), cancellation_token)
						 .ConfigureAwait(false);
		}

		/// <summary>
		///     Use this method to get a list of supported currencies. Returns array of <see cref="Currency" />
		/// </summary>
		/// <param name="crypto_pay_client_client">
		///     <see cref="CryptoPayClient" />
		/// </param>
		/// <param name="cancellation_token">
		///     A cancellation token that can be used by other objects or threads to receive notice of
		///     cancellation.
		/// </param>
		/// <returns>List of <see cref="Currency" /></returns>
		/// <exception cref="RequestException">This exception can be thrown.</exception>
		public static async Task<List<Currency>> GetCurrenciesAsync(
			this ICryptoPayClient crypto_pay_client_client,
			CancellationToken cancellation_token = default) {
			return await crypto_pay_client_client
						 .MakeRequestAsync(new GetCurrenciesRequest(), cancellation_token)
						 .ConfigureAwait(false);
		}

		/// <summary>
		///     Use this method to send coins from your app's balance to a user. On success, returns object of completed
		///     <see cref="Transfer" />.
		/// </summary>
		/// <param name="crypto_pay_client_client">
		///     <see cref="CryptoPayClient" />
		/// </param>
		/// <param name="user_id">
		///     Telegram user ID. User must have previously used <c>@CryptoBot</c> (<c>@CryptoTestnetBot</c> for
		///     testnet).
		/// </param>
		/// <param name="asset">
		///     Currency code.
		///     <remarks>
		///         Due to the fact that the list of available currencies in the CryptoPay service is constantly changing,
		///         utilizing <see cref="Assets" /> becomes ineffective. However, you can resort to using Assets.BTC.ToString()
		///         instead.
		///     </remarks>
		/// </param>
		/// <param name="amount">Amount of the transfer in float. Values between $0.1-500 are accepted.</param>
		/// <param name="spend_id">
		///     Unique ID to make your request idempotent and ensure that only one of the transfers with the same <c>spendId</c>
		///     will be accepted by Crypto Pay API.
		///     This parameter is useful when the transfer should be retried (i.e. request timeout, connection reset, 500 HTTP
		///     status, etc).
		///     It can be some unique withdrawal identifier for example. Up to 64 symbols.
		/// </param>
		/// <param name="comment">
		///     Optional. Comment for the transfer. Users will see this comment when they receive a notification
		///     about the transfer. Up to 1024 symbols.
		/// </param>
		/// <param name="disable_send_notification">
		///     Optional. Pass true if the user should not receive a notification about the
		///     transfer. Default is <c>false</c>.
		/// </param>
		/// <param name="cancellation_token">
		///     A cancellation token that can be used by other objects or threads to receive notice of
		///     cancellation.
		/// </param>
		/// <returns>
		///     <see cref="Transfer" />Optional. Pass true if the user should not receive a notification about the transfer.
		///     Default is false.
		/// </returns>
		/// <exception cref="RequestException">This exception can be thrown.</exception>
		public static async Task<Transfer> TransferAsync(
			this ICryptoPayClient crypto_pay_client_client,
			long user_id,
			string asset,
			double amount,
			string spend_id,
			string comment = default,
			bool? disable_send_notification = default,
			CancellationToken cancellation_token = default) {
			return await crypto_pay_client_client
						 .MakeRequestAsync(
							 new TransferRequest(
								 user_id,
								 asset,
								 amount,
								 spend_id,
								 comment,
								 disable_send_notification
							 ),
							 cancellation_token
						 )
						 .ConfigureAwait(false);
		}

		/// <summary>
		///     Use this method to get transfers created by your app. On success, returns array of <see cref="Transfer" />.
		/// </summary>
		/// <param name="crypto_pay_client_client">
		///     <see cref="CryptoPayClient" />
		/// </param>
		/// <param name="asset">
		///     Optional. Cryptocurrency alphabetic code. Defaults to all currencies.
		///     <remarks>
		///         Due to the fact that the list of available currencies in the CryptoPay service is constantly changing,
		///         utilizing <see cref="Assets" /> becomes ineffective. However, you can resort to using Assets.BTC.ToString()
		///         instead.
		///     </remarks>
		/// </param>
		/// <param name="transfer_ids">Optional. List of transfer IDs.</param>
		/// <param name="offset">Optional. Offset needed to return a specific subset of transfers. Defaults to 0.</param>
		/// <param name="count">Optional. Number of transfers to be returned. Values between 1-1000 are accepted. Defaults to 100.</param>
		/// <param name="cancellation_token">
		///     A cancellation token that can be used by other objects or threads to receive notice of
		///     cancellation.
		/// </param>
		/// <returns>
		///     <see cref="Transfer" />Optional. Pass true if the user should not receive a notification about the transfer.
		///     Default is false.
		/// </returns>
		/// <exception cref="RequestException">This exception can be thrown.</exception>
		public static async Task<Transfers> GetTransfersAsync(
			this ICryptoPayClient crypto_pay_client_client,
			IEnumerable<string> asset = default,
			IEnumerable<string> transfer_ids = default,
			int offset = 0,
			int count = 100,
			CancellationToken cancellation_token = default) {
			return await crypto_pay_client_client
						 .MakeRequestAsync(
							 new GetTransfersRequest(
								 asset,
								 transfer_ids,
								 offset,
								 count
							 ),
							 cancellation_token
						 )
						 .ConfigureAwait(false);
		}


		/// <summary>
		///     Use this method to get invoices of your app. On success, returns array of <see cref="Invoice" />.
		/// </summary>
		/// <param name="crypto_pay_client_client">
		///     <see cref="CryptoPayClient" />
		/// </param>
		/// <param name="assets">
		///     Optional. List of assets. Supported assets: <see cref="Assets" />
		///     <remarks>
		///         Due to the fact that the list of available currencies in the CryptoPay service is constantly changing,
		///         utilizing <see cref="Assets" /> becomes ineffective. However, you can resort to using Assets.BTC.ToString()
		///         instead.
		///     </remarks>
		/// </param>
		/// <param name="invoice_ids">Optional. List of Invoice IDs.</param>
		/// <param name="status">
		///     Optional. Status of invoices to be returned. Available statuses: “active” and “paid”. Defaults to
		///     all statuses.
		/// </param>
		/// <param name="offset">Optional. Offset needed to return a specific subset of invoices. Default is 0.</param>
		/// <param name="count">Optional. Number of invoices to be returned. Values between 1-1000 are accepted. Defaults to 100.</param>
		/// <param name="cancellation_token">
		///     A cancellation token that can be used by other objects or threads to receive notice of
		///     cancellation.
		/// </param>
		/// <returns>
		///     <see cref="Invoice" />
		/// </returns>
		/// <exception cref="RequestException">This exception can be thrown.</exception>
		public static async Task<Invoices> GetInvoicesAsync(
			this ICryptoPayClient crypto_pay_client_client,
			IEnumerable<string> assets = default,
			IEnumerable<long> invoice_ids = default,
			Statuses? status = default,
			int offset = 0,
			int count = 100,
			CancellationToken cancellation_token = default) {
			return await crypto_pay_client_client
						 .MakeRequestAsync(
							 new GetInvoicesRequest(
								 assets,
								 invoice_ids,
								 status,
								 offset,
								 count
							 ),
							 cancellation_token
						 )
						 .ConfigureAwait(false);
		}

		/// <summary>
		/// </summary>
		/// <param name="crypto_pay_client_client">
		///     <see cref="CryptoPayClient" />
		/// </param>
		/// <param name="invoice_id"></param>
		/// <param name="cancellation_token">
		///     A cancellation token that can be used by other objects or threads to receive notice of
		///     cancellation.
		/// </param>
		/// <returns>
		///     <see cref="Invoice" />
		/// </returns>
		/// <exception cref="RequestException">This exception can be thrown.</exception>
		public static async Task<bool> DeleteInvoiceAsync(
			this ICryptoPayClient crypto_pay_client_client,
			long invoice_id,
			CancellationToken cancellation_token = default) {
			return await crypto_pay_client_client
						 .MakeRequestAsync(
							 new DeleteInvoiceRequest(invoice_id),
							 cancellation_token
						 )
						 .ConfigureAwait(false);
		}

		/// <summary>
		///     Use this method to create a new <see cref="Check" />.
		/// </summary>
		/// <param name="crypto_pay_client_client">
		///     <see cref="CryptoPayClient" />
		/// </param>
		/// <param name="asset">
		///     Cryptocurrency alphabetic code.
		///     <remarks>
		///         Due to the fact that the list of available currencies in the CryptoPay service is constantly changing,
		///         utilizing <see cref="Assets" /> becomes ineffective. However, you can resort to using Assets.BTC.ToString()
		///         instead.
		///     </remarks>
		/// </param>
		/// <param name="amount">Amount of the invoice in float. For example: 125.50</param>
		/// <param name="cancellation_token">
		///     A cancellation token that can be used by other objects or threads to receive notice of
		///     cancellation.
		/// </param>
		/// <returns><see cref="Check" />On success, returns an <see cref="Check" /> of the created.</returns>
		/// <exception cref="RequestException">This exception can be thrown.</exception>
		public static async Task<Check> CreateCheckAsync(
			this ICryptoPayClient crypto_pay_client_client,
			string asset,
			double amount,
			CancellationToken cancellation_token = default) {
			return await crypto_pay_client_client
						 .MakeRequestAsync(
							 new CreateCheckRequest(asset, amount),
							 cancellation_token
						 )
						 .ConfigureAwait(false);
		}

		/// <summary>
		///     Use this method to delete checks created by your app.
		/// </summary>
		/// <param name="crypto_pay_client_client">
		///     <see cref="CryptoPayClient" />
		/// </param>
		/// <param name="check_id"></param>
		/// <param name="cancellation_token">
		///     A cancellation token that can be used by other objects or threads to receive notice of
		///     cancellation.
		/// </param>
		/// <returns>Returns True on success.</returns>
		/// <exception cref="RequestException">This exception can be thrown.</exception>
		public static async Task<bool> DeleteCheckAsync(
			this ICryptoPayClient crypto_pay_client_client,
			long check_id,
			CancellationToken cancellation_token = default) {
			return await crypto_pay_client_client
						 .MakeRequestAsync(
							 new DeleteCheckRequest(check_id),
							 cancellation_token
						 )
						 .ConfigureAwait(false);
		}

		/// <summary>
		///     Use this method to get checks created by your app.
		/// </summary>
		/// <param name="crypto_pay_client_client">
		///     <see cref="CryptoPayClient" />
		/// </param>
		/// <param name="assets">
		///     Optional. Cryptocurrency alphabetic code. Supported crypto assets from <see cref="Assets" />.Defaults to all
		///     currencies.
		///     <remarks>
		///         Due to the fact that the list of available currencies in the CryptoPay service is constantly changing,
		///         utilizing <see cref="Assets" /> becomes ineffective. However, you can resort to using Assets.BTC.ToString()
		///         instead.
		///     </remarks>
		/// </param>
		/// <param name="check_ids">Optional. List of check IDs.</param>
		/// <param name="status">
		///     Optional. Status of check to be returned. Available statuses: <see cref="CheckStatus" />. Defaults
		///     to all statuses.
		/// </param>
		/// <param name="offset">Optional. Offset needed to return a specific subset of check. Defaults to 0.</param>
		/// <param name="count">Optional. Number of check to be returned. Values between 1-1000 are accepted. Defaults to 100.</param>
		/// <param name="cancellation_token">
		///     A cancellation token that can be used by other objects or threads to receive notice of
		///     cancellation.
		/// </param>
		/// <returns><see cref="Check" />On success, returns array of <see cref="Check" /></returns>
		/// <exception cref="RequestException">This exception can be thrown.</exception>
		public static async Task<Checks> GetChecksAsync(
			this ICryptoPayClient crypto_pay_client_client,
			IEnumerable<string> assets = default,
			IEnumerable<long> check_ids = default,
			IEnumerable<Statuses> status = default,
			int offset = 0,
			int count = 100,
			CancellationToken cancellation_token = default) {
			return await crypto_pay_client_client
						 .MakeRequestAsync(
							 new GetChecksRequest(
								 assets,
								 check_ids,
								 status,
								 offset,
								 count
							 ),
							 cancellation_token
						 )
						 .ConfigureAwait(false);
		}
	}
}