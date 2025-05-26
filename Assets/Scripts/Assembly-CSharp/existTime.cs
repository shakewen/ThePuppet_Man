using UnityEngine;

public class existTime : MonoBehaviour
{
	public float eTime = 2f;

	private void Start()
	{
		Object.Destroy(base.gameObject, eTime);
	}
}
