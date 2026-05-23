using System;

namespace Sekai
{
	[Flags]
	public enum HeaderCategory
	{
		Default = 0x107,
		Rank = 1,
		Currency = 2,
		LiveBoost = 4,
		Gacha = 8,
		GachaItemExchange = 0x10,
		CrystalShop = 0x120,
		EventItem = 0x40,
		TicketExchange = 0x80,
		Menu = 0x100,
		SpecialMaterialExchange = 0x200,
		CurrencyPaid = 0x400
	}
}
