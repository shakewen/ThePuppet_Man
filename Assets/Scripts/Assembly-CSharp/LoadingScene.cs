using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
	public string mainScene;

	private void Start()
	{
		SceneManager.LoadSceneAsync(mainScene);
	}

	
}
