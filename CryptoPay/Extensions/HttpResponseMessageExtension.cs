using System;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

using CryptoPay.Exceptions;
using CryptoPay.Responses;
using CryptoPay.Types;

namespace CryptoPay.Extensions {
	/// <summary>
	///     HttpResponseMessage extension class.
	/// </summary>
	public static class HttpResponseMessageExtensions {
		private static async Task<T> DeserializeJsonFromStreamAsync<T>(this Stream stream, CancellationToken cancellation_token)
				where T : class {
			if (stream is null || !stream.CanRead) {
				return default;
			}

			var options = new JsonSerializerOptions {
				PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
				DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
				NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
			};

			return await JsonSerializer.DeserializeAsync<T>(stream, options, cancellation_token);
		}

		/// <summary>
		///     Deserialize body from HttpContent into <typeparamref name="T" />.
		/// </summary>
		/// <param name="http_response"><see cref="HttpResponseMessage" /> instance.</param>
		/// <param name="guard"></param>
		/// <param name="cancellation_token"></param>
		/// <typeparam name="T">Type of the resulting object.</typeparam>
		/// <returns></returns>
		/// <exception cref="RequestException">
		///     Thrown when body in the response can not be deserialized into <typeparamref name="T" />.
		/// </exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static async Task<T> DeserializeContentAsync<T>(
			this HttpResponseMessage http_response,
			Func<T, bool> guard,
			CancellationToken cancellation_token)
				where T : class {
			Stream content_stream = null;

			if (http_response.Content is null) {
				throw new RequestException(
					"Response doesn't contain any content",
					null,
					http_response.StatusCode
				);
			}

			try {
				T deserialized_object;
				try {
					content_stream = await http_response.Content
														.ReadAsStreamAsync(cancellation_token)
														.ConfigureAwait(false);

					deserialized_object = await content_stream
							.DeserializeJsonFromStreamAsync<T>(cancellation_token);
				} catch (Exception exception) {
					throw HttpResponseMessageExtensions.CreateRequestException(
						http_response,
						exception: exception
					);
				}

				if (deserialized_object is null) {
					throw HttpResponseMessageExtensions.CreateRequestException(
						http_response,
						message: "Required properties not found in response"
					);
				}

				if (guard(deserialized_object)) {
					throw HttpResponseMessageExtensions.CreateRequestException(
						http_response,
						(deserialized_object as ApiResponseWithError)?.Error
					);
				}

				return deserialized_object;
			}
			finally {
				if (content_stream is { }) {
					await content_stream
						  .DisposeAsync()
						  .ConfigureAwait(false);
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static RequestException CreateRequestException(
			HttpResponseMessage http_response,
			Error error = default,
			string message = default,
			Exception exception = default
		) {
			return exception is null ?
					new(
						message,
						error,
						http_response.StatusCode
					) :
					new RequestException(
						exception.Message,
						http_response.StatusCode,
						exception
					);
		}
	}
}