using UnityEngine;
using admob;

public class admobdemo : MonoBehaviour
{
	private Admob ad;

	private void Start()
	{
		Debug.Log("start unity demo-------------");
		initAdmob();
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			Debug.Log(string.Concat(KeyCode.Escape, "-----------------"));
		}
	}

	private void initAdmob()
	{
		ad = Admob.Instance();
		ad.bannerEventHandler += onBannerEvent;
		ad.interstitialEventHandler += onInterstitialEvent;
		ad.rewardedVideoEventHandler += onRewardedVideoEvent;
		ad.nativeBannerEventHandler += onNativeBannerEvent;
		ad.initAdmob("ca-app-pub-3940256099942544/2934735716", "ca-app-pub-3940256099942544/4411468910");
		ad.setGender(AdmobGender.MALE);
		string[] array = new string[3] { "game", "crash", "male game" };
		Debug.Log("admob inited -------------");
	}

	private void OnGUI()
	{
		if (GUI.Button(new Rect(120f, 0f, 100f, 60f), "showInterstitial"))
		{
			if (ad.isInterstitialReady())
			{
				ad.showInterstitial();
			}
			else
			{
				ad.loadInterstitial();
			}
		}
		if (GUI.Button(new Rect(240f, 0f, 100f, 60f), "showRewardVideo"))
		{
			if (ad.isRewardedVideoReady())
			{
				ad.showRewardedVideo();
			}
			else
			{
				ad.loadRewardedVideo("ca-app-pub-3940256099942544/1712485313");
			}
		}
		if (GUI.Button(new Rect(0f, 100f, 100f, 60f), "showbanner"))
		{
			Admob.Instance().showBannerRelative(AdSize.SmartBanner, AdPosition.BOTTOM_CENTER, 0);
		}
		if (GUI.Button(new Rect(120f, 100f, 100f, 60f), "showbannerABS"))
		{
			Admob.Instance().showBannerAbsolute(AdSize.Banner, 20, 300);
		}
		if (GUI.Button(new Rect(240f, 100f, 100f, 60f), "removebanner"))
		{
			Admob.Instance().removeBanner();
		}
		string nativeBannerID = "ca-app-pub-3940256099942544/3986624511";
		if (GUI.Button(new Rect(0f, 200f, 100f, 60f), "showNative"))
		{
			Admob.Instance().showNativeBannerRelative(new AdSize(320, 132), AdPosition.BOTTOM_CENTER, 0, nativeBannerID);
		}
		if (GUI.Button(new Rect(120f, 200f, 100f, 60f), "showNativeABS"))
		{
			Admob.Instance().showNativeBannerAbsolute(new AdSize(-1, 200), 20, 300, nativeBannerID);
		}
		if (GUI.Button(new Rect(240f, 200f, 100f, 60f), "removeNative"))
		{
			Admob.Instance().removeNativeBanner();
		}
	}

	private void onInterstitialEvent(string eventName, string msg)
	{
		Debug.Log("handler onAdmobEvent---" + eventName + "   " + msg);
		if (eventName == AdmobEvent.onAdLoaded)
		{
			Admob.Instance().showInterstitial();
		}
	}

	private void onBannerEvent(string eventName, string msg)
	{
		Debug.Log("handler onAdmobBannerEvent---" + eventName + "   " + msg);
	}

	private void onRewardedVideoEvent(string eventName, string msg)
	{
		Debug.Log("handler onRewardedVideoEvent---" + eventName + "  rewarded: " + msg);
	}

	private void onNativeBannerEvent(string eventName, string msg)
	{
		Debug.Log("handler onAdmobNativeBannerEvent---" + eventName + "   " + msg);
	}
}
