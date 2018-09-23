using UnityEngine;

/**
 * Attach this singleton to any game object in your initial scene
 */
public class GameSettingsComponent : MonoBehaviour
{
    private static GameSettingsComponent instance;
    public static GameSettingsComponent Instance
    {
        get { return instance; }
    }
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            if (RemoveAfterLoad)
            {
                Destroy(gameObject);
            }
            return;
        }

        Debug.LogError("Error: multiple GameSettingsComponent scripts exist in scene");
    }

    [SerializeField] private GameSettings gameSettings;
    public GameSettings GameSettings
    {
        get { return gameSettings; }
    }

    /**
     * Remove parent game object from the scene after the settings have been loaded
     */
    public bool RemoveAfterLoad = true;
}
