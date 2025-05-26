using Umeng;

public class GameAnalytics : SingletonHW<GameAnalytics>
{
	public void Init()
	{
		Analytics.StartWithAppKeyAndChannelId("5bbee8c5b465f5c51f0002e1", "App Store");
		Analytics.SetLogEnabled(false);
	}

	public void Clear()
	{
	}
}
