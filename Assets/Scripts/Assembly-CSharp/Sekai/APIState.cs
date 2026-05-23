namespace Sekai
{
	public enum APIState
	{
		Ready = 0,
		ConnectionFailed = 1,
		Sent = 2,
		SentFailed = 3,
		Timeout = 4,
		WaitResponse = 5,
		Interrupting = 6,
		Interrupted = 7,
		Reveived = 8,
		ReceiveFailed = 9,
		HttpClientError = 10,
		HttpServerError = 11,
		ServerMaintenance = 12,
		ModuleMaintenance = 13,
		ResponseDeserializeFailed = 14,
		UnknownFailed = 15,
		Complete = 16,
		FailedComplete = 17,
		SuccessComplete = 18,
		UpdateRequired = 19,
		UpdateDataRequired = 20,
		Cheat = 21,
		CSBan = 22,
		LoginBan = 23,
		CSInspecting = 24,
		InvalidToken = 25,
		NotReachable = 26,
		CookieError = 27,
		OutOfPeriodAprilFool2022 = 28,
		UpdateRuleAgreementRequired = 29,
		UserBlockCountLimit = 30
	}
}
