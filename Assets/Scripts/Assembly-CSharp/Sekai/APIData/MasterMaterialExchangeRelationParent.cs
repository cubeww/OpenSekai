using System.Collections.Generic;
using MessagePack;

namespace Sekai.ApiData
{
	[MessagePackObject(false)]
	public class MasterMaterialExchangeRelationParent
	{
		[Key("id")]
		public int id;

		[Key("description")]
		public string description;

		[Key("assetbundleName")]
		public string assetbundleName;

		[Key("materialExchangeRelationChildren")]
		public List<MasterMaterialExchangeRelationChild> materialExchangeRelationChildren;

		public MasterMaterialExchangeRelationParent()
		{
		}
	}
}
