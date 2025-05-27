using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
public class AdManager1 : MonoBehaviour
{
    /// <summary>
    /// 是否是屏蔽区域—默认被屏蔽，白包
    /// </summary>
    public bool ShieldAddress = true;
   
    public static AdManager1 instance;
    //public static bool isyuanshengshow;

    public UnityAction actionCallBack;

    public GameObject hideBtn;
    public GameObject moreBtn;
    public GameObject quitBtn;
    public GameObject kefuBtn;
    private enum Platform
    {
        NONE,
        VIVO,
        OPPO,
        HUAWEI,
        HONOR,
        XIAOMI
    }
    private Platform currentPlatform;
    static AdManager1()
    {
        //初始化
        GameObject go = new GameObject("AndroidObj");
        DontDestroyOnLoad(go);
        instance=go.AddComponent<AdManager1>();
    }
    public void AddBtn(GameObject HideBtn, GameObject MoreBtn, GameObject QuitBtn, GameObject KefuBtn)
    {
        hideBtn = HideBtn;
        moreBtn = MoreBtn;
        quitBtn = QuitBtn;
        kefuBtn = KefuBtn;
        GetPlatform();
    }

    public void GetPlatform()
    {
#if UNITY_ANDROID && !UNITY_EDITOR && !EVALUATING
using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        					   {
        					    using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
        					     {
        					        jo.Call("JudgeChannel");
        					     }
        					  } 
        
#endif
    }

    public void SetPlatform(string str)
    {
        Debug.Log($"当前平台是: {str}");
        Platform platform;
        if (Enum.TryParse(str, out platform))
        {
            switch (platform)
            {
                case Platform.VIVO:
                    currentPlatform = Platform.VIVO;
                    hideBtn.SetActive(true);
                    moreBtn.SetActive(false);
                    quitBtn.SetActive(false);
                    kefuBtn.SetActive(true);
                    break;
                case Platform.OPPO:
                    currentPlatform = Platform.OPPO;
                    hideBtn.SetActive(true);
                    moreBtn.SetActive(true);
                    quitBtn.SetActive(false);
                    kefuBtn.SetActive(true);
                    break;
                case Platform.HUAWEI:
                    currentPlatform = Platform.HUAWEI;
                    hideBtn.SetActive(true);
                    moreBtn.SetActive(false);
                    quitBtn.SetActive(true);
                    kefuBtn.SetActive(false);
                    break;
                case Platform.HONOR:
                    currentPlatform = Platform.HONOR;
                    hideBtn.SetActive(true);
                    moreBtn.SetActive(false);
                    quitBtn.SetActive(false);
                    kefuBtn.SetActive(false);
                    break;
                case Platform.XIAOMI:
                    currentPlatform = Platform.XIAOMI;
                    hideBtn.SetActive(true);
                    moreBtn.SetActive(false);
                    quitBtn.SetActive(false);
                    kefuBtn.SetActive(false);
                    break;
                default://白包
                    currentPlatform = Platform.NONE;
                    hideBtn.SetActive(false);
                    moreBtn.SetActive(false);
                    quitBtn.SetActive(false);
                    kefuBtn.SetActive(false);
                    break;
            }
        }
    }
    /// <summary>
    /// 退出游戏
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ShowVideo(UnityAction callback)
    {
        actionCallBack = null;
        actionCallBack = callback;
#if UNITY_EDITOR
        Debug.Log("弹出了视频广告!!");
        callback();
        return;
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
        					   using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        					   {
        					    using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
        					     {
        					        jo.Call("GameVideo");
        					     }
        					  } 
#endif


    }
    public void GameVideoCallBack()
    {
        actionCallBack.Invoke();
    }

    public void KeFu()
    {
        Debug.Log("联系客服");
#if UNITY_ANDROID && !UNITY_EDITOR
        					   using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        					   {
        					    using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
        					     {
        					        jo.Call("KeFu");
        					     }
        					  } 
#endif
    }
    public void More()
    {
        Debug.Log("更多精彩");
#if UNITY_ANDROID && !UNITY_EDITOR
        					   using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        					   {
        					    using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
        					     {
        					        jo.Call("More");
        					     }
        					  } 
#endif
    }


    public void YinSi()
    {
        Debug.Log("隐私政策");
#if UNITY_ANDROID && !UNITY_EDITOR
        					   using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        					   {
        					    using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
        					     {
        					        jo.Call("YinSi");
        					     }
        					  } 
#endif
    }
    /// <summary>
    /// 原生
    /// </summary>
    /// <param name="isshielding">是否屏蔽</param>
    public void ShowYuanSheng(bool isSheild)
    {
#if UNITY_EDITOR
        Debug.Log("展示原生广告");
#endif
        if (isSheild)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        					   using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        					   {
        					    using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
        					     {
        					        jo.Call("ChaPing");
        					     }
        					  } 
#endif
        }
        else
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        					   using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        					   {
        					    using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
        					     {
        					        jo.Call("OnPanelCha");
        					     }
        					  } 
#endif
        }



    }
 
    public void ShowYuanShengSelf()
    {
#if UNITY_EDITOR
        Debug.Log("弹出了原生广告!!");
        return;
#endif
        Debug.Log("原生模版调用");
      
    }
    public void HideYuanSheng()
    {
       
    }
   
   
    public void ShowBanner()
    {


    }
    public void AddTable()
    {
        
    }




}

