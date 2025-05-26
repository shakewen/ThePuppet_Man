using UnityEngine;

public class addMoneyInput : MonoBehaviour
{
	public string coinName = "coin";

	public int coinAddCount = 1000;

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Delete))
		{
			PlayerPrefs.DeleteAll();
		}
		else if (Input.GetKeyDown(KeyCode.Insert))
		{
			PlayerPrefs.SetInt(coinName, PlayerPrefs.GetInt(coinName) + coinAddCount);
		}
	}
}
