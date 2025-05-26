using UnityEngine;

public class bullet_fire : MonoBehaviour
{
	[HideInInspector]
	public int scoreAtk = 10;

	private bool hitDO;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!hitDO)
		{
			if (other.tag == "body")
			{
				GameObject.FindGameObjectWithTag("gameManage").GetComponent<gameManage>().coinAdd(scoreAtk);
				GetComponent<Collider>().enabled = false;
				other.GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * 100f);
				hitDO = true;
			}
			Object.Destroy(base.gameObject, 2f);
		}
	}
}
