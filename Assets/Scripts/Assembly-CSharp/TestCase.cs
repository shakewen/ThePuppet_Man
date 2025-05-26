using Umeng;
using UnityEngine;

public class TestCase : MonoBehaviour
{
	private void Start()
	{
		Analytics.StartWithAppKeyAndChannelId("551bc899fd98c55326000032", "App Store");
		Analytics.SetLogEnabled(true);
		Analytics.SetLogEncryptEnabled(true);
		GA.ProfileSignIn("fkdafjadklfjdklf");
		GA.ProfileSignIn("jfkdajfdakfj", "app strore");
		MonoBehaviour.print("GA.ProfileSignOff();");
		GA.ProfileSignOff();
	}

	private void OnGUI()
	{
		if (GUI.Button(new Rect(150f, 100f, 500f, 100f), "Event"))
		{
			GA.SetUserLevel(2);
			Analytics.Event(new string[3] { "one", "1234567890123456000", "one" }, 2, "label");
		}
	}
}
