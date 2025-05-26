using UnityEngine;

public static class Util
{
	public static T GetChildControl<T>(this GameObject _obj, string _target) where T : Component
	{
		Transform transform = _obj.transform.Find(_target);
		if (transform != null)
		{
			return transform.GetComponent<T>();
		}
		return (T)null;
	}
}
