using System;
using System.Net;

using CryptoPay.Requests;
using CryptoPay.Types;

using Xunit;

namespace CryptoPay.Tests.TestData {
	/// <summary>
	///     For this test, you must have test coins.
	/// </summary>
	public class TransferData : TheoryData<HttpStatusCode, Error?, TransferRequest> {
		public TransferData() {
			Add(
				default,
				default,
				new(
					CryptoPayTestHelper.user_id,
					Assets.TON.ToString(),
					0.5,
					Guid.NewGuid().ToString(),
					disable_send_notification: true
				)
			);

			Add(
				default,
				default,
				new(
					CryptoPayTestHelper.user_id,
					Assets.TON.ToString(),
					0.5,
					Guid.NewGuid().ToString(),
					Guid.NewGuid().ToString(),
					false
				)
			);

			Add(
				default,
				default,
				new(
					CryptoPayTestHelper.user_id,
					Assets.BNB.ToString(),
					0.0123,
					Guid.NewGuid().ToString(),
					disable_send_notification: false
				)
			);

			Add(
				HttpStatusCode.BadRequest,
				new(400, "AMOUNT_TOO_BIG"),
				new(
					CryptoPayTestHelper.user_id,
					Assets.BTC.ToString(),
					1000,
					Guid.NewGuid().ToString()
				)
			);
			Add(
				HttpStatusCode.InternalServerError,
				new(500, "APP_ERROR"),
				new(
					CryptoPayTestHelper.user_id,
					Assets.Unknown.ToString(),
					10,
					Guid.NewGuid().ToString()
				)
			);
		}
	}
}