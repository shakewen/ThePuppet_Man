using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������ѽ�ҽ������ܵ� MonoBehaviour ����
/// ���������չʾ������������¼�UI״̬ͬ��
/// </summary>
public class freeCoin : MonoBehaviour
{
    /// <summary>
    /// ������ʾ��ǰ����������ı��������
    /// </summary>
    public Text[] coinText;

    /// <summary>
    /// �����PlayerPrefs�еĴ洢������Ĭ��ֵΪ"coin"
    /// </summary>
    public string coinName = "coin";

    /// <summary>
    /// ÿ�ν������ɹ������ӵĽ������
    /// </summary>
    public int freeCoinCount;

    /// <summary>
    /// ������水ť��GameObject����
    /// </summary>
    public GameObject[] freeCoinBtn;

    /// <summary>
    /// ������ť�ϵ�������ʾ�ı��������
    /// </summary>
    public Text[] freeCoinText;

    /// <summary>
    /// Unity�������ڷ���������Ϸ�����ʼ��ʱ����
    /// ��ʼ��������ť���ı���ʾ�ͼ���״̬
    /// </summary>
    private void Start()
    {
        // ��ʼ�����н�����ť��״̬
        for (int i = 0; i < freeCoinBtn.Length; i++)
        {
            // ���ð�ť�ı���������ڣ�
            if ((bool)freeCoinText[i])
            {
                freeCoinText[i].text = "+" + freeCoinCount;
            }
            // ���ݹ��׼��״̬���ð�ť����״̬
            if ((bool)freeCoinBtn[i])
            {
                //freeCoinBtn[i].SetActive(SingletonHW<AdManager>.Instance.IsReadyReward());
                freeCoinBtn[i].SetActive(true);
            }
        }
    }

    /// <summary>
    /// ��̬���½�����ť�ļ���״̬
    /// ���ڹ��״̬�仯ʱˢ��UI����
    /// </summary>
    public void freeCoinCheck()
    {
        // �������н�����ť
        for (int i = 0; i < freeCoinBtn.Length; i++)
        {
            // ���ݹ��׼��״̬���°�ť����״̬
            if ((bool)freeCoinBtn[i])
            {
                //freeCoinBtn[i].SetActive(SingletonHW<AdManager>.Instance.IsReadyReward());
                freeCoinBtn[i].SetActive(true);
            }
        }
    }

    /// <summary>
    /// ����չʾ�������
    /// ���ù���������չʾ�������󶨻ص�����
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
    /// �������ɹ����ź�Ļص�����
    /// ���ӽ��������ͬ�������������UI���
    /// </summary>
    /// <param name="str">���ƽ̨���ص�״̬�ַ���</param>
    public void Success(string str)
    {
        // �����µĽ������
        int num = PlayerPrefs.GetInt(coinName) + freeCoinCount;
        PlayerPrefs.SetInt(coinName, num);

        // �������н����ʾ�ı�
        for (int i = 0; i < coinText.Length; i++)
        {
            if ((bool)coinText[i])
            {
                coinText[i].text = string.Empty + num;
            }
        }

        // ˢ�½�����ť״̬
        freeCoinCheck();

        // ��ȡ��Ϸ��������������½�Ҽ���
        gameManage_charSelect component = GameObject.FindGameObjectWithTag("gameManage").GetComponent<gameManage_charSelect>();
        if ((bool)component)
        {
            component.coinCount = num;
            return;
        }

        // ���˵�������Ϸ�������������
        gameManage component2 = GameObject.FindGameObjectWithTag("gameManage").GetComponent<gameManage>();
        if ((bool)component2)
        {
            component2.coinCount = num;
        }
    }

    /// <summary>
    /// ������沥��ʧ�ܵĻص�����
    /// ��ǰδʵ�־���ʧ�ܴ����߼�
    /// </summary>
    public void Fail()
    {
    }
}