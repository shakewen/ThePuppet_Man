using UnityEngine;

public class SingletonMonoBehavior<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T _Instance;

	public static T Instance
	{
		get
		{
			if (_Instance == null)
			{
				_Instance = Object.FindObjectOfType<T>();
				if (_Instance == null)
				{
					_Instance = new GameObject(typeof(T).Name).AddComponent<T>();
				}
			}
			return _Instance;
		}
	}

	protected virtual void Awake()
	{
		if (this != Instance)
		{
			GameObject obj = base.gameObject;
			Object.Destroy(this);
			Object.Destroy(obj);
			Debug.LogWarning("has instance destory " + typeof(T).Name);
		}
	}
}
