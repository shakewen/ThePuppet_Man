using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;


public class gameManage_charSelect : MonoBehaviour
{
	public Text coinText;

	public Text pieceText;

	public Text succesText;

	public GameObject[] partsObj;

	public Sprite[] partsIcon;

	public int[] pieceCount;

	public Text btnBuyText;

	public GameObject noMoneyUI;

	private int partsNum;

	[HideInInspector]
	public int coinCount;

	private freeCoin freeCoinScript;

	public GameObject hideBtn;
	public GameObject moreBtn;
    public GameObject quitBtn;
	public GameObject kefuBtn;

	


    private void Start()
	{
		//获取按钮
		AdManager1.instance.AddBtn(hideBtn, moreBtn, quitBtn, kefuBtn);

		if (PlayerPrefs.GetInt("coin") == 0)
		{
			PlayerPrefs.SetInt("coin", 500);
		}
		freeCoinScript = GameObject.FindGameObjectWithTag("gameManage").GetComponent<freeCoin>();
		coinCount = PlayerPrefs.GetInt("coin");
		coinText.text = string.Empty + coinCount;
		check();
	}
	
    
    public void btnPrevious()
	{
		//freeCoinScript.freeCoinCheck();
		partsNum--;
		if (partsNum < 0)
		{
			partsNum = partsObj.Length - 1;
		}
		check();
	}

	public void btnNext()
	{
		//freeCoinScript.freeCoinCheck();
		partsNum++;
		if (partsNum > partsObj.Length - 1)
		{
			partsNum = 0;
		}
		check();
	}

	public void btnPlay()
	{
       
		AdManager1.instance.ShowVideo(() =>
		{
            SceneManager.LoadScene("game");
        });
	}

	/// <summary>
/// 处理玩家购买零件的按钮点击事件
/// </summary>
/// <remarks>
/// 参数说明:
/// 无显式参数，通过类成员变量partsNum获取当前零件索引
/// 
/// 返回值:
/// 无返回值，操作结果通过UI反馈和PlayerPrefs持久化存储体现
/// 
/// 副作用:
/// 1. 修改PlayerPrefs中的硬币数量和零件拥有状态
/// 2. 操作多个UI元素的状态和文本显示
/// 3. 触发freeCoinScript的免费硬币检查逻辑
/// </remarks>
public void btnBuy()
{
	// 检查当前零件是否未被购买（PlayerPrefs中对应键值不为1）
	if (PlayerPrefs.GetInt("parts" + partsNum) != 1)
	{
		// 判断玩家硬币是否足够支付当前零件价格
		if (coinCount >= pieceCount[partsNum])
		{
			// 执行购买操作：
			// 1. 扣除对应硬币数量
			coinCount -= pieceCount[partsNum];
			// 2. 更新PlayerPrefs中的硬币记录
			PlayerPrefs.SetInt("coin", coinCount);
			// 3. 更新UI显示的硬币数量
			coinText.text = string.Empty + coinCount;
			// 4. 标记该零件为已购买状态
			PlayerPrefs.SetInt("parts" + partsNum, 1);
			// 5. 显示购买成功提示（通过短暂隐藏再显示实现刷新效果）
			succesText.text = "购买成功";
			succesText.gameObject.SetActive(false);
			succesText.gameObject.SetActive(true);
			// 6. 隐藏价格显示文本，更新购买按钮文字为"拥有"
			pieceText.gameObject.SetActive(false);
			btnBuyText.text = "拥有";
		}
		else
		{
			// 硬币不足时的操作：
			// 1. 显示缺钱提示UI
			noMoneyUI.SetActive(true);
			// 2. 触发免费硬币获取检查逻辑
			freeCoinScript.freeCoinCheck();
		}
	}
}

	private void check()
	{
		for (int i = 0; i < partsObj.Length; i++)
		{
			if (PlayerPrefs.GetInt("parts" + i) == 1)
			{
				partsObj[i].SetActive(true);
			}
			else if (PlayerPrefs.GetInt("parts" + i) == 0)
			{
				partsObj[i].SetActive(false);
			}
		}
		partsObj[partsNum].SetActive(true);
		pieceText.transform.parent.Find("Image").gameObject.GetComponent<Image>().sprite = partsIcon[partsNum];
		pieceText.text = string.Empty + pieceCount[partsNum];
		int num = PlayerPrefs.GetInt("parts" + partsNum);
		if (num == 1)
		{
			pieceText.gameObject.SetActive(false);
			btnBuyText.text = "拥有";
		}
		else
		{
			pieceText.gameObject.SetActive(true);
			btnBuyText.text = "购买";
		}
	}

	public void btnYinSi()
	{
		AdManager1.instance.YinSi();
	}

	public void btnZhuXiao()
	{
		AdManager1.instance.QuitGame();
	}

	public void btnMore()
	{
		AdManager1.instance.More();
	}

	public void btnKeFu()
    {
        AdManager1.instance.KeFu();
    }
}
