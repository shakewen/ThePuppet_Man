using UnityEngine;

public class knife : MonoBehaviour
{
	public float forceCount;

	public int scoreAtk = 10;

	private GameObject[] playerPoint;

	private int playerPointNum;

	private bool mouseUpDO;

	public GameObject fxHit;

	private bool hitDO;

	private Rigidbody m_rigidbody;

	private void Start()
	{
		playerPoint = GameObject.FindGameObjectsWithTag("playerPoint");
		playerPointNum = Random.Range(0, playerPoint.Length);
		Debug.Log(playerPoint.Length);
        m_rigidbody = GetComponent<Rigidbody>();

    }

	private void FixedUpdate()
	{
		if (!mouseUpDO)
		{
			base.transform.LookAt(playerPoint[playerPointNum].transform.position);
		}
	}

	public void mouseUp()
	{
		GetComponent<Collider>().enabled = true;
        Rigidbody component = base.gameObject.GetComponent<Rigidbody>();
		if (component == null)
		{
			
			
			component = gameObject.AddComponent<Rigidbody>();

        }
        component.isKinematic = false;
		component.AddRelativeForce(Vector3.forward * forceCount);
		mouseUpDO = true;
	}

	private void OnCollisionEnter(Collision collisionInfo)
	{
		if (!hitDO)
		{
			if (collisionInfo.transform.tag == "body")
			{
				GetComponent<Collider>().enabled = false;
				Object.Destroy(GetComponent<Rigidbody>());
				GameObject.FindGameObjectWithTag("gameManage").GetComponent<gameManage>().coinAdd(scoreAtk);
				base.transform.SetParent(collisionInfo.transform);
				hitDO = true;
			}
			ContactPoint contactPoint = collisionInfo.contacts[0];
			Vector3 point = contactPoint.point;
			Object.Instantiate(fxHit, point, Quaternion.identity);
			Object.Destroy(base.gameObject, 5f);
		}
	}
}
