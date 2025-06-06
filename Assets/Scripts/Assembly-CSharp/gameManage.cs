using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManage : MonoBehaviour
{
    // 角色对象数组
    public GameObject[] charObj;

    // 攻击对象数组
    public GameObject[] atkObj;

    // 攻击对象名称数组
    public string[] atkObjName;

    // 攻击对象部件数量数组
    public int[] atkObjPiece;

    // 武器图像数组
    public Sprite[] wqImage;

    // 自动攻击点数组
    public Transform[] autoFirePoint;

    // 自动攻击按钮
    public GameObject btn;

    // 自动攻击冷却时间
    public float autoFireCD;

    // 金币显示文本
    public Text coinText;

    // 金币计数
    [HideInInspector]
    public int coinCount;

    // 保存冷却时间
    public float saveCD = 5f;

    // 无钱UI提示
    public GameObject noMoneyUI;

    // 获得攻击对象UI提示
    public GameObject getAtkObjUI;

    // 部件对象数组
    public GameObject[] partsObj;

    // 金币动画控制器
    private Animator coinAni;

    // 时间临时变量
    private float timeTemp;

    // 头部动画控制器
    private Animator aniHead;

    // 是否开启自动攻击
    private bool openDO;

    // 初始化游戏管理器
    private void Start()
    {
        // 初始化头部动画控制器
        aniHead = GameObject.FindGameObjectWithTag("head").GetComponent<Animator>();

        // 初始化角色对象
        for (int i = 0; i < charObj.Length; i++)
        {
            charObj[i].SetActive(false);
        }
        int num = PlayerPrefs.GetInt("charNum");
        charObj[num].SetActive(true);

        // 初始化金币计数和显示
        coinCount = PlayerPrefs.GetInt("coin");
        coinText.text = string.Empty + coinCount;

        // 初始化攻击对象和相关按钮
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

        // 初始化部件对象
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

        // 初始化金币动画控制器
        coinAni = coinText.transform.parent.gameObject.GetComponent<Animator>();
    }

    // 固定更新，用于保存金币计数
    private void FixedUpdate()
    {
        if (Time.time > timeTemp)
        {
            timeTemp = Time.time + saveCD;
            PlayerPrefs.SetInt("coin", coinCount);
        }
    }

    // 增加金币
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

    // 攻击对象按钮事件
    public void btnAtkObj(int num)
    {
        GetComponent<control>().objNum = num;
    }

    // 角色选择按钮事件
    public void btnCharSelect()
    {
        SceneManager.LoadScene("charSelect");
    }

    // 自动攻击按钮事件
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

    // 获得攻击对象事件
    public void getAtkObj(int num)
    {
        getAtkObjUI.SetActive(true);
        getAtkObjUI.transform.Find("Image").gameObject.GetComponent<Image>().sprite = wqImage[num];
    }
}
