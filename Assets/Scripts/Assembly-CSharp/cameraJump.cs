using System.Collections;
using UnityEngine;

public class cameraJump : MonoBehaviour
{
	public float jumpCD = 0.01f;

	private bool jumpDO = true;

	public float jumpLength = 0.5f;

	private bool cameraJumpDO;

	private Vector3 positionTemp;

	public float strength = 0.1f;

	private void Start()
	{
	}

	private void Update()
	{
		if (cameraJumpDO && jumpDO)
		{
			base.transform.localPosition = new Vector3(positionTemp.x + Random.Range(0f - strength, strength), positionTemp.y + Random.Range(0f - strength, strength), positionTemp.z);
			jumpDO = false;
			StartCoroutine(waitJumpCD());
		}
	}

	private IEnumerator waitJumpCD()
	{
		yield return new WaitForSeconds(jumpCD);
		jumpDO = true;
	}

	public void camera_jump()
	{
		cameraJumpDO = true;
		positionTemp = base.transform.localPosition;
		StartCoroutine(waitJumpLength());
	}

	private IEnumerator waitJumpLength()
	{
		yield return new WaitForSeconds(jumpLength);
		cameraJumpDO = false;
		base.transform.localPosition = positionTemp;
	}
}
