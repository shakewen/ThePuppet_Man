using UnityEngine;

public class freeCoin_btnAtkObj : MonoBehaviour
{
	public float checkCD;

	private float timeTemp;

	[HideInInspector]
	public bool AD_DO;

	private void Start()
	{
	}

	private void FixedUpdate()
	{
		if (!AD_DO && Time.time > timeTemp)
		{
			
			if (SingletonHW<AdManager>.Instance.IsReadyReward())
			{
                
                AD_DO = true;
			}
			timeTemp = Time.time + checkCD;
			//Debug.Log(AD_DO);
		}
	}

	public void ShowRewardAD()
	{
		SingletonHW<AdManager>.Instance.ShowReward(Success, Fail);
		AD_DO = false;
	}

	public void Success(string str)
	{
	}

	public void Fail()
	{
	}
}
