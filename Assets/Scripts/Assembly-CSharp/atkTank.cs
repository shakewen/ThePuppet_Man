using UnityEngine;

public class atkTank : MonoBehaviour
{
	public float speed;

	public float posXstop = -1f;

	public float atkCD = 3f;

	public Transform firePoint;

	public float forceCount;

	public int scoreAtk = 10;

	public GameObject fxFire;

	public GameObject bullet;

	public int atkNumMax = 5;

	private int atkNum;

	private float timeTemp;

	private bool stopDO;

	private Transform playerPoint;

	private Transform tankHead;

	private float posXbegin;

	private void Start()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("playerPoint");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].name == "playerPoint (1)")
			{
				playerPoint = array[i].transform;
				break;
			}
		}
		tankHead = base.transform.Find("head");
		posXbegin = base.transform.position.x;
	}

	private void FixedUpdate()
	{
		if (!stopDO)
		{
			base.transform.Translate(Vector3.forward * speed * Time.deltaTime);
			if (base.transform.position.x > posXstop)
			{
				stopDO = true;
				timeTemp = Time.time + atkCD;
			}
			else if (base.transform.position.x < posXbegin)
			{
				Object.Destroy(base.gameObject);
			}
			return;
		}
		tankHead.rotation = Quaternion.Slerp(tankHead.rotation, Quaternion.LookRotation(playerPoint.position - tankHead.position), 2f * Time.deltaTime);
		if (Time.time > timeTemp)
		{
			Object.Instantiate(fxFire, firePoint.position, firePoint.rotation);
			GameObject gameObject = Object.Instantiate(bullet, firePoint.position, firePoint.rotation);
			bullet component = gameObject.GetComponent<bullet>();
			if ((bool)component)
			{
				component.forceCount = forceCount;
				component.scoreAtk = scoreAtk;
			}
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cameraJump>().camera_jump();
			Handheld.Vibrate();
			atkNum++;
			if (atkNum == atkNumMax)
			{
				stopDO = false;
				speed = 0f - speed;
			}
			timeTemp = Time.time + atkCD;
		}
	}
}
