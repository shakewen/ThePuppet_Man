using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ��ť�������࣬����������ť���Զ������ť�ͽ�����ť�Ľ����߼�
/// ʵ��IPointerDownHandler�ӿڴ���ָ�밴���¼�
/// </summary>
public class btncontrol : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IEventSystemHandler
{
	/// <summary>
	/// ���������ţ�0-��ͨ������5-���⹥����
	/// �������ֲ�ͬ�������͵ı�ʶ��
	/// </summary>
	[HideInInspector]
	public int atkObjNum;

	/// <summary>
	/// �Զ������ȴʱ�䣨�룩
	/// �����Զ�������ܵ���ȴ���
	/// </summary>
	[HideInInspector]
	public float autoFireCD;

	/// <summary>
	/// ����������Ƭ����
	/// �����¹�����������Ļ�������
	/// </summary>
	[HideInInspector]
	public int unlockPiece;

	/// <summary>
	/// �Զ���������־
	/// ָʾ��ǰ�Ƿ����Զ����״̬
	/// </summary>
	[HideInInspector]
	public bool autoDO;

	// ˽�б�����������
	private float lastTime;        // �ϴδ����Զ������ʱ��
	private float nowTime;         // ��ǰʱ�䣨δʹ�ã�
	private float textTime;        // ʣ����ȴʱ����ʾֵ
	private Text timeText;         // ��ȴʱ���ı����
	private gameManage gm;         // ȫ����Ϸ����������
	private freeCoin_btnAtkObj freeCoin; // ��ѻ����������
	private Animator freeCoinTis;  // ��ѻ�����ʾ������

	/// <summary>
	/// ��ʼ������
	/// ��ȡ��Ҫ�����������Ϸ״̬��ʼ����ť״̬
	/// </summary>
	private void Start()
	{
		// ��ȡȫ�ֹ������
		gm = GameObject.FindGameObjectWithTag("gameManage").GetComponent<gameManage>();

		freeCoin = GameObject.FindGameObjectWithTag("gameManage").GetComponent<freeCoin_btnAtkObj>();
		//һ���ı�
		freeCoinTis = GameObject.FindGameObjectWithTag("freeCoinTis").GetComponent<Animator>();
		
		// ��ȡ��ȴʱ���ı����
		timeText = base.transform.parent.Find("timeText").GetComponent<Text>();
		
		// ���ݹ������ͺͰ�ť�������ó�ʼ״̬
		if ((atkObjNum == 0 || atkObjNum == 5) && base.name == "btnAutoFire")
		{
			base.gameObject.SetActive(false);
			timeText.gameObject.SetActive(false);
		}
		
		// ���ù�����ť��ɫ��ʶ
		if (atkObjNum == 0 && base.name == "btnAtkObj")
		{
			GetComponent<Image>().color = new Color(0.32f, 1f, 0f);
		}
		
		// ���������ť��ʼ״̬
		if (atkObjNum == 0 && base.name == "btnUnlock")
		{
			base.gameObject.SetActive(false);
		}
		
		// �������ƫ�����ø���UI״̬
		if (PlayerPrefs.GetInt("atkObj" + atkObjNum) == 1 && base.name == "btnUnlock")
		{
			base.gameObject.SetActive(false);
		}
		else if (PlayerPrefs.GetInt("atkObj" + atkObjNum) == 0 && base.name == "btnUnlock")
		{
			base.transform.parent.Find("btnAutoFire").gameObject.SetActive(false);
			base.transform.parent.Find("timeText").gameObject.SetActive(false);
			base.transform.Find("piece").gameObject.GetComponent<Text>().text = string.Empty + unlockPiece;
		}
	}

	/// <summary>
	/// �̶����·���
	/// ÿ֡������ȴʱ����ʾ
	/// </summary>
	private void FixedUpdate()
	{
		// ����ʣ����ȴʱ��
		textTime = lastTime + autoFireCD - Time.time;
		
		// ��ʽ��ʱ����ʾ����:�룩
		timeText.text = string.Empty + Mathf.Floor(textTime / 60f) + ":" + Mathf.Floor(textTime - Mathf.Floor(textTime / 60f) * 60f);
		
		// ������ȴ�������
		if (textTime < 0f)
		{
			timeText.text = "0:0";
		}
	}

	/// <summary>
	/// ָ�밴���¼�����
	/// ���ݲ�ͬ��ť����ִ�ж�Ӧ����
	/// </summary>
	/// <param name="eventData">�����¼����ݵ�PointerEventData����</param>
	public void OnPointerDown(PointerEventData eventData)
	{
		if (base.name == "btnAtkObj")
		{
			// �л�����ģʽ
			gm.btnAtkObj(atkObjNum);
			
			// �������й�����ť���Ӿ�״̬
			GameObject[] array = GameObject.FindGameObjectsWithTag("btnAtkObj");
			for (int i = 0; i < array.Length; i++)
			{
				array[i].GetComponent<Image>().color = new Color(1f, 1f, 1f);
			}
			GetComponent<Image>().color = new Color(0.32f, 1f, 0f);
		}
		else if (base.name == "btnAutoFire")
		{
			//if (freeCoin.AD_DO)
			//{
			//	// �����Զ��������
			//	freeCoin.ShowRewardAD();
			//	autoDO = true;
			//	gm.btnAutoFire(atkObjNum);
			//	base.transform.localPosition = new Vector3(-2000f, 0f, 0f);
			//	StartCoroutine(waitAutoFire());
			//	lastTime = Time.time;
			//}
			//else
			//{
			//	// ������ѻ�����ʾ����
			//	freeCoinTis.SetTrigger("play");
			//}
			AdManager1.instance.ShowVideo(() =>
			{
				autoDO = true;
				gm.btnAutoFire(atkObjNum);
				base.transform.localPosition = new Vector3(-2000f, 0f, 0f);
				StartCoroutine(waitAutoFire());
				lastTime = Time.time;
			});
		}
		else if (base.name == "btnUnlock")
		{
			if (gm.coinCount >= unlockPiece)
			{
				// ִ�н�������
				gm.coinCount -= unlockPiece;
				gm.coinText.text = string.Empty + gm.coinCount;
				PlayerPrefs.SetInt("coin", gm.coinCount);
				PlayerPrefs.SetInt("atkObj" + atkObjNum, 1);
				base.gameObject.SetActive(false);
				
				// ����UI״̬
				if (atkObjNum != 5)
				{
					base.transform.parent.Find("btnAutoFire").gameObject.SetActive(true);
					base.transform.parent.Find("timeText").gameObject.SetActive(true);
				}
				
				// �������������ȡ���л�
				gm.getAtkObj(atkObjNum);
				gm.btnAtkObj(atkObjNum);
				
				// ���°�ť�Ӿ�״̬
				GameObject[] array2 = GameObject.FindGameObjectsWithTag("btnAtkObj");
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j].GetComponent<Image>().color = new Color(1f, 1f, 1f);
				}
				base.transform.parent.Find("btnAtkObj").gameObject.GetComponent<Image>().color = new Color(0.32f, 1f, 0f);
			}
			else
			{
				// ��ʾ�ʽ�����ʾ
				gm.noMoneyUI.SetActive(true);
				GameObject.FindGameObjectWithTag("gameManage").GetComponent<freeCoin>().freeCoinCheck();
			}
		}
	}

	/// <summary>
	/// �Զ������ȴЭ��
	/// �ȴ�ָ��ʱ������ð�ť״̬
	/// </summary>
	/// <returns>IEnumerator��������Э�̵���</returns>
	private IEnumerator waitAutoFire()
	{
		// �ȴ���ȴʱ��
		yield return new WaitForSeconds(autoFireCD);
		autoDO = false;
		
		// ���������Զ������ť
		GameObject[] btnAutoFireObj = GameObject.FindGameObjectsWithTag("btnAutoFire");
		
		// ���÷Ǽ���״̬�İ�ťλ�ú�UI״̬
		for (int i = 0; i < btnAutoFireObj.Length; i++)
		{
			if (!btnAutoFireObj[i].GetComponent<btncontrol>().autoDO)
			{
				btnAutoFireObj[i].transform.localPosition = new Vector3(95f, 0f, 0f);
				btnAutoFireObj[i].transform.parent.Find("timeText").gameObject.SetActive(true);
			}
		}
	}

	
	public void OnPointerUp(PointerEventData eventData)
	{
		// �ӿ�ʵ�ֱ���������
	}

	
	public void OnPointerExit(PointerEventData eventData)
	{
		// �ӿ�ʵ�ֱ���������
	}
}