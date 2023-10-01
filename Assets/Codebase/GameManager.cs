using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private InputManager inputManager;
    private AudioManager audioManager;
    private UIManager uiManager;

    public GameObject FOVHelp;

    private GameObject Player;

    private GameObject WinBox;
    private ParticleSystem VFX;

    private TextMeshProUGUI CdBlastText;
    private TextMeshProUGUI CdLocatorText;

    private TextMeshProUGUI TutorialText;

    private PsiBlastLogic psiBlastLogic;
    private PsiLocatorLogic psiLocatorLogic;

    private Image HUD;
    private Tween HudFade;

    private Image Menu;


    // Start is called before the first frame update
    private void Awake()
    {
        FOVHelp.SetActive(false);

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        
    }
    void Start()
    {   
        inputManager = InputManager.instance;
        audioManager = AudioManager.instance;
        uiManager = UIManager.instance;

        SetReferencesUI();


    }

    // Update is called once per frame
    void Update()
    {
        CdBlastText.text = psiBlastLogic.timeLeft.ToString("F2");
        CdLocatorText.text = psiLocatorLogic.CdLocator.ToString("F2");
    }


    public void ChekWin()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
        //Zaderjka i vizov Okna Pobedi
        Debug.Log("WIN");

    }

    public void ChekLose()
    {
        Menu.gameObject.SetActive(true);
        Menu.DOFade(1, 3f);
        Time.timeScale = 0f;
        Debug.Log("Lose");
    }

    public void PlayVFX()
    {   
        if (Player == null)
        {
            WinBox = GameObject.FindWithTag("Finish");
            VFX = WinBox.GetComponent<ParticleSystem>();
        }
    
        VFX.Play();
    }

    public void ShowCDBlast()
    {
        if (psiBlastLogic.isCDActive)
        {
            CdBlastText.alpha = 1f;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(CdBlastText.rectTransform.parent as RectTransform, inputManager.mousePosition, null, out Vector2 localMousePosition);
            CdBlastText.rectTransform.localPosition = localMousePosition;
            CdBlastText.DOFade(0f, 0.7f);
        }
    }
    public void ShowCDLocator()
    {
        if (psiLocatorLogic.isCDActive)
        {
            CdLocatorText.alpha = 1f;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(CdBlastText.rectTransform.parent as RectTransform, inputManager.mousePosition, null, out Vector2 localMousePosition);
            CdLocatorText.rectTransform.localPosition = localMousePosition;
            CdLocatorText.DOFade(0f, 0.7f);
        }
    }

    public void Restart()
    {
        HudFade.Kill();
        Time.timeScale = 1f;

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

        SetReferencesUI();
    }

    public void SetReferencesUI()
    {
        CdBlastText = uiManager.CdBlastText;
        CdLocatorText = uiManager.CdLocatorText;
        TutorialText = uiManager.TutorialText;
        HUD = uiManager.HUD;
        Menu = uiManager.Menu;

        WinBox = GameObject.FindWithTag("Finish");
        WinBox.SetActive(true);
        VFX = WinBox.GetComponent<ParticleSystem>();
        VFX.Stop();


        Player = GameObject.FindWithTag("Player");
        psiBlastLogic = Player.GetComponent<PsiBlastLogic>();
        psiLocatorLogic = Player.GetComponent<PsiLocatorLogic>();

        HUD.color = Color.black;
        HudFade = HUD.DOFade(0f, 4f);
    }
}
