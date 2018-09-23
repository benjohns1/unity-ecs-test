using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class Bootstrap
{
    public static GameSettings Settings;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void InitializeAfterSceneLoad()
    {
        if (!InitializeWithScene())
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    private static void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        InitializeWithScene();
    }

    public static bool InitializeWithScene()
    {
        GameSettingsComponent settingsComponent = GameObject.FindObjectOfType<GameSettingsComponent>();
        if (settingsComponent == null)
        {
            return false;
        }

        Settings = settingsComponent?.GameSettings;
        return true;
    }
}