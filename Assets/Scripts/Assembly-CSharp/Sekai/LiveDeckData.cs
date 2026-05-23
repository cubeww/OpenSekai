using Sekai.Multiplay;

namespace Sekai
{
	public class LiveDeckData
	{
		public class UserData
		{
			public int DeckId { get; set; }
			public string DeckName { get; set; }
			public int Leader { get; set; }
			public int SubLeader { get; set; }
			public int Member1 { get; set; }
			public int Member2 { get; set; }
			public int Member3 { get; set; }
			public int Member4 { get; set; }
			public int Member5 { get; set; }
			public int CharacterId { get; set; }

			public UserData(UserDeck userDeck)
			{
				DeckName = string.Empty;
			}

			public UserData(UserChallengeLiveSoloDeck userChallengeLiveSoloDeck)
			{
				DeckName = string.Empty;
			}
		}

		public UserData UserInfo { get; set; }

		public LiveDeckMember[] Members { get; private set; }

		public LiveDeckMember[] FormationMembers { get; set; }

		public int TotalPowerIncludeBuff { get; set; }

		public LiveDeckData()
		{
			Members = System.Array.Empty<LiveDeckMember>();
			FormationMembers = Members;
		}

		public LiveDeckData(int deckId, bool is6MemberMv = false)
			: this()
		{
			UserInfo = new UserData((UserDeck)null) { DeckId = deckId };
		}

		public LiveDeckData(int[] deckCardIds)
			: this()
		{
		}

		public LiveDeckData(MultiLivePartyMember member)
			: this()
		{
		}

		public void AttachFormationInfo(int[] formationIndexes)
		{
			FormationMembers = Members;
		}
	}
}
