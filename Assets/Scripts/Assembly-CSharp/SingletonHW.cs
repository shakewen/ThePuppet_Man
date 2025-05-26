public class SingletonHW<T> : ISingleton where T : ISingleton, new()
{
	private static T _instance;

	public static T Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new T();
			}
			return _instance;
		}
	}
}
