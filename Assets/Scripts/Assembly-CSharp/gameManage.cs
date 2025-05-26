using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManage : MonoBehaviour
{
	public GameObject[] charObj;

	public GameObject[] atkObj;

	public string[] atkObjName;

	public int[] atkObjPiece;

	public Sprite[] wqImage;

	public Transform[] autoFirePoint;

	public GameObject btn;

	public float autoFireCD;

	public Text coinText;

	[HideInInspector]
	public int coinCount;

	public float saveCD = 5f;

	public GameObject noMoneyUI;

	public GameObject getAtkObjUI;

	public GameObject[] partsObj;

	private Animator coinAni;

	private float timeTemp;

	private Animator aniHead;

	private bool openDO;

	private void Start()
	{
		aniHead = GameObject.FindGameObjectWithTag("head").GetComponent<Animator>();
		for (int i = 0; i < charObj.Length; i++)
		{
			charObj[i].SetActive(false);
		}
		int num = PlayerPrefs.GetInt("charNum");
		charObj[num].SetActive(true);
		coinCount = PlayerPrefs.GetInt("coin");
		coinText.text = string.Empty + coinCount;
		GetComponent<control>().atkObj = atkObj;
		for (int j = 0; j < atkObj.Length; j++)
		{
			GameObject gameObject = Object.Instantiate(btn);
			gameObject.transform.SetParent(btn.transform.parent);
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			gameObject.transform.Find("btnAtkObj").gameObject.GetComponent<btncontrol>().atkObjNum = j;
			gameObject.transform.Find("btnUnlock").gameObject.GetComponent<btncontrol>().atkObjNum = j;
			gameObject.transform.Find("btnUnlock").gameObject.GetComponent<btncontrol>().unlockPiece = atkObjPiece[j];
			gameObject.transform.Find("btnAutoFire").gameObject.GetComponent<btncontrol>().atkObjNum = j;
			gameObject.transform.Find("btnAutoFire").gameObject.GetComponent<btncontrol>().autoFireCD = autoFireCD;
			gameObject.transform.Find("btnAtkObj/name").gameObject.GetComponent<Text>().text = string.Empty + atkObjName[j];
			gameObject.transform.Find("btnAtkObj/Image").gameObject.GetComponent<Image>().sprite = wqImage[j];
			gameObject.SetActive(true);
		}
		for (int k = 0; k < partsObj.Length; k++)
		{
			if (PlayerPrefs.GetInt("parts" + k) == 1)
			{
				partsObj[k].SetActive(true);
			}
			else if (PlayerPrefs.GetInt("parts" + k) == 0)
			{
				partsObj[k].SetActive(false);
			}
		}
		coinAni = coinText.transform.parent.gameObject.GetComponent<Animator>();
	}

	private void FixedUpdate()
	{
		if (Time.time > timeTemp)
		{
			timeTemp = Time.time + saveCD;
			PlayerPrefs.SetInt("coin", coinCount);
		}
	}

	public void coinAdd(int coinCountAdd)
	{
		aniHead.SetTrigger("hit");
		coinCount += coinCountAdd;
		coinText.text = string.Empty + coinCount;
		coinAni.SetTrigger("play");
		int num = Mathf.FloorToInt(coinCountAdd / 5) + 1;
		for (int i = 0; i < num; i++)
		{
			Object.Instantiate(Resources.Load("FX/coin") as GameObject, GameObject.FindGameObjectWithTag("playerPoint").transform.position, Quaternion.identity);
		}
	}

	public void btnAtkObj(int num)
	{
		GetComponent<control>().objNum = num;
	}

	public void btnCharSelect()
	{
		SceneManager.LoadScene("charSelect");
	}

	public void btnAutoFire(int num)
	{
		openDO = false;
		for (int i = 0; i < autoFirePoint.Length; i++)
		{
			if (autoFirePoint[i].gameObject.activeInHierarchy)
			{
				autoFireCD component = Object.Instantiate(atkObj[num], autoFirePoint[i].position, Quaternion.identity).GetComponent<autoFireCD>();
				component.autoCD = autoFireCD;
				component.autoFirePoint = autoFirePoint[i].gameObject;
				component.begin();
				break;
			}
		}
		for (int j = 0; j < autoFirePoint.Length; j++)
		{
			if (autoFirePoint[j].gameObject.activeInHierarchy)
			{
				openDO = true;
			}
		}
		if (openDO)
		{
			return;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("btnAutoFire");
		for (int k = 0; k < array.Length; k++)
		{
			array[k].transform.localPosition = new Vector3(-2000f, 0f, 0f);
			if (!array[k].GetComponent<btncontrol>().autoDO)
			{
				array[k].transform.parent.Find("timeText").gameObject.SetActive(false);
			}
		}
	}

	public void getAtkObj(int num)
	{
		getAtkObjUI.SetActive(true);
		getAtkObjUI.transform.Find("Image").gameObject.GetComponent<Image>().sprite = wqImage[num];
	}
}
