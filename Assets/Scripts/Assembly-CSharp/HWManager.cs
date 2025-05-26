using System;
using System.Collections;
using UnityEngine;

public class HWManager : SingletonMonoBehavior<HWManager>
{
	private const string appid = "1427433142";

	private const string developId = "1296715962";

	private string shareUrl = "https://itunes.apple.com/cn/app/id1427433142";

	private string rateUrl = string.Format("https://itunes.apple.com/cn/app/id{0}?mt=8", "1427433142");

	private string moreUrl = "https://itunes.apple.com/cn/developer/id1296715962";

	private void Start()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		SingletonHW<GameAnalytics>.Instance.Init();
		SingletonHW<AdManager>.Instance.Init();
		DownLoadSelfUrl();
	}

	private void OnGoClick()
	{
		ShowSelfAd(false);
	}

	private void OnCloseClick()
	{
		ShowSelfAd(false);
	}

	public void OpenRateUrl()
	{
		Application.OpenURL(rateUrl);
	}

	public void OpenMoreUrl()
	{
		Application.OpenURL(moreUrl);
	}

	public void ShowSelfAd(bool show)
	{
		if (!show)
		{
		}
	}

	private void DownLoadSelfUrl()
	{
	}

	private void DownLoadSelfUrlSuccess(WWW www)
	{
		if (string.IsNullOrEmpty(www.text))
		{
			Debug.LogWarning("url is null");
			return;
		}
		string[] array = www.text.Split('\n');
		foreach (string text in array)
		{
			Debug.Log(text);
			if (string.IsNullOrEmpty(text))
			{
				continue;
			}
			string[] array2 = text.Split('|');
			if (array2.Length < 3)
			{
				Debug.LogError("param is not enough:" + array2.Length);
			}
			else if (array2[0] == "10000" || array2[0] == "1427433142")
			{
				if (array2.Length > 3)
				{
					SingletonHW<AdManager>.Instance.showAdmob = array2[3] == "0";
				}
				if (array2.Length > 4)
				{
					int result = 3;
					int.TryParse(array2[4], out result);
					SingletonHW<AdManager>.Instance.showInterstitialAd = result;
				}
			}
		}
		DownLoadImage();
	}

	private void DownLoadSelfUrlFail()
	{
	}

	private void DownLoadImage()
	{
	}

	private void DownLoadImageSuccess(WWW www)
	{
	}

	private void DownLoadImageFail()
	{
	}

	public IEnumerator Download(string url, Action<WWW> successHandle, Action failHandle)
	{
		WWW www = new WWW(url + "?" + UnityEngine.Random.Range(1000000, 9999999));
		Debug.Log("Download Start :" + url);
		while (!www.isDone)
		{
			if (www.error != null)
			{
				Debug.Log("Failed not Done" + url);
				failHandle();
				www = null;
				yield break;
			}
			yield return null;
		}
		yield return www;
		if (www.error != null)
		{
			Debug.Log("Failed Done" + url);
			failHandle();
			www = null;
		}
		else
		{
			successHandle(www);
		}
	}
}
