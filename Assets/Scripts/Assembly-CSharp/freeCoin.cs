using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 管理免费金币奖励功能的 MonoBehaviour 子类
/// 处理奖励广告展示、金币数量更新及UI状态同步
/// </summary>
public class freeCoin : MonoBehaviour
{
    /// <summary>
    /// 用于显示当前金币数量的文本组件数组
    /// </summary>
    public Text[] coinText;

    /// <summary>
    /// 金币在PlayerPrefs中的存储键名，默认值为"coin"
    /// </summary>
    public string coinName = "coin";

    /// <summary>
    /// 每次奖励广告成功后增加的金币数量
    /// </summary>
    public int freeCoinCount;

    /// <summary>
    /// 奖励广告按钮的GameObject数组
    /// </summary>
    public GameObject[] freeCoinBtn;

    /// <summary>
    /// 奖励按钮上的数量显示文本组件数组
    /// </summary>
    public Text[] freeCoinText;

    /// <summary>
    /// Unity生命周期方法：在游戏对象初始化时调用
    /// 初始化奖励按钮的文本显示和激活状态
    /// </summary>
    private void Start()
    {
        // 初始化所有奖励按钮的状态
        for (int i = 0; i < freeCoinBtn.Length; i++)
        {
            // 设置按钮文本（如果存在）
            if ((bool)freeCoinText[i])
            {
                freeCoinText[i].text = "+" + freeCoinCount;
            }
            // 根据广告准备状态设置按钮激活状态
            if ((bool)freeCoinBtn[i])
            {
                //freeCoinBtn[i].SetActive(SingletonHW<AdManager>.Instance.IsReadyReward());
                freeCoinBtn[i].SetActive(true);
            }
        }
    }

    /// <summary>
    /// 动态更新奖励按钮的激活状态
    /// 用于广告状态变化时刷新UI表现
    /// </summary>
    public void freeCoinCheck()
    {
        // 遍历所有奖励按钮
        for (int i = 0; i < freeCoinBtn.Length; i++)
        {
            // 根据广告准备状态更新按钮激活状态
            if ((bool)freeCoinBtn[i])
            {
                //freeCoinBtn[i].SetActive(SingletonHW<AdManager>.Instance.IsReadyReward());
                freeCoinBtn[i].SetActive(true);
            }
        }
    }

    /// <summary>
    /// 请求展示奖励广告
    /// 调用广告管理器的展示方法并绑定回调函数
    /// </summary>
    public void ShowRewardAD()
    {
        //SingletonHW<AdManager>.Instance.ShowReward(Success, Fail);
        AdManager1.instance.ShowVideo(() =>
        {
            Success("");
        });
    }

    /// <summary>
    /// 奖励广告成功播放后的回调方法
    /// 增加金币数量并同步更新所有相关UI组件
    /// </summary>
    /// <param name="str">广告平台返回的状态字符串</param>
    public void Success(string str)
    {
        // 计算新的金币数量
        int num = PlayerPrefs.GetInt(coinName) + freeCoinCount;
        PlayerPrefs.SetInt(coinName, num);

        // 更新所有金币显示文本
        for (int i = 0; i < coinText.Length; i++)
        {
            if ((bool)coinText[i])
            {
                coinText[i].text = string.Empty + num;
            }
        }

        // 刷新奖励按钮状态
        freeCoinCheck();

        // 获取游戏管理器组件并更新金币计数
        gameManage_charSelect component = GameObject.FindGameObjectWithTag("gameManage").GetComponent<gameManage_charSelect>();
        if ((bool)component)
        {
            component.coinCount = num;
            return;
        }

        // 回退到基础游戏管理器组件类型
        gameManage component2 = GameObject.FindGameObjectWithTag("gameManage").GetComponent<gameManage>();
        if ((bool)component2)
        {
            component2.coinCount = num;
        }
    }

    /// <summary>
    /// 奖励广告播放失败的回调方法
    /// 当前未实现具体失败处理逻辑
    /// </summary>
    public void Fail()
    {
    }
}