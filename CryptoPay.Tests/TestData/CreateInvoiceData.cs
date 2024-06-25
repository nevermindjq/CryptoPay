using System.Net;

using CryptoPay.Requests;
using CryptoPay.Types;

using Xunit;

namespace CryptoPay.Tests.TestData {
	public class CreateInvoiceData : TheoryData<HttpStatusCode, Error?, CreateInvoiceRequest> {
		public CreateInvoiceData() {
			Add(
				default,
				default,
				new(
					5.105,
					asset: Assets.TON.ToString()
				)
			);
			Add(
				default,
				default,
				new(
					1.105,
					CurrencyTypes.Fiat,
					fiat: Assets.USD.ToString()
				)
			);
			Add(
				default,
				default,
				new(
					5.105,
					asset: Assets.TON.ToString(),
					description: "description",
					hidden_message: "hiddenMessage",
					paid_btn_name: PaidButtonNames.callback,
					paid_btn_url: "https://t.me/paidBtnUrl",
					payload: "payload",
					allow_comments: false,
					allow_anonymous: false,
					expires_in: 1800
				)
			);
			Add(
				default,
				default,
				new(
					2.35,
					CurrencyTypes.Fiat,
					default,
					Assets.EUR.ToString(),
					default,
					"description",
					"hiddenMessage",
					PaidButtonNames.callback,
					"https://t.me/paidBtnUrl",
					"payload",
					true,
					false,
					360
				)
			);

			Add(
				default,
				default,
				new(
					0.0234,
					CurrencyTypes.Crypto,
					Assets.BNB.ToString(),
					default,
					default,
					"description",
					"hiddenMessage",
					PaidButtonNames.callback,
					"https://t.me/paidBtnUrl",
					"payload",
					true,
					false,
					360
				)
			);

			Add(
				HttpStatusCode.BadRequest,
				new(400, "PAID_BTN_URL_REQUIRED"),
				new(
					0.105,
					asset: Assets.TON.ToString(),
					paid_btn_name: PaidButtonNames.callback
				)
			);
			Add(
				HttpStatusCode.BadRequest,
				new(400, "UNSUPPORTED_ASSET"),
				new(
					0.123,
					asset: "FFF",
					paid_btn_name: PaidButtonNames.callback
				)
			);
		}
	}
}