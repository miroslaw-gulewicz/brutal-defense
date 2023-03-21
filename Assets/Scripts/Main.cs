using UnityEngine;

public static class Main
{
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	public static void Execute()
	{
		Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("Systems")));
	}
}