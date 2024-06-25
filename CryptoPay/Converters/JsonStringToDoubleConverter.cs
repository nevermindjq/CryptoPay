using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CryptoPay.Converters {
	internal class JsonStringToDoubleConverter : JsonConverter<double> {
		public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
			return double.Parse(reader.GetString()!);
		}

		public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options) {
			throw new NotImplementedException();
		}
	}
}