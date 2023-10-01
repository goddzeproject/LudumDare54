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

    public GameObject FOVHelp;
    public Collider WinBox;
    public GameObject Player;

    public ParticleSystem[] VFX;

    public TextMeshProUGUI CdBlastText;
    public TextMeshProUGUI CdLocatorText;

    public TextMeshProUGUI TutorialText;

    private PsiBlastLogic psiBlastLogic;
    private PsiLocatorLogic psiLocatorLogic;

    public Image HUD;
    private Tween HudFade;

    public Image Menu;

    public Transform[] AllObjects;
    private Vector3[] startPropsPositions;

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

        startPropsPositions = new Vector3[AllObjects.Length];
        for (int i = 0; i < AllObjects.Length; i++)
        {
            startPropsPositions[i] = AllObjects[i].position;
        }

        foreach (var item in VFX)
        {
            item.Stop();
        }

        psiBlastLogic = Player.GetComponent<PsiBlastLogic>();
        psiLocatorLogic = Player.GetComponent<PsiLocatorLogic>();
        HUD.color = Color.black;
        HudFade = HUD.DOFade(0f, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        CdBlastText.text = psiBlastLogic.timeLeft.ToString("F2");
        CdLocatorText.text = psiLocatorLogic.CdLocator.ToString("F2");
    }


    public void ChekWin()
    {
        //Zaderjka i vizov Okna Pobedi
        Debug.Log("WIN");

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex+1 != 4)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
            SceneManager.LoadScene(0);

    }

    public void ChekLose()
    {
        Menu.gameObject.SetActive(true);
        Menu.DOFade(1, 3f);
        Time.timeScale = 0f;
        Debug.Log("Lose");
    }

    public void PlayVFX(int number)
    {
        VFX[number].Play();
    }

    public void ShowCDBlast()
    {
        if (!psiBlastLogic.isCDActive)
        {
            CdBlastText.alpha = 1f;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(CdBlastText.rectTransform.parent as RectTransform, inputManager.mousePosition, null, out Vector2 localMousePosition);
            CdBlastText.rectTransform.localPosition = localMousePosition;
            CdBlastText.DOFade(0f, 0.7f);
        }
    }
    public void ShowCDLocator()
    {
        if (!psiLocatorLogic.isCDActive)
        {
            CdLocatorText.alpha = 1f;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(CdBlastText.rectTransform.parent as RectTransform, inputManager.mousePosition, null, out Vector2 localMousePosition);
            CdLocatorText.rectTransform.localPosition = localMousePosition;
            CdLocatorText.DOFade(0f, 0.7f);
        }
    }

    public void Restart()
    {
        Menu.color = Color.black;
        Menu.gameObject.SetActive(false);
        Time.timeScale = 1f;

        HudFade.Kill();
        HUD.color = Color.black;
        HudFade = HUD.DOFade(0f, 3f);
        Player.GetComponent<HealthSystem>().health = 100;
        Player.GetComponent<HealthSystem>().GlassBankAnim();
        for (int i = 0; i < AllObjects.Length; i++)
        {
            Rigidbody rb = AllObjects[i].GetComponent<Rigidbody>();
            rb.Sleep();
            AllObjects[i].position = startPropsPositions[i];
        }
    }
}
