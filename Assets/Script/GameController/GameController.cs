using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MEC;

public class GameController : MonoBehaviour 
{
	public static GameController Instance;

	public GameFlow GameFlow
	{
		get
		{
			return gameFlow;
		}

		set
		{
			gameFlow = value;
		}
	}

	[SerializeField]
	GameFlow gameFlow;

	[SerializeField]
	List<GameObject> UIs;

	[SerializeField][ReadOnly]
	GameFlowController gameFlowController;

//	[SerializeField]
//	Button resetBtn;

	const int GamePlayerSceneIndex = 1;
	const int TempSceneIndex = 2;

	// Use this for initialization
	public void Awake () 
	{
		Instance = this;
		DontDestroyOnLoad (this.gameObject);
		gameFlowController = new GameFlowController ();

		gameFlowController.Init ();
	}

	void Update()
	{
		float deltaTime = Time.deltaTime;
		gameFlowController.Stay (deltaTime);
	}

	public void Reset()
	{	
		UIs.ForEach (UI=>
			{
				UI.SetActive(true);
			});

		Timing.KillCoroutines ();

		SceneManager.LoadScene (TempSceneIndex);

		gameFlowController.ForceChangeState<StandbyState> ();
	}

	public IEnumerator<float> IntoPlaySceneCor ()
	{	
		UIs.ForEach (UI=>
			{
				UI.SetActive(false);
			});
		
		AsyncOperation loadAsyn = SceneManager.LoadSceneAsync (GamePlayerSceneIndex);

		while (!loadAsyn.isDone) 
		{
			yield return Timing.WaitForOneFrame;
		}

		yield return Timing.WaitForOneFrame;
	}

}
