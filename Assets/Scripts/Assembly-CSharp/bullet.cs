using UnityEngine;

public class bullet : MonoBehaviour
{
	[HideInInspector]
	public float forceCount;

	[HideInInspector]
	public int scoreAtk = 10;

	public GameObject fxHit;

	private bool hitDO;

	private void Start()
	{
		Object.Destroy(base.gameObject, 5f);
		GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * forceCount);
	}

	private void Update()
	{
	}

	private void OnCollisionEnter(Collision collisionInfo)
	{
		if (!hitDO && collisionInfo.transform.tag == "body")
		{
			GameObject.FindGameObjectWithTag("gameManage").GetComponent<gameManage>().coinAdd(scoreAtk);
			GetComponent<Collider>().enabled = false;
			GetComponent<Rigidbody>().useGravity = true;
			Object.Instantiate(fxHit, base.transform.position, Quaternion.identity);
			Object.Destroy(base.gameObject);
			hitDO = true;
		}
	}
}
