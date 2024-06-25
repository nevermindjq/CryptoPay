using System;
using System.Net;
using System.Runtime.CompilerServices;

using CryptoPay.Types;

namespace CryptoPay.Extensions {
	/// <summary>
	///     Exception included <see cref="Error" />
	/// </summary>
	public sealed class RequestException : Exception {
		/// <summary>
		///     Initializes a new instance of the <see cref="RequestException" /> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public RequestException(string message)
				: base(message) {
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="RequestException" /> class.
		/// </summary>
		/// <param name="message">
		///     The error message that explains the reason for the exception.
		/// </param>
		/// <param name="error"></param>
		/// <param name="http_status_code">
		///     <see cref="HttpStatusCode" /> of the received response.
		/// </param>
		public RequestException(string message, Error error, HttpStatusCode http_status_code)
				: base(RequestException.PrepareErrorMessage(message, error)) {
			Error = error;
			HttpStatusCode = http_status_code;
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="RequestException" /> class.
		/// </summary>
		/// <param name="message">
		///     The error message that explains the reason for the exception.
		/// </param>
		/// <param name="http_status_code">
		///     <see cref="HttpStatusCode" /> of the received response.
		/// </param>
		/// <param name="inner_exception">
		///     The exception that is the cause of the current exception, or a null reference
		///     (Nothing in Visual Basic) if no inner exception is specified.
		/// </param>
		public RequestException(string message, HttpStatusCode http_status_code, Exception inner_exception)
				: base(message, inner_exception) {
			HttpStatusCode = http_status_code;
		}

		/// <summary>
		///     <see cref="HttpStatusCode" /> of the received response.
		/// </summary>
		public HttpStatusCode? HttpStatusCode { get; }

		/// <summary>
		///     Error from response.
		/// </summary>
		public Error Error { get; }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static string PrepareErrorMessage(string message, Error error) {
			if (error is null) {
				return message;
			}

			var error_message = $"Code: {error.Code} Name: {error.Name}";
			return message is null ? error_message : $"{message}{Environment.NewLine}{error_message}";
		}
	}
}