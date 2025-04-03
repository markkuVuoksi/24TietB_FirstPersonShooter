using UnityEngine;
using UnityEngine.SceneManagement;
public class SL_settingsManagement : MonoBehaviour
{

    private static SL_settingsManagement instance;
    private Canvas canvas;
    public GameObject mainMenuButton;
    public GameObject gameModePanel;
    public GameObject tutorialButton;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject); // Prevent multiple music players
        }
        canvas = GetComponent<Canvas>();
        // Ensure we subscribe to scene change event
        SceneManager.activeSceneChanged += OnSceneChanged;
    }
    // void OnEnable()
    // {
    //     SceneManager.sceneLoaded += OnSceneLoaded;
    // }

    // void OnDisable()
    // {
    //     SceneManager.sceneLoaded -= OnSceneLoaded;
    // }

    // void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     Debug.Log("Scene name : " + scene.name);
    //     // Enable Canvas only in Main Menu Scene
    //     if (scene.name == "SL_MainMenu")
    //     {
    //         canvas.gameObject.SetActive(true);
    //     }
    //     else
    //     {
    //         canvas.gameObject.SetActive(false);
    //     }
    // }

    void OnDestroy()
    {
        // Prevent memory leaks by unsubscribing
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }

    void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        Debug.Log("Scene name : " + newScene.name);
        // Activate Canvas 2 only in Scene 1 (change "Scene1" to your actual scene name)
        if (newScene.name == "SL_MainMenu")
        {
            canvas.gameObject.SetActive(true); // Make sure it's active
            mainMenuButton.SetActive(true);
            gameModePanel.SetActive(false);
            tutorialButton.SetActive(true);
        }
        else
        {
            canvas.gameObject.SetActive(false); // Disable in other scenes
            mainMenuButton.SetActive(false);
            gameModePanel.SetActive(false);
            tutorialButton.SetActive(false);
        }
    }
}
