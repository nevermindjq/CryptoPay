using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using CryptoPay.Exceptions;
using CryptoPay.Requests;
using CryptoPay.Tests.TestData;
using CryptoPay.Types;

using Xunit;

#pragma warning disable CS0618 // Type or member is obsolete

namespace CryptoPay.Tests {
	public class AvailableMethodsTests {
		#region Private Methods

		private static void AssertException(RequestException request_exception, HttpStatusCode status_code, Error error) {
			Assert.NotNull(request_exception);
			Assert.Equal(status_code, request_exception.HttpStatusCode);

			var error_exception = request_exception.Error;
			Assert.NotNull(error_exception);
			Assert.Equal(error.Code, error_exception.Code);
			Assert.Equal(error.Name, error_exception.Name);
		}

		#endregion

		#region Public Fields

		private readonly CancellationToken _cancellation_token = new CancellationTokenSource().Token;

		private readonly ICryptoPayClient _crypto_pay_client = new CryptoPayClient(
			CryptoPayTestHelper.token,
			api_url: CryptoPayTestHelper.api_url
		);

		#endregion

		/// Enter your own actual values in
		/// <see cref="CryptoPayTestHelper.token" />
		/// <see cref="CryptoPayTestHelper.api_url" />
		/// <see cref="CryptoPayTestHelper.user_id" />
		/// For test
		/// <see cref="TransferTest" />
		/// , you must have test
		/// <see cref="Assets.TON" />
		/// > coins in you application wallet.

		#region Tests

		[Theory, ClassData(typeof(CryptoPayClientData))]
		public async Task AuthorizationAndGetMeTest(
			HttpStatusCode status_code,
			Error error,
			string token,
			string api_url) {
			try {
				var local_crypto_pay_client = new CryptoPayClient(token, api_url: api_url);
				var application = await local_crypto_pay_client.GetMeAsync(_cancellation_token);

				Assert.NotNull(application);
				Assert.NotEmpty(application.Name);
				Assert.NotEmpty(application.PaymentProcessingBotUsername);
				Assert.Equal(local_crypto_pay_client.AppId, application.AppId);
			} catch (ArgumentNullException argument_null_exception) {
				Assert.NotNull(argument_null_exception);
			} catch (RequestException request_exception) {
				AvailableMethodsTests.AssertException(request_exception, status_code, error);
			}
		}

		[Theory, ClassData(typeof(CreateInvoiceData))]
		public async Task CreateInvoiceTest(HttpStatusCode status_code, Error error, CreateInvoiceRequest invoice_request) {
			try {
				var invoice = await _crypto_pay_client.CreateInvoiceAsync(
					invoice_request.Amount,
					invoice_request.CurrencyType,
					invoice_request.Asset,
					invoice_request.Fiat,
					invoice_request.AcceptedAssets,
					invoice_request.Description,
					invoice_request.HiddenMessage,
					invoice_request.PaidBtnName,
					invoice_request.PaidBtnUrl,
					invoice_request.Payload,
					invoice_request.AllowComments!.Value,
					invoice_request.AllowAnonymous!.Value,
					invoice_request.ExpiresIn,
					_cancellation_token
				);

				Assert.NotNull(invoice);
				Assert.NotNull(invoice.PayUrl);
				Assert.NotNull(invoice.Hash);
				Assert.Equal(invoice_request.Amount, invoice.Amount);
				Assert.Equal(invoice_request.CurrencyType, invoice.CurrencyType);
				Assert.Equal(invoice_request.Asset, invoice.Asset);
				Assert.Equal(AssetsHelper.TryParse(invoice_request.Asset), AssetsHelper.TryParse(invoice.Asset));
				Assert.Equal(invoice_request.Fiat, invoice.Fiat);
				// Assert.Equal(invoiceRequest.AcceptedAssets, invoice.AcceptedAssets);
				Assert.Equal(invoice_request.Description, invoice.Description);
				Assert.Equal(invoice_request.HiddenMessage, invoice.HiddenMessage);
				Assert.Equal(invoice_request.PaidBtnName, invoice.PaidBtnName);
				Assert.Equal(invoice_request.PaidBtnUrl, invoice.PaidBtnUrl);
				Assert.Equal(invoice_request.Payload, invoice.Payload);
				Assert.Equal(invoice_request.AllowComments!.Value, invoice.AllowComments);
				Assert.Equal(invoice_request.AllowAnonymous!.Value, invoice.AllowAnonymous);
				Assert.Equal(invoice.CreatedAt.AddSeconds(invoice_request.ExpiresIn).ToString("g"), invoice.ExpirationDate?.ToString("g"));
			} catch (RequestException request_exception) {
				AvailableMethodsTests.AssertException(request_exception, status_code, error);
			}
		}

		[Fact]
		public async Task GetBalanceTest() {
			var balance = await _crypto_pay_client.GetBalanceAsync(_cancellation_token);

			Assert.NotNull(balance);
			Assert.True(balance.Any());
		}

		[Fact]
		public async Task GetExchangeRatesTest() {
			var exchange_rates = await _crypto_pay_client.GetExchangeRatesAsync(_cancellation_token);

			Assert.NotNull(exchange_rates);
			Assert.True(exchange_rates.Any());
		}

		[Fact]
		public async Task GetCurrenciesTest() {
			var currencies = await _crypto_pay_client.GetCurrenciesAsync(_cancellation_token);

			Assert.NotNull(currencies);
			Assert.True(currencies.Any());
		}

		/// <summary>
		///     For this test, you must have test coins.
		/// </summary>
		[Theory, ClassData(typeof(TransferData))]
		public async Task TransferTest(HttpStatusCode status_code, Error error, TransferRequest transfer_request) {
			try {
				var transfer = await _crypto_pay_client.TransferAsync(
					transfer_request.UserId,
					transfer_request.Asset,
					transfer_request.Amount,
					transfer_request.SpendId,
					transfer_request.Comment,
					transfer_request.DisableSendNotification,
					_cancellation_token
				);

				Assert.NotNull(transfer);
				Assert.Equal(transfer_request.UserId, transfer.UserId);
				Assert.Equal(transfer_request.Asset, transfer.Asset);
				Assert.Equal(AssetsHelper.TryParse(transfer_request.Asset), AssetsHelper.TryParse(transfer_request.Asset));
				Assert.Equal(transfer_request.Amount, transfer.Amount);
				//Assert.Equal(transferRequest.Comment, transfer.Comment);
				Assert.Equal(transfer_request.DisableSendNotification, transfer_request.DisableSendNotification);
			} catch (RequestException request_exception) {
				AvailableMethodsTests.AssertException(request_exception, status_code, error);
			}
		}

		/// <summary>
		///     For this test, you must have test coins.
		/// </summary>
		[Theory, ClassData(typeof(GetTransfersData))]
		public async Task GetTransfersTest(HttpStatusCode status_code, Error error, TransferRequest transfer_request) {
			try {
				var transfer = await _crypto_pay_client.TransferAsync(
					transfer_request.UserId,
					transfer_request.Asset,
					transfer_request.Amount,
					transfer_request.SpendId,
					transfer_request.Comment,
					transfer_request.DisableSendNotification,
					_cancellation_token
				);

				var transfers = await _crypto_pay_client.GetTransfersAsync(cancellation_token: _cancellation_token);

				Assert.NotNull(transfer);

				Assert.NotNull(transfers);
				Assert.True(transfers.Items.Any());
			} catch (RequestException request_exception) {
				AvailableMethodsTests.AssertException(request_exception, status_code, error);
			}
		}

		[Theory, ClassData(typeof(GetInvoicesData))]
		public async Task GetInvoicesTest(
			HttpStatusCode status_code,
			Error error,
			IList<string> assets,
			IList<long> invoice_ids,
			Statuses? status,
			int offset,
			int count) {
			try {
				var invoices = await _crypto_pay_client.GetInvoicesAsync(
					assets,
					invoice_ids,
					status,
					offset,
					count,
					_cancellation_token
				);

				Assert.NotNull(invoices);
			} catch (RequestException request_exception) {
				AvailableMethodsTests.AssertException(request_exception, status_code, error);
			}
		}

		[Theory, ClassData(typeof(DeleteInvoicesData))]
		public async Task DeleteInvoiceTest(HttpStatusCode status_code, Error error, CreateInvoiceRequest invoice_request) {
			try {
				var invoice = await _crypto_pay_client.CreateInvoiceAsync(
					invoice_request.Amount,
					invoice_request.CurrencyType,
					invoice_request.Asset,
					invoice_request.Fiat,
					invoice_request.AcceptedAssets,
					invoice_request.Description,
					invoice_request.HiddenMessage,
					invoice_request.PaidBtnName,
					invoice_request.PaidBtnUrl,
					invoice_request.Payload,
					invoice_request.AllowComments!.Value,
					invoice_request.AllowAnonymous!.Value,
					invoice_request.ExpiresIn,
					_cancellation_token
				);

				var deleted = await _crypto_pay_client.DeleteInvoiceAsync(invoice.InvoiceId, _cancellation_token);

				Assert.NotNull(invoice);
				Assert.True(deleted);
			} catch (RequestException request_exception) {
				AvailableMethodsTests.AssertException(request_exception, status_code, error);
			}
		}

		[Theory, ClassData(typeof(CreateCheckData))]
		public async Task CreateCheckTest(HttpStatusCode status_code, Error error, CreateCheckRequest create_check_request) {
			try {
				var check = await _crypto_pay_client.CreateCheckAsync(
					create_check_request.Asset,
					create_check_request.Amount,
					_cancellation_token
				);

				Assert.NotNull(check);
				Assert.Equal(create_check_request.Asset, check.Asset);
				Assert.Equal(AssetsHelper.TryParse(create_check_request.Asset), AssetsHelper.TryParse(check.Asset));
				Assert.Equal(create_check_request.Amount, check.Amount);
			} catch (RequestException request_exception) {
				AvailableMethodsTests.AssertException(request_exception, status_code, error);
			}
		}

		[Theory, ClassData(typeof(CreateCheckData))]
		public async Task DeleteCheckTest(HttpStatusCode status_code, Error error, CreateCheckRequest create_check_request) {
			try {
				var check = await _crypto_pay_client.CreateCheckAsync(
					create_check_request.Asset,
					create_check_request.Amount,
					_cancellation_token
				);

				var deleted = await _crypto_pay_client.DeleteCheckAsync(check.CheckId, _cancellation_token);

				Assert.True(deleted);
			} catch (RequestException request_exception) {
				AvailableMethodsTests.AssertException(request_exception, status_code, error);
			}
		}

		[Theory, ClassData(typeof(CreateCheckData))]
		public async Task GetCheckTest(HttpStatusCode status_code, Error error, CreateCheckRequest create_check_request) {
			try {
				var check = await _crypto_pay_client.CreateCheckAsync(
					create_check_request.Asset,
					create_check_request.Amount,
					_cancellation_token
				);

				var checks = await _crypto_pay_client.GetChecksAsync(cancellation_token: _cancellation_token);

				Assert.NotNull(check);
				Assert.NotNull(checks);
				Assert.True(checks.Items.Any());
			} catch (RequestException request_exception) {
				AvailableMethodsTests.AssertException(request_exception, status_code, error);
			}
		}

		#endregion
	}
}