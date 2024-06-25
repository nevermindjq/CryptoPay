using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CryptoPay.Requests.Base {
	/// <inheritdoc />
	public class RequestBase<TResponse> : IRequest<TResponse> {
		#region Public Methods

		/// <inheritdoc />
		public HttpContent ToHttpContent() {
			var options = new JsonSerializerOptions {
				PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
				DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
				NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
			};

			var payload = JsonSerializer.Serialize(this, GetType(), options);
			return new StringContent(payload, Encoding.UTF8, "application/json");
		}

		#endregion

		#region Constructors

		/// <summary>
		///     Initializes an instance of request.
		/// </summary>
		/// <param name="method_name">Bot API method</param>
		protected RequestBase(string method_name)
				: this(method_name, HttpMethod.Post) {
		}

		/// <summary>
		///     Initializes an instance of request.
		/// </summary>
		/// <param name="method_name">Bot API method.</param>
		/// <param name="method">HTTP method to use.</param>
		protected RequestBase(string method_name, HttpMethod method) {
			MethodName = method_name;
			Method = method;
		}

		#endregion

		#region Public Fields

		/// <inheritdoc />
		public HttpMethod Method { get; }

		/// <inheritdoc />
		public string MethodName { get; }

		#endregion
	}
}