using System;
using UnityEngine;
using UnityEngine.Advertisements;
using admob;

public class AdManager : SingletonHW<AdManager>
{
	private Admob ad;

	private string bannerId = "ca-app-pub-3973911991874301/8229656381";

	private string interstitialId = "ca-app-pub-3756638287360783/8284112807";

	private string rewardId = "ca-app-pub-3756638287360783/6587887755";

	private string unityId = "2845472";

	private string unityRewardKey = "rewardedVideo";

	private bool showBanner;

	private bool showInterstitial = true;

	private int m_showInterstitialAd;

	private int interstitialCount;

	private bool m_showAdmob;

	private Action<string> rewardDoneCallBack;

	private Action rewardCancelCallBack;

	private string adsKey = "ads";

	public int showInterstitialAd
	{
		get
		{
			return m_showInterstitialAd;
		}
		set
		{
			m_showInterstitialAd = value;
			PlayerPrefs.SetInt("showInterstitialAd", value);
		}
	}

	public bool showAdmob
	{
		get
		{
			return m_showAdmob;
		}
		set
		{
			m_showAdmob = value;
			PlayerPrefs.SetInt("showAdmob", (!m_showAdmob) ? 1 : 0);
		}
	}

	public void Init()
	{
		m_showInterstitialAd = PlayerPrefs.GetInt("showInterstitialAd", 1);
		m_showAdmob = PlayerPrefs.GetInt("showAdmob", 0) == 0;
		InitAdmob();
		Advertisement.Initialize(unityId);
	}

	public void Clear()
	{
	}

	private void InitAdmob()
	{
		ad = Admob.Instance();
		ad.bannerEventHandler += OnBannerEvent;
		ad.interstitialEventHandler += OnInterstitialEvent;
		ad.rewardedVideoEventHandler += OnRewardedVideoEvent;
		ad.nativeBannerEventHandler += OnNativeBannerEvent;
		ad.initAdmob(bannerId, interstitialId);
		ad.loadRewardedVideo(rewardId);
		ad.loadInterstitial();
		showInterstitial = PlayerPrefs.GetInt(adsKey, 1) == 1;
		Debug.Log("admob inited -------------");
	}

	public void GameOver()
	{
		if (showInterstitial)
		{
			interstitialCount++;
			Debug.Log("adGameOverCount:" + interstitialCount);
			if (interstitialCount >= showInterstitialAd)
			{
				ShowInterstitial();
				interstitialCount = 0;
			}
			else
			{
				ad.loadInterstitial();
			}
		}
	}

	public void RemoveAds()
	{
		showInterstitial = false;
		PlayerPrefs.SetInt(adsKey, 0);
	}

	private void ShowBanner()
	{
		Debug.Log("ShowBanner");
		Admob.Instance().showBannerRelative(AdSize.SmartBanner, AdPosition.BOTTOM_CENTER, 0);
	}

	private void HideBanner()
	{
		Debug.Log("HideBanner");
		Admob.Instance().removeBanner();
	}

	private void ShowInterstitial()
	{
		Debug.Log("ShowInterstitial");
		bool flag = false;
		if (!((!showAdmob) ? ShowUnityInterstitial() : ShowAdmobInterstitial()))
		{
			if (!showAdmob)
			{
				ShowAdmobInterstitial();
			}
			else
			{
				ShowUnityInterstitial();
			}
		}
	}

	private bool ShowAdmobInterstitial()
	{
		if (ad == null)
		{
			return false;
		}
		if (ad.isInterstitialReady())
		{
			ad.showInterstitial();
			return true;
		}
		ad.loadInterstitial();
		return false;
	}

	private bool ShowUnityInterstitial()
	{
		if (Advertisement.IsReady())
		{
			Advertisement.Show();
			return true;
		}
		return false;
	}

	public bool IsReadyReward()
	{
		if (ad == null)
		{
			return false;
		}
		if (ad.isRewardedVideoReady() || Advertisement.IsReady(unityRewardKey))
		{
			return true;
		}
		ad.loadRewardedVideo(rewardId);
		return false;
	}

	/// <summary>
/// 显示奖励广告，根据配置选择广告平台，并在失败时触发取消回调。
/// </summary>
/// <param name="doneCallBack">奖励成功时的回调，参数为奖励标识</param>
/// <param name="cancelCallBack">广告无法显示或失败时的取消回调</param>
/// <returns>None</returns>
public void ShowReward(Action<string> doneCallBack, Action cancelCallBack)
{
    Debug.Log("ShowReward");
    rewardDoneCallBack = doneCallBack;
    rewardCancelCallBack = cancelCallBack;

    // 尝试根据当前广告平台显示奖励广告，若失败则切换平台再次尝试
    bool flag = false;
    flag = ((!showAdmob) ? ShowUnityReward() : ShowAdmobReward());

    // 如果首次尝试失败，切换广告平台进行备用尝试
    if (!flag)
    {
        flag = (showAdmob ? ShowUnityReward() : ShowAdmobReward());
    }

    // 所有广告尝试均失败，触发取消回调并清空引用
    if (!flag && rewardCancelCallBack != null)
    {
        rewardCancelCallBack();
        rewardCancelCallBack = null;
    }
}

	private bool ShowUnityReward()
	{
		if (Advertisement.IsReady(unityRewardKey))
		{
			ShowOptions showOptions = new ShowOptions();
			showOptions.resultCallback = HandleShowResult;
			Advertisement.Show(unityRewardKey, showOptions);
			return true;
		}
		return false;
	}

	private void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log("reward done");
			if (rewardDoneCallBack != null)
			{
				rewardDoneCallBack("1");
				rewardDoneCallBack = null;
			}
			break;
		case ShowResult.Skipped:
			Debug.Log("cancelRewardAd");
			if (rewardCancelCallBack != null)
			{
				rewardCancelCallBack();
				rewardCancelCallBack = null;
			}
			break;
		case ShowResult.Failed:
			Debug.Log("cancelRewardAd");
			if (rewardCancelCallBack != null)
			{
				rewardCancelCallBack();
				rewardCancelCallBack = null;
			}
			break;
		}
	}

	private bool ShowAdmobReward()
	{
		if (ad == null)
		{
			return false;
		}
		if (ad.isRewardedVideoReady())
		{
			ad.showRewardedVideo();
			return true;
		}
		ad.loadRewardedVideo(rewardId);
		return false;
	}

	private void OnInterstitialEvent(string eventName, string msg)
	{
		Debug.Log("handler onAdmobEvent---" + eventName + "   " + msg);
		if (eventName == AdmobEvent.onAdClosed)
		{
			ad.loadInterstitial();
		}
	}

	private void OnBannerEvent(string eventName, string msg)
	{
		Debug.Log("handler onAdmobBannerEvent---" + eventName + "   " + msg);
	}

	private void OnRewardedVideoEvent(string eventName, string msg)
	{
		Debug.Log("handler onRewardedVideoEvent---" + eventName + "  rewarded: " + msg);
		if (eventName == AdmobEvent.onRewarded)
		{
			Debug.Log("Rewarding player...rewarded count" + msg);
			if (rewardDoneCallBack != null)
			{
				rewardDoneCallBack(msg);
				rewardDoneCallBack = null;
			}
		}
		if (eventName == AdmobEvent.onAdClosed)
		{
			Debug.Log("cancelRewardAd");
			if (rewardCancelCallBack != null)
			{
				rewardCancelCallBack();
				rewardCancelCallBack = null;
			}
			ad.loadRewardedVideo(rewardId);
		}
	}

	private void OnNativeBannerEvent(string eventName, string msg)
	{
		Debug.Log("handler onAdmobNativeBannerEvent---" + eventName + "   " + msg);
	}
}
