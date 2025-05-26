using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 按钮控制器类，负责处理攻击按钮、自动射击按钮和解锁按钮的交互逻辑
/// 实现IPointerDownHandler接口处理指针按下事件
/// </summary>
public class btncontrol : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IEventSystemHandler
{
	/// <summary>
	/// 攻击对象编号（0-普通攻击，5-特殊攻击）
	/// 用于区分不同攻击类型的标识符
	/// </summary>
	[HideInInspector]
	public int atkObjNum;

	/// <summary>
	/// 自动射击冷却时间（秒）
	/// 控制自动射击功能的冷却间隔
	/// </summary>
	[HideInInspector]
	public float autoFireCD;

	/// <summary>
	/// 解锁所需碎片数量
	/// 解锁新攻击类型所需的货币数量
	/// </summary>
	[HideInInspector]
	public int unlockPiece;

	/// <summary>
	/// 自动射击激活标志
	/// 指示当前是否处于自动射击状态
	/// </summary>
	[HideInInspector]
	public bool autoDO;

	// 私有变量声明区域
	private float lastTime;        // 上次触发自动射击的时间
	private float nowTime;         // 当前时间（未使用）
	private float textTime;        // 剩余冷却时间显示值
	private Text timeText;         // 冷却时间文本组件
	private gameManage gm;         // 全局游戏管理器引用
	private freeCoin_btnAtkObj freeCoin; // 免费货币组件引用
	private Animator freeCoinTis;  // 免费货币提示动画器

	/// <summary>
	/// 初始化方法
	/// 获取必要组件并根据游戏状态初始化按钮状态
	/// </summary>
	private void Start()
	{
		// 获取全局管理组件
		gm = GameObject.FindGameObjectWithTag("gameManage").GetComponent<gameManage>();

		freeCoin = GameObject.FindGameObjectWithTag("gameManage").GetComponent<freeCoin_btnAtkObj>();
		//一个文本
		freeCoinTis = GameObject.FindGameObjectWithTag("freeCoinTis").GetComponent<Animator>();
		
		// 获取冷却时间文本组件
		timeText = base.transform.parent.Find("timeText").GetComponent<Text>();
		
		// 根据攻击类型和按钮名称设置初始状态
		if ((atkObjNum == 0 || atkObjNum == 5) && base.name == "btnAutoFire")
		{
			base.gameObject.SetActive(false);
			timeText.gameObject.SetActive(false);
		}
		
		// 设置攻击按钮颜色标识
		if (atkObjNum == 0 && base.name == "btnAtkObj")
		{
			GetComponent<Image>().color = new Color(0.32f, 1f, 0f);
		}
		
		// 处理解锁按钮初始状态
		if (atkObjNum == 0 && base.name == "btnUnlock")
		{
			base.gameObject.SetActive(false);
		}
		
		// 根据玩家偏好设置更新UI状态
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
	/// 固定更新方法
	/// 每帧更新冷却时间显示
	/// </summary>
	private void FixedUpdate()
	{
		// 计算剩余冷却时间
		textTime = lastTime + autoFireCD - Time.time;
		
		// 格式化时间显示（分:秒）
		timeText.text = string.Empty + Mathf.Floor(textTime / 60f) + ":" + Mathf.Floor(textTime - Mathf.Floor(textTime / 60f) * 60f);
		
		// 处理冷却结束情况
		if (textTime < 0f)
		{
			timeText.text = "0:0";
		}
	}

	/// <summary>
	/// 指针按下事件处理
	/// 根据不同按钮类型执行对应操作
	/// </summary>
	/// <param name="eventData">包含事件数据的PointerEventData对象</param>
	public void OnPointerDown(PointerEventData eventData)
	{
		if (base.name == "btnAtkObj")
		{
			// 切换攻击模式
			gm.btnAtkObj(atkObjNum);
			
			// 更新所有攻击按钮的视觉状态
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
			//	// 启动自动射击流程
			//	freeCoin.ShowRewardAD();
			//	autoDO = true;
			//	gm.btnAutoFire(atkObjNum);
			//	base.transform.localPosition = new Vector3(-2000f, 0f, 0f);
			//	StartCoroutine(waitAutoFire());
			//	lastTime = Time.time;
			//}
			//else
			//{
			//	// 触发免费货币提示动画
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
				// 执行解锁操作
				gm.coinCount -= unlockPiece;
				gm.coinText.text = string.Empty + gm.coinCount;
				PlayerPrefs.SetInt("coin", gm.coinCount);
				PlayerPrefs.SetInt("atkObj" + atkObjNum, 1);
				base.gameObject.SetActive(false);
				
				// 更新UI状态
				if (atkObjNum != 5)
				{
					base.transform.parent.Find("btnAutoFire").gameObject.SetActive(true);
					base.transform.parent.Find("timeText").gameObject.SetActive(true);
				}
				
				// 触发攻击对象获取和切换
				gm.getAtkObj(atkObjNum);
				gm.btnAtkObj(atkObjNum);
				
				// 更新按钮视觉状态
				GameObject[] array2 = GameObject.FindGameObjectsWithTag("btnAtkObj");
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j].GetComponent<Image>().color = new Color(1f, 1f, 1f);
				}
				base.transform.parent.Find("btnAtkObj").gameObject.GetComponent<Image>().color = new Color(0.32f, 1f, 0f);
			}
			else
			{
				// 显示资金不足提示
				gm.noMoneyUI.SetActive(true);
				GameObject.FindGameObjectWithTag("gameManage").GetComponent<freeCoin>().freeCoinCheck();
			}
		}
	}

	/// <summary>
	/// 自动射击冷却协程
	/// 等待指定时间后重置按钮状态
	/// </summary>
	/// <returns>IEnumerator对象用于协程迭代</returns>
	private IEnumerator waitAutoFire()
	{
		// 等待冷却时间
		yield return new WaitForSeconds(autoFireCD);
		autoDO = false;
		
		// 查找所有自动射击按钮
		GameObject[] btnAutoFireObj = GameObject.FindGameObjectsWithTag("btnAutoFire");
		
		// 重置非激活状态的按钮位置和UI状态
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
		// 接口实现保留方法体
	}

	
	public void OnPointerExit(PointerEventData eventData)
	{
		// 接口实现保留方法体
	}
}