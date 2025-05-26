using UnityEngine;

public class control : MonoBehaviour
{
	[HideInInspector]
	public GameObject[] atkObj;

	[HideInInspector]
	public int objNum;

	private Transform atkObjTemp;

	private Camera camera;

	private void Start()
	{
		camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			//print("按下鼠标左键：————————");
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo) && hitInfo.transform.tag == "groud")
			{
				if ((bool)atkObjTemp)
				{
					atkObjTemp.gameObject.SendMessage("mouseUp", base.gameObject, SendMessageOptions.DontRequireReceiver);
				}
				atkObjTemp = Object.Instantiate(atkObj[objNum], hitInfo.point, Quaternion.identity).transform;
			}
		}
		else if (Input.GetMouseButtonUp(0))
		{
			//print("弹起鼠标左键：————————");
			Ray ray2 = camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo2;
			if (Physics.Raycast(ray2, out hitInfo2) && hitInfo2.transform.tag == "groud" && (bool)atkObjTemp)
			{
				atkObjTemp.gameObject.SendMessage("mouseUp", base.gameObject, SendMessageOptions.DontRequireReceiver);
			}
		}
		else if (Input.GetMouseButton(0))
		{
            
			Ray ray3 = camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo3;
			if (Physics.Raycast(ray3, out hitInfo3) && hitInfo3.transform.tag == "groud" && (bool)atkObjTemp)
			{
				atkObjTemp.position = hitInfo3.point;
			}
		}
	}
}
