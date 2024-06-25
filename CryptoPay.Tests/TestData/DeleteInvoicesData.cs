using System.Net;

using CryptoPay.Requests;
using CryptoPay.Types;

using Xunit;

namespace CryptoPay.Tests.TestData {
	public sealed class DeleteInvoicesData : TheoryData<HttpStatusCode, Error?, CreateInvoiceRequest> {
		public DeleteInvoicesData() {
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
		}
	}
}