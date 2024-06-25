using System.Net;

using CryptoPay.Types;

using Xunit;

namespace CryptoPay.Tests.TestData {
	public class CryptoPayClientData : TheoryData<HttpStatusCode?, Error?, string, string> {
		public CryptoPayClientData() {
			Add(default, default, string.Empty, string.Empty);
			Add(default, default, CryptoPayTestHelper.token, CryptoPayTestHelper.api_url);
			Add(
				HttpStatusCode.Unauthorized,
				new(401, "UNAUTHORIZED"),
				CryptoPayTestHelper.token + "abc",
				CryptoPayTestHelper.api_url
			);
			Add(
				HttpStatusCode.MethodNotAllowed,
				new(405, "METHOD_NOT_FOUND"),
				CryptoPayTestHelper.token,
				CryptoPayTestHelper.api_url + "abc"
			);
		}
	}
}