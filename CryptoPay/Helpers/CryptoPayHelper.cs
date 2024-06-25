﻿using System;
using System.Security.Cryptography;
using System.Text;

using CryptoPay.Types;

// ReSharper disable once CheckNamespace
namespace CryptoPay {
	/// <summary>
	///     Helper for main <see cref="CryptoPayClient" />.
	/// </summary>
	public static class CryptoPayHelper {
		/// <summary>
		///     This method verify the integrity of the received data.
		/// </summary>
		/// <param name="signature">Sting from header parameter <c>crypto-pay-api-signature</c>.</param>
		/// <param name="token">Your application token from CryptoPay.</param>
		/// <param name="body">Response <see cref="Update">body</see>.</param>
		/// <returns><c>true</c> if the header parameter crypto-pay-api-signature equals hash of request body.</returns>
		public static bool CheckSignature(string signature, string token, Update body) {
			using (var sha256_hash = SHA256.Create()) {
				var secret = sha256_hash.ComputeHash(Encoding.UTF8.GetBytes(token));

				using (var hmac = new HMACSHA256(secret)) {
					var check_string = Encoding.UTF8.GetBytes(body.ToString());

					return Convert.ToHexString(hmac.ComputeHash(check_string)).ToLower() == signature;
				}
			}
		}
	}
}