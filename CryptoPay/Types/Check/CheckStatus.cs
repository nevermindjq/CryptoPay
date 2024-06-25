﻿using System.Text.Json.Serialization;

namespace CryptoPay.Types.Check {
#pragma warning disable CS1591

	/// <summary>
	///     Status of the check.
	/// </summary>
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum CheckStatus {
		Active,
		Activated
	}
}