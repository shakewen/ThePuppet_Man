using UnityEngine;

public class ButtonCtrl : MonoBehaviour
{
	public void OnMoreClick()
	{
		SingletonMonoBehavior<HWManager>.Instance.OpenMoreUrl();
	}

	public void OnRateClick()
	{
		SingletonMonoBehavior<HWManager>.Instance.OpenRateUrl();
	}

	public void OnRankClick()
	{
	}
}
