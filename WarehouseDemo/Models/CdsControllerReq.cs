// ----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
// ----------------------------------------------------------------------------

namespace WarehouseDemo.Models
{
	using Newtonsoft.Json;

	public partial class UpdateDataRequest
	{
		// DataMember and DataContract can also be used as an alternative to JsonProperty for serialization & deserialization
		[JsonProperty("baseId", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
		public string BaseId { get; set; }

		[JsonProperty("updatedData", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
		public string UpdatedData { get; set; }

		[JsonProperty("updateEntityType", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
		public string UpdateEntityType { get; set; }
	}

	public partial class AddDataRequest
	{
		[JsonProperty("newData", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
		public string NewData { get; set; }

		[JsonProperty("addEntityType", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
		public string AddEntityType { get; set; }
	}

	public partial class AddAndUpdateDataRequest
	{
		[JsonProperty("UpdateReqBody", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
		public UpdateDataRequest UpdateReqBody { get; set; }
		
		[JsonProperty("AddReqBody", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
		public AddDataRequest AddReqBody { get; set; }
	}

	public partial class UpdateDataRequest
	{
		public static UpdateDataRequest FromJson(string jsonString) => JsonConvert.DeserializeObject<UpdateDataRequest>(jsonString, WarehouseDemo.Models.Converter.Settings);
	}

	public partial class AddDataRequest
	{
		public static AddDataRequest FromJson(string jsonString) => JsonConvert.DeserializeObject<AddDataRequest>(jsonString, WarehouseDemo.Models.Converter.Settings);
	}

	public partial class AddAndUpdateDataRequest
	{
		public static AddAndUpdateDataRequest FromJson(string jsonString) => JsonConvert.DeserializeObject<AddAndUpdateDataRequest>(jsonString, WarehouseDemo.Models.Converter.Settings);
	}
}