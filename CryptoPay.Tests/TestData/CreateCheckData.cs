using System.Net;

using CryptoPay.Requests;
using CryptoPay.Types;

using Xunit;

namespace CryptoPay.Tests.TestData {
	/// <summary>
	///     For this test, you must have test coins.
	/// </summary>
	public sealed class CreateCheckData : TheoryData<HttpStatusCode, Error?, CreateCheckRequest> {
		public CreateCheckData() {
			Add(
				default,
				default,
				new(
					Assets.BNB.ToString(),
					0.0123
				)
			);
			Add(
				HttpStatusCode.BadRequest,
				new(400, "NOT_ENOUGH_COINS"),
				new(
					Assets.TON.ToString(),
					100.2345
				)
			);
			Add(
				HttpStatusCode.BadRequest,
				new(400, "ASSET_INVALID"),
				new(
					"FFF",
					0.0123
				)
			);
		}
	}
}