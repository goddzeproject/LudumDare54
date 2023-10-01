using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI CdBlastText;
    public TextMeshProUGUI CdLocatorText;

    public TextMeshProUGUI TutorialText;

    public Image HUD;
    public Image Menu;

    public static UIManager instance { get; private set; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

}
