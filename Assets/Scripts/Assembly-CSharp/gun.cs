using UnityEngine;

public class gun : MonoBehaviour
{
	public float atkCD;

	public GameObject bullet;

	public Transform firePoint;

	public float forceCount;

	public float piancha;

	public int scoreAtk = 10;

	public GameObject fxFire;

	private float timeTemp;

	private bool overDO;

	private GameObject[] playerPoint;

	private int playerPointNum;

	private void Start()
	{
		playerPoint = GameObject.FindGameObjectsWithTag("playerPoint");
		playerPointNum = Random.Range(0, playerPoint.Length);
	}

	private void FixedUpdate()
	{
		if (overDO)
		{
			return;
		}
		base.transform.LookAt(playerPoint[playerPointNum].transform.position);
		if (!(Time.time > timeTemp))
		{
			return;
		}
		Object.Instantiate(fxFire, firePoint.position, firePoint.rotation);
		GameObject gameObject = Object.Instantiate(bullet, firePoint.position, firePoint.rotation);
		gameObject.transform.localEulerAngles += new Vector3(Random.Range(0f - piancha, piancha), Random.Range(0f - piancha, piancha), Random.Range(0f - piancha, piancha));
		bullet component = gameObject.GetComponent<bullet>();
		if ((bool)component)
		{
			component.forceCount = forceCount;
			component.scoreAtk = scoreAtk;
		}
		else
		{
			bullet_light component2 = gameObject.GetComponent<bullet_light>();
			if ((bool)component2)
			{
				component2.scoreAtk = scoreAtk;
			}
			else
			{
				bullet_fire component3 = gameObject.GetComponent<bullet_fire>();
				if ((bool)component3)
				{
					component3.scoreAtk = scoreAtk;
				}
			}
		}
		timeTemp = Time.time + atkCD;
	}

	public void mouseUp()
	{
		GetComponent<Rigidbody>().isKinematic = false;
		overDO = true;
		Object.Destroy(base.gameObject, 5f);
	}
}
