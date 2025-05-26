using UnityEngine;

public class coinMove : MonoBehaviour
{
	private Transform coinObj;

	private Transform coinPoint;

	private void Start()
	{
		coinPoint = GameObject.FindGameObjectWithTag("coinPoint").transform;
		base.transform.eulerAngles += new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
		coinObj = base.transform.Find("coin");
		Object.Destroy(base.gameObject, 1.5f);
	}

	private void FixedUpdate()
	{
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, Quaternion.LookRotation(coinPoint.position - base.transform.position), 7f * Time.deltaTime);
		base.transform.Translate(Vector3.forward * 5f * Time.deltaTime);
		coinObj.transform.Rotate(Vector3.up * 1500f * Time.deltaTime);
	}
}
