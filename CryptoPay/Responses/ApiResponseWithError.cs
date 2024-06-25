using System.Text.Json.Serialization;

using CryptoPay.Types;

namespace CryptoPay.Responses {
	/// <summary>
	///     Response with <see cref="Error" />.
	/// </summary>
	internal sealed class ApiResponseWithError : IResponse {
		/// <summary>
		///     Initializes an instance of <see cref="ApiResponseWithError" />
		/// </summary>
		/// <param name="error">Instanse of <see cref="Error" />.</param>
		public ApiResponseWithError(Error error) {
			Ok = false;
			Error = error;
		}

		[JsonConstructor]
		private ApiResponseWithError() {
		}

		/// <summary>
		///     Instance of <see cref="Error" />.
		/// </summary>
		[JsonRequired]
		public Error Error { get; init; }

		/// <inheritdoc cref="IResponse.Ok" />
		[JsonRequired]
		public bool Ok { get; init; }
	}
}