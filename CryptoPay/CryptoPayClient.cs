using System;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

using CryptoPay.Extensions;
using CryptoPay.Requests.Base;
using CryptoPay.Responses;

namespace CryptoPay {
	/// <inheritdoc />
	public sealed class CryptoPayClient : ICryptoPayClient {
		#region Constructors

		/// <summary>
		///     Create <see cref="ICryptoPayClient" /> instance.
		/// </summary>
		/// <param name="token">Your application token from CryptoPay.</param>
		/// <param name="http_client">Optional. <see cref="HttpClient" />.</param>
		/// <param name="api_url">
		///     Optional. Default value is <see cref="DefaultCryptoBotApiUrl" /> main api url.
		///     Test api url is <code>https://testnet-pay.crypt.bot/</code>.
		/// </param>
		/// <exception cref="ArgumentNullException">If token is null.</exception>
		public CryptoPayClient(
			string token,
			HttpClient http_client = null,
			string api_url = default) {
			_token = string.IsNullOrEmpty(token) ? throw new ArgumentNullException(nameof(token)) : token;

			_http_client = http_client ?? new HttpClient();
			_crypto_bot_api_url = api_url ?? CryptoPayClient.DefaultCryptoBotApiUrl;
			AppId = CryptoPayClient.GetApplicationId(_token);
		}

		#endregion

		#region Public Methods

		/// <inheritdoc />
		public async Task<TResponse> MakeRequestAsync<TResponse>(
			IRequest<TResponse> request,
			CancellationToken cancellation_token = default) {
			if (request is null) {
				throw new ArgumentNullException(nameof(request));
			}

			var url = $"{_crypto_bot_api_url}api/{request.MethodName}";

			using var http_request = new HttpRequestMessage(request.Method, url) {
				Content = request.ToHttpContent()
			};

			http_request.Headers.Add("Crypto-Pay-API-Token", _token);

			using var http_response = await SendRequestAsync(
						_http_client,
						http_request,
						cancellation_token
					)
					.ConfigureAwait(false);

			if (http_response.StatusCode != HttpStatusCode.OK) {
				await http_response
					  .DeserializeContentAsync<ApiResponseWithError>(
						  response =>
								  response.Ok == false,
						  cancellation_token
					  )
					  .ConfigureAwait(false);
			}

			var api_response = await http_response
									 .DeserializeContentAsync<ApiResponse<TResponse>>(
										 response =>
												 response.Ok == false ||
												 response.Result is null,
										 cancellation_token
									 )
									 .ConfigureAwait(false);

			return api_response.Result!;

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			static async Task<HttpResponseMessage> SendRequestAsync(
				HttpClient http_client,
				HttpRequestMessage http_request,
				CancellationToken cancellation_token) {
				HttpResponseMessage http_response;
				try {
					http_response = await http_client
										  .SendAsync(http_request, cancellation_token)
										  .ConfigureAwait(false);
				} catch (TaskCanceledException exception) {
					if (cancellation_token.IsCancellationRequested) {
						throw;
					}

					throw new("Request timed out", exception);
				} catch (Exception exception) {
					throw new(
						"Exception during making request",
						exception
					);
				}

				return http_response;
			}
		}

		#endregion

		#region Private Methods

		private static long GetApplicationId(string token) {
			ReadOnlySpan<char> data_as_span = token;
			var end_ind = token.IndexOf(":", StringComparison.Ordinal);
			return long.Parse(data_as_span.Slice(0, end_ind));
		}

		#endregion

		#region Public Fields

		/// <summary>
		///     Crypto Bot Api Url.
		/// </summary>
		private static string DefaultCryptoBotApiUrl { get; } = "https://pay.crypt.bot/";

		/// <inheritdoc />
		public long AppId { get; init; }

		#endregion

		#region Private Fields

		private readonly HttpClient _http_client;
		private readonly string _crypto_bot_api_url;
		private readonly string _token;

		#endregion
	}
}