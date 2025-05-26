using System.Collections;
using UnityEngine;

public class autoFireCD : MonoBehaviour
{
	[HideInInspector]
	public float autoCD;

	[HideInInspector]
	public GameObject autoFirePoint;

	public void begin()
	{
		StartCoroutine(waitAutoCD());
	}

	private IEnumerator waitAutoCD()
	{
		autoFirePoint.SetActive(false);
		yield return new WaitForSeconds(autoCD);
		autoFirePoint.SetActive(true);
		Object.Destroy(base.gameObject);
	}

	private void Update()
	{
	}
}
