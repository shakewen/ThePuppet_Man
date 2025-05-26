using System;
using System.Collections.Generic;
using UnityEngine;

namespace Umeng
{
	public class Analytics
	{
		private static class SingletonHolder
		{
			public static AndroidJavaClass instance_mobclick;

			public static AndroidJavaObject instance_context;

			static SingletonHolder()
			{
				instance_mobclick = new AndroidJavaClass("com.umeng.analytics.game.UMGameAgent");
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
				{
					instance_context = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
				}
			}
		}

		private static string version = "2.3";

		private static bool hasInit;

		private static string _AppKey;

		private static string _ChannelId;

		private static AndroidJavaClass _UpdateAgent;

		public static string AppKey
		{
			get
			{
				return _AppKey;
			}
		}

		public static string ChannelId
		{
			get
			{
				return _ChannelId;
			}
		}

		protected static AndroidJavaClass Agent
		{
			get
			{
				return SingletonHolder.instance_mobclick;
			}
		}

		protected static AndroidJavaClass UpdateAgent
		{
			get
			{
				if (_UpdateAgent == null)
				{
					_UpdateAgent = new AndroidJavaClass("com.umeng.update.UmengUpdateAgent");
				}
				return _UpdateAgent;
			}
		}

		protected static AndroidJavaObject Context
		{
			get
			{
				return SingletonHolder.instance_context;
			}
		}

		public static void StartWithAppKeyAndChannelId(string appKey, string channelId)
		{
			_AppKey = appKey;
			_ChannelId = channelId;
			UMGameAgentInit();
			if (!hasInit)
			{
				onResume();
				CreateUmengManger();
				hasInit = true;
			}
		}

		public static void SetLogEnabled(bool value)
		{
			Agent.CallStatic("setDebugMode", value);
		}

		public static void Event(string eventId)
		{
			Agent.CallStatic("onEvent", Context, eventId);
		}

		public static void Event(string eventId, string label)
		{
			Agent.CallStatic("onEvent", Context, eventId, label);
		}

		public static void Event(string eventId, Dictionary<string, string> attributes)
		{
			Agent.CallStatic("onEvent", Context, eventId, ToJavaHashMap(attributes));
		}

		public static void PageBegin(string pageName)
		{
			Agent.CallStatic("onPageStart", pageName);
		}

		public static void PageEnd(string pageName)
		{
			Agent.CallStatic("onPageEnd", pageName);
		}

		public static void Event(string eventId, Dictionary<string, string> attributes, int value)
		{
			try
			{
				if (attributes == null)
				{
					attributes = new Dictionary<string, string>();
				}
				if (attributes.ContainsKey("__ct__"))
				{
					attributes["__ct__"] = value.ToString();
					Event(eventId, attributes);
				}
				else
				{
					attributes.Add("__ct__", value.ToString());
					Event(eventId, attributes);
					attributes.Remove("__ct__");
				}
			}
			catch (Exception)
			{
			}
		}

		public static string GetDeviceInfo()
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.umeng.analytics.UnityUtil");
			return androidJavaClass.CallStatic<string>("getDeviceInfo", new object[1] { Context });
		}

		public static void SetLogEncryptEnabled(bool value)
		{
			Agent.CallStatic("enableEncrypt", value);
		}

		public static void SetLatency(int value)
		{
			Agent.CallStatic("setLatencyWindow", (long)value);
		}

		public static void Event(string[] keyPath, int value, string label)
		{
			string text = string.Join(";=umengUnity=;", keyPath);
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.umeng.analytics.UnityUtil");
			androidJavaClass.CallStatic("onEventForUnity", Context, text, value, label);
		}

		public static void SetContinueSessionMillis(long milliseconds)
		{
			Agent.CallStatic("setSessionContinueMillis", milliseconds);
		}

		[Obsolete("Flush")]
		public static void Flush()
		{
			Agent.CallStatic("flush", Context);
		}

		[Obsolete("SetEnableLocation已弃用")]
		public static void SetEnableLocation(bool reportLocation)
		{
			Agent.CallStatic("setAutoLocation", reportLocation);
		}

		public static void EnableActivityDurationTrack(bool isTraceActivity)
		{
			Agent.CallStatic("openActivityDurationTrack", isTraceActivity);
		}

		public static void SetCheckDevice(bool value)
		{
			Agent.CallStatic("setCheckDevice", value);
		}

		private static void CreateUmengManger()
		{
			GameObject gameObject = new GameObject();
			gameObject.AddComponent<UmengManager>();
			gameObject.name = "UmengManager";
		}

		public static void onResume()
		{
			Agent.CallStatic("onResume", Context);
		}

		public static void onPause()
		{
			Agent.CallStatic("onPause", Context);
		}

		public static void onKillProcess()
		{
			Agent.CallStatic("onKillProcess", Context);
		}

		private static AndroidJavaObject ToJavaHashMap(Dictionary<string, string> dic)
		{
			AndroidJavaObject androidJavaObject = new AndroidJavaObject("java.util.HashMap");
			IntPtr methodID = AndroidJNIHelper.GetMethodID(androidJavaObject.GetRawClass(), "put", "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");
			object[] array = new object[2];
			foreach (KeyValuePair<string, string> item in dic)
			{
				using (AndroidJavaObject androidJavaObject2 = new AndroidJavaObject("java.lang.String", item.Key))
				{
					using (AndroidJavaObject androidJavaObject3 = new AndroidJavaObject("java.lang.String", item.Value))
					{
						array[0] = androidJavaObject2;
						array[1] = androidJavaObject3;
						AndroidJNI.CallObjectMethod(androidJavaObject.GetRawObject(), methodID, AndroidJNIHelper.CreateJNIArgArray(array));
					}
				}
			}
			return androidJavaObject;
		}

		public static void UMGameAgentInit()
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.umeng.analytics.UnityUtil");
			androidJavaClass.CallStatic("initUnity", Context, AppKey, ChannelId, version);
		}

		public void Dispose()
		{
			Agent.Dispose();
			Context.Dispose();
		}
	}
}
