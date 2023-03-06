using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [Header("Main Menu")]
    [SerializeField]
    private UIDocument docMainMenu;
    public string sceneIntro;
    public string mainMenuScene;
    private VisualElement rootMainMenu;

    [Header("Settings Menu")]
    [SerializeField]
    private UIDocument docSettingsMenu;
    private VisualElement rootSettingsMenu;

    [Header("Credits Menu")]
    [SerializeField]
    private UIDocument docCreditMenu;
    private VisualElement rootCreditMenu;


    [Header("Play Menu")]
    [SerializeField]
    private UIDocument docPlayMenu;
    private VisualElement rootPlayMenu;

    private VisualElement currentVisualElement;
    private UIDocument lastMenuCheck;
    [SerializeField] private PlayerInput playerInput;
    private bool activeMenuGame;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        if (docMainMenu) rootMainMenu = docMainMenu.rootVisualElement;
        else Debug.LogError("NO MAIN MENU REFERENCE");

        if (docSettingsMenu) rootSettingsMenu = docSettingsMenu.rootVisualElement;
        else Debug.LogError("NO SETTINGS MENU REFERENCE !");

        if (docCreditMenu) rootCreditMenu = docCreditMenu.rootVisualElement;
        else Debug.LogError("NO Credits MENU REFERENCE !");

        if (docPlayMenu) rootPlayMenu = docPlayMenu.rootVisualElement;
        else Debug.LogError("NO Play MENU REFERENCE !");

        SetMainMenu();
        SetOptionsMenu();
        SetCreditMenu();
        SetPlayMenu();

        docMainMenu.rootVisualElement.style.display = DisplayStyle.Flex;
        docSettingsMenu.rootVisualElement.style.display = DisplayStyle.None;
        docCreditMenu.rootVisualElement.style.display = DisplayStyle.None;
        docPlayMenu.rootVisualElement.style.display = DisplayStyle.None;


        SceneManager.LoadScene(mainMenuScene, LoadSceneMode.Additive);
    }

    private void Update()
    {
        EnableMenu();
    }

    /// <summary>
    /// Set the main menu settings
    /// </summary>
    private void SetMainMenu()
    {
        if (rootMainMenu == null) Debug.LogError("Can't set the main menu, null ref");

        Button newGameButton = rootMainMenu.Q<Button>("NewGame");
        Button optionButton = rootMainMenu.Q<Button>("Options");
        Button creditsButton = rootMainMenu.Q<Button>("Credits");
        Button leaveButton = rootMainMenu.Q<Button>("Leave");

        newGameButton.clickable.clicked += () => { LauchGame(); };
        optionButton.clickable.clicked += () => { EnableMenu(docSettingsMenu, docMainMenu); };
        creditsButton.clickable.clicked += () => { EnableMenu(docCreditMenu, docMainMenu); };
        leaveButton.clickable.clicked += LeaveGame;


        Debug.Log("Menu Set");
    }

    /// <summary>
    /// Set the option menu for button and activate input
    /// </summary>
    private void SetOptionsMenu()
    {
        if (rootSettingsMenu == null) Debug.LogError("Can't set the settings menu, null ref");

        Button settingsButton = rootSettingsMenu.Q<Button>("Settings");
        Button audioButton = rootSettingsMenu.Q<Button>("Audio");
        Button videoButton = rootSettingsMenu.Q<Button>("Video");
        Button manetteButton = rootSettingsMenu.Q<Button>("Manette");
        Button clavierButton = rootSettingsMenu.Q<Button>("Clavier");
        Button exitButton = rootSettingsMenu.Q<Button>("Leave");

        VisualElement settingVisual = rootSettingsMenu.Q<VisualElement>("GameSetting");
        VisualElement audioVisual = rootSettingsMenu.Q<VisualElement>("AudioSetting");
        VisualElement videoVisual = rootSettingsMenu.Q<VisualElement>("VideoSetting");
        VisualElement manetteVisual = rootSettingsMenu.Q<VisualElement>("ManetteSetting");
        VisualElement clavierSetting = rootSettingsMenu.Q<VisualElement>("ClavierSetting");

        currentVisualElement = settingVisual;

        settingsButton.clickable.clicked += () => { EnableVisualElement(settingVisual); };
        audioButton.clickable.clicked += () => { EnableVisualElement(audioVisual); };
        videoButton.clickable.clicked += () => { EnableVisualElement(videoVisual); };
        manetteButton.clickable.clicked += () => { EnableVisualElement(manetteVisual); };
        clavierButton.clickable.clicked += () => { EnableVisualElement(clavierSetting); };
        exitButton.clickable.clicked += () => { EnableMenu(lastMenuCheck, docSettingsMenu); };

        Debug.Log("Option menu Set");
    }

    /// <summary>
    /// Set the settings menu for button and activate input
    /// </summary>
    private void SetCreditMenu()
    {
        if (rootCreditMenu == null) Debug.LogError("Can't set the credit menu, null ref");

        Button leaveButton = rootCreditMenu.Q<Button>("Leave");
        leaveButton.clickable.clicked += () => { EnableMenu(docMainMenu, docCreditMenu); };

        Debug.Log("Credit menu Set");
    }

    /// <summary>
    /// Set the play menu of the game
    /// </summary>
    private void SetPlayMenu()
    {
        if (rootPlayMenu == null) Debug.LogError("Can't set the play  menu, null ref");

        Button resumeButton = rootPlayMenu.Q<Button>("Resume");
        Button optionButton = rootPlayMenu.Q<Button>("Options");
        Button leaveButton = rootPlayMenu.Q<Button>("Leave");

        resumeButton.clickable.clicked += () => { EnableMenu(null, docPlayMenu); Time.timeScale = 1; activeMenuGame = true; };
        optionButton.clickable.clicked += () => { EnableMenu(docSettingsMenu, docPlayMenu); };
        leaveButton.clickable.clicked += () => { EnableMenu(docMainMenu, docPlayMenu); };

        Debug.Log("Play Menu Set");
    }

    #region MainMenuVoid

    /// <summary>
    /// Lauch the main game
    /// </summary>
    private void LauchGame()
    {
        Debug.Log("Game Lauch");
        SceneManager.UnloadSceneAsync(mainMenuScene);

        EnableMenu(null, docMainMenu);
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneIntro, LoadSceneMode.Additive);
    }

    /// <summary>
    /// Leave the game
    /// </summary>
    private void LeaveGame()
    {
        Debug.Log("You leave the game");
        Application.Quit();
    }

    #endregion


    /// <summary>
    /// Enable or disable a UI Menu 
    /// </summary>
    /// <param name="docToActivate">The UI Doc to activate</param>
    /// <param name="docToDisable">The UI Doc to disable</param>
    private void EnableMenu(UIDocument docToActivate, UIDocument docToDisable)
    {
        if (docToActivate != null)
        {
            Debug.Log("Enable menu : " + docToActivate.gameObject.name);
            docToActivate.rootVisualElement.style.display = DisplayStyle.Flex;
        }

        if (docToDisable != null)
        {
            docToDisable.rootVisualElement.style.display = DisplayStyle.None;
            lastMenuCheck = docToDisable;
        }
    }


    /// <summary>
    /// Enable or disable a Visual Element
    /// </summary>
    /// <param name="visualElementToActivate"></param>
    /// <param name="visualElementToDisable"></param>
    private void EnableVisualElement(VisualElement visualElementToActivate)
    {
        if (currentVisualElement != null)
        {
            currentVisualElement.style.display = DisplayStyle.None;
            Debug.Log("Disable Visual : " + currentVisualElement.name);
        }

        Debug.Log("Enable Visual : " + visualElementToActivate.name);
        visualElementToActivate.style.display = DisplayStyle.Flex;
        currentVisualElement = visualElementToActivate;
    }

    private void EnableMenu()
    {
        if (playerInput.CanMenu)
        {
            if (activeMenuGame)
            {
                Time.timeScale = 0;
                EnableMenu(docPlayMenu, null);
                activeMenuGame = false;
            }
            else
            {
                Time.timeScale = 1;
                EnableMenu(null, docPlayMenu);
                activeMenuGame = true;
            }

            playerInput.CanMenu = false;
        }
    }
}