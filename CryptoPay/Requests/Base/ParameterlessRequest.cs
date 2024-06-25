using System.Net.Http;

using CryptoPay.Types.Update;

namespace CryptoPay.Requests.Base {
	/// <summary>
	///     Represents a request that doesn't require any parameters.
	/// </summary>
	/// <typeparam name="TResult">Type of response. For example <see cref="Invoice" /></typeparam>
	public class ParameterlessRequest<TResult> : RequestBase<TResult> {
		#region Constructors

		/// <summary>
		///     Initializes an instance of <see cref="ParameterlessRequest{TResult}" />
		/// </summary>
		/// <param name="method_name">Name of request method.</param>
		protected ParameterlessRequest(string method_name)
				: base(method_name) {
		}

		/// <summary>
		///     Initializes an instance of <see cref="ParameterlessRequest{TResult}" />
		/// </summary>
		/// <param name="method_name">Name of request method.</param>
		/// <param name="method">HTTP request method.</param>
		protected ParameterlessRequest(string method_name, HttpMethod method)
				: base(method_name, method) {
		}

		#endregion
	}
}