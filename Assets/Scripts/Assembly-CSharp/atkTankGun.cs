using UnityEngine;

public class atkTankGun : MonoBehaviour
{
	private void Start()
	{
		if (GameObject.FindGameObjectWithTag("atkTank") == null)
		{
			Transform transform = GameObject.FindGameObjectWithTag("tankPoint").transform;
			Object.Instantiate(Resources.Load("atkTank") as GameObject, transform.position, transform.rotation);
		}
	}

	public void mouseUp()
	{
		Object.Destroy(base.gameObject);
	}
}
