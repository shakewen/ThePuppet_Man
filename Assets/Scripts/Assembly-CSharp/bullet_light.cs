using UnityEngine;

public class bullet_light : MonoBehaviour
{
	public float scaleSpeed;

	private bool scaleOverDO;

	[HideInInspector]
	public int scoreAtk = 10;

	public GameObject fxHit;

	private bool hitDO;

	private void Start()
	{
		Object.Destroy(base.gameObject, 0.5f);
	}

	private void FixedUpdate()
	{
		if (!scaleOverDO)
		{
			base.transform.localScale += new Vector3(0f, 0f, scaleSpeed);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!hitDO)
		{
			if (other.tag == "body")
			{
				GameObject.FindGameObjectWithTag("gameManage").GetComponent<gameManage>().coinAdd(scoreAtk);
				scaleOverDO = true;
				GetComponent<Collider>().enabled = false;
				other.GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * 100f);
				hitDO = true;
			}
			Object.Instantiate(fxHit, base.transform.Find("hitPoint").position, Quaternion.identity);
			Object.Destroy(base.gameObject, 0.05f);
		}
	}
}
