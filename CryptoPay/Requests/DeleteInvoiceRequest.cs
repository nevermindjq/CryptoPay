﻿using System.Text.Json.Serialization;

using CryptoPay.Requests.Base;

namespace CryptoPay.Requests {
	/// <summary>
	///     Use this method to delete invoices created by your app.
	/// </summary>
	public sealed class DeleteInvoiceRequest
			: ParameterlessRequest<bool> {
		#region Constructors

		/// <summary>
		///     Initializes a new request delete invoices created by your app.
		/// </summary>
		/// <param name="invoice_id">Invoice ID to be deleted.</param>
		public DeleteInvoiceRequest(long invoice_id)
				: base("deleteInvoice") {
			InvoiceId = invoice_id;
		}

		#endregion

		#region Public Fields

		/// <summary>
		///     Unique ID for this invoice.
		/// </summary>
		[JsonRequired]
		public long InvoiceId { get; set; }

		#endregion
	}
}