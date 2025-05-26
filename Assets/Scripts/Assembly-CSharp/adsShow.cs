using UnityEngine;

public class adsShow : MonoBehaviour
{
	public float showCD;

	private float timeTemp;

	private void Start()
	{
		timeTemp = Time.time + showCD;
	}

	private void FixedUpdate()
	{
		if (Time.time > timeTemp)
		{
			SingletonHW<AdManager>.Instance.GameOver();
			timeTemp = Time.time + showCD;
		}
	}
}
