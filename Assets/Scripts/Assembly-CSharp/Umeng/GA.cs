using System;
using UnityEngine;

namespace Umeng
{
	public class GA : Analytics
	{
		public enum Gender
		{
			Unknown = 0,
			Male = 1,
			Female = 2
		}

		public enum PaySource
		{
			AppStore = 1,
			支付宝 = 2,
			网银 = 3,
			财付通 = 4,
			移动 = 5,
			联通 = 6,
			电信 = 7,
			Paypal = 8,
			Source9 = 9,
			Source10 = 10,
			Source11 = 11,
			Source12 = 12,
			Source13 = 13,
			Source14 = 14,
			Source15 = 15,
			Source16 = 16,
			Source17 = 17,
			Source18 = 18,
			Source19 = 19,
			Source20 = 20
		}

		public enum BonusSource
		{
			玩家赠送 = 1,
			Source2 = 2,
			Source3 = 3,
			Source4 = 4,
			Source5 = 5,
			Source6 = 6,
			Source7 = 7,
			Source8 = 8,
			Source9 = 9,
			Source10 = 10
		}

		public static void SetUserLevel(int level)
		{
			Analytics.Agent.CallStatic("setPlayerLevel", level);
		}

		[Obsolete("SetUserLevel(string level) 已弃用, 请使用 SetUserLevel(int level)")]
		public static void SetUserLevel(string level)
		{
			Debug.LogWarning("SetUserLevel(string level) 已弃用, 请使用 SetUserLevel(int level)");
		}

		[Obsolete("SetUserInfo已弃用, 请使用ProfileSignIn")]
		public static void SetUserInfo(string userId, Gender gender, int age, string platform)
		{
			Analytics.Agent.CallStatic("setPlayerInfo", userId, age, (int)gender, platform);
		}

		public static void StartLevel(string level)
		{
			Analytics.Agent.CallStatic("startLevel", level);
		}

		public static void FinishLevel(string level)
		{
			Analytics.Agent.CallStatic("finishLevel", level);
		}

		public static void FailLevel(string level)
		{
			Analytics.Agent.CallStatic("failLevel", level);
		}

		public static void Pay(double cash, PaySource source, double coin)
		{
			Analytics.Agent.CallStatic("pay", cash, coin, (int)source);
		}

		public static void Pay(double cash, int source, double coin)
		{
			if (source < 1 || source > 100)
			{
				throw new ArgumentException();
			}
			Analytics.Agent.CallStatic("pay", cash, coin, source);
		}

		public static void Pay(double cash, PaySource source, string item, int amount, double price)
		{
			Analytics.Agent.CallStatic("pay", cash, item, amount, price, (int)source);
		}

		public static void Buy(string item, int amount, double price)
		{
			Analytics.Agent.CallStatic("buy", item, amount, price);
		}

		public static void Use(string item, int amount, double price)
		{
			Analytics.Agent.CallStatic("use", item, amount, price);
		}

		public static void Bonus(double coin, BonusSource source)
		{
			Analytics.Agent.CallStatic("bonus", coin, (int)source);
		}

		public static void Bonus(string item, int amount, double price, BonusSource source)
		{
			Analytics.Agent.CallStatic("bonus", item, amount, price, (int)source);
		}

		public static void ProfileSignIn(string userId)
		{
			Analytics.Agent.CallStatic("onProfileSignIn", userId);
		}

		public static void ProfileSignIn(string userId, string provider)
		{
			Analytics.Agent.CallStatic("onProfileSignIn", provider, userId);
		}

		public static void ProfileSignOff()
		{
			Analytics.Agent.CallStatic("onProfileSignOff");
		}
	}
}
