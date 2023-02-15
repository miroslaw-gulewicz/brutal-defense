using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AutomatedBuild 
{
	public static string[] scenes = { "Assets/Scenes/SampleScene.unity" };

	[MenuItem("BrutalDefense/Build/All")]
	public static void BuildAll()
	{
		BuildWindows();
		BuildAndroid();
		//BuildiOS();
	}

		[MenuItem("BrutalDefense/Build/Windows (release)")]
	public static void BuildWindows()
	{
		Debug.Log("Starting Windows Build!");
		BuildPipeline.BuildPlayer(
			scenes,
			"Build/Windows/BrutalDefense.exe",
			BuildTarget.StandaloneWindows,
			BuildOptions.None);
	}

	[MenuItem("BrutalDefense/Build/Android (release)")]
	static void BuildAndroid()
	{
		Debug.Log("Starting Android Build!");

		/* We absolutely do not want to ever store secrets in code (or even add them to
		   version control), so instead we'll fetch them from the system environment.
		   Don't forget to set these environment variables before invoking the build script! *//*
		PlayerSettings.Android.keystorePass = Environment.GetEnvironmentVariable("KEY_STORE_PASS");
		PlayerSettings.Android.keyaliasPass = Environment.GetEnvironmentVariable("KEY_ALIAS_PASS");
*/
		BuildPipeline.BuildPlayer(
			scenes,
			"Build/Android/GAMENAME.aab",
			BuildTarget.Android,
			BuildOptions.None);
	}

/*	[MenuItem("BrutalDefense/Build/iOS (release)")]
	static void BuildiOS()
	{
		Debug.Log("Starting iOS Build!");
		BuildPipeline.BuildPlayer(
			scenes,
			"Build/iOS",
			BuildTarget.iOS,
			BuildOptions.AcceptExternalModificationsToPlayer);
	}*/
}
