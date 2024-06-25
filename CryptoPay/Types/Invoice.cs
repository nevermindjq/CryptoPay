using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CryptoPay.Types;

/// <inheritdoc />
public sealed class Invoice : IInvoice
{
    /// <summary>
    /// Unique ID for this invoice.
    /// </summary>
    [JsonRequired, JsonPropertyName("invoice_id")]
    public long InvoiceId { get; set; }

    /// <summary>
    /// Status of the invoice, can be either “active”, “paid” or “expired”.
    /// </summary>
    [JsonRequired]
    public Statuses Status { get; set; }

    /// <summary>
    /// Hash of the invoice.
    /// </summary>
    [JsonRequired]
    public string Hash { get; set; }

    /// <summary>
    /// URL should be presented to the user to pay the invoice.
    /// </summary>
    [Obsolete("The field PayUrl is now deprecated, use the new field BotInvoiceUrl instead"), JsonPropertyName("pay_url")]
    public string PayUrl { get; set; }

    /// <summary>
    /// Date the invoice was created in ISO 8601 format.
    /// </summary>
    [JsonRequired, JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// True, if the user can pay the invoice anonymously.
    /// </summary>
    [JsonPropertyName("allow_anonymous")]
    public bool? AllowAnonymous { get; set; }

    /// <summary>
    /// Optional. Date the invoice expires in Unix time.
    /// </summary>
    [JsonPropertyName("expiration_date")]
    public DateTime? ExpirationDate { get; set; }

    /// <summary>
    /// Optional. Date the invoice was paid in Unix time.
    /// </summary>
    [JsonPropertyName("paid_at")]
    public DateTime? PaidAt { get; set; }

    /// <summary>
    /// True, if the invoice was paid anonymously.
    /// </summary>
    [JsonPropertyName("paid_anonymously")]
    public bool? PaidAnonymously { get; set; }

    /// <summary>
    /// Optional. Comment to the payment from the user.
    /// </summary>
    public string Comment { get; set; }

    /// <inheritdoc />
    public string Asset { get; set; }

    /// <inheritdoc />
    [JsonRequired]
    public double Amount { get; set; }

    /// <inheritdoc />
    public string Description { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("allow_comments")]
    public bool? AllowComments { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("hidden_message")]
    public string HiddenMessage { get; set; }

    /// <inheritdoc />
    public string Payload { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("paid_btn_name")]
    public PaidButtonNames? PaidBtnName { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("paid_btn_url")]
    public string PaidBtnUrl { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("currency_type")]
    public CurrencyTypes CurrencyType { get; set; }

    /// <inheritdoc />
    [JsonPropertyName("accepted_assets")]
    public IEnumerable<string> AcceptedAssets { get; set; }

    /// <summary>
    /// Optional. Amount of charged service fees.
    /// </summary>
    [Obsolete("The field Fee in the Webhook update payload is now deprecated, use the new field FeeAmount instead")]
    public string Fee { get; set; }

    /// <summary>
    /// Optional. Price of the asset in USD at the time the invoice was paid.
    /// </summary>
    [Obsolete("The field UsdRate in the Webhook update payload is now deprecated, use the new field PaidUsdRate instead"), JsonPropertyName("usd_rate")]
    public string UsdRate { get; set; }

    /// <summary>
    /// URL should be provided to the user to pay the invoice.
    /// </summary>
    [JsonRequired, JsonPropertyName("bot_invoice_url")]
    public string BotInvoiceUrl { get; set; }

    /// <summary>
    /// Optional. Price of the asset in USD. Available only if status is <see cref="Statuses.paid"/>.
    /// </summary>
    [JsonPropertyName("paid_usd_rate")]
    public string PaidUsdRate { get; set; }

    /// <summary>
    /// Optional. Amount of service fees charged when the invoice was paid.
    /// Available only if status is <see cref="Statuses.paid"/>.
    /// </summary>
    [JsonPropertyName("fee_amount")]
    public double FeeAmount { get; set; }

    /// <summary>
    /// Optional. Fiat currency code. Available only if the value of the field <see cref="CurrencyType"/> is <see cref="CurrencyTypes.fiat"/>.
    /// Currently one of fiat from <see cref="Assets"/>.
    /// </summary>
    public string Fiat { get; set; }

    /// <summary>
    /// Optional. Cryptocurrency alphabetic code for which the invoice was paid.
    /// Available only if <see cref="CurrencyType"/> is <see cref="CurrencyTypes.fiat"/> and status is <see cref="Statuses.paid"/>.
    /// </summary>
    [JsonPropertyName("paid_asset")]
    public string PaidAsset { get; set; }

    /// <summary>
    /// Optional. Amount of the invoice for which the invoice was paid.
    /// Available only if <see cref="CurrencyType"/> is <see cref="CurrencyTypes.fiat"/> and status is <see cref="Statuses.paid"/>.
    /// </summary>
    [JsonPropertyName("paid_amount")]
    public string PaidAmount { get; set; }

    /// <summary>
    /// Optional. The rate of the paid_asset valued in the fiat currency.
    /// Available only if the value of the field <see cref="CurrencyType"/> is <see cref="CurrencyTypes.fiat"/> and the value of the field status is <see cref="Statuses.paid"/>.
    /// </summary>
    [JsonPropertyName("paid_fiat_rate")]
    public string PaidFiatRate { get; set; }

    /// <summary>
    /// Optional. Asset of service fees charged when the invoice was paid.
    /// Available only if status is <see cref="Statuses.paid"/>.
    /// </summary>
    [JsonPropertyName("fee_asset")]
    public string FeeAsset { get; set; }
}