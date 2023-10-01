using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PsiLocatorLogic : MonoBehaviour
{   
    
    private FieldOfView fieldOfView;
    private AudioManager audioManager;

    [SerializeField] private float increaseDuration = 2f;
    [SerializeField] private float maxValue = 20f;

    public float CdLocator;
    public bool isCDActive;

    void Start()
    {
        fieldOfView = GetComponent<FieldOfView>();
        audioManager = AudioManager.instance;
    }


    public void PsiLocate()
    {   

        if (!isCDActive)
        {
            StartCoroutine(TimerPsiWave());
            audioManager.Play("PsiLocator");
        }
    }

    private IEnumerator TimerPsiWave()
    {
        isCDActive = true;
        float startTime = Time.time;
        float elapsedTime = 0;

        while (elapsedTime < increaseDuration)
        {
            elapsedTime = Time.time - startTime;
            fieldOfView.viewRadius = Mathf.Lerp(1f, maxValue, elapsedTime / increaseDuration);
            CdLocator = elapsedTime;
            yield return null;
        }

        fieldOfView.viewRadius = 1;
        isCDActive = false;


    }

}
