using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PsiLocatorLogic : MonoBehaviour
{   
    private InputManager inputManager;
    
    private FieldOfView fieldOfView;

    [SerializeField] private float increaseDuration = 2f;
    [SerializeField] private float maxValue = 20f;

    void Start()
    {
        inputManager = InputManager.instance;
        fieldOfView = GetComponent<FieldOfView>();
    }


    public void PsiLocate()
    {
       StartCoroutine(TimerPsiWave());

    }

    private IEnumerator TimerPsiWave()
    {
        float startTime = Time.time;
        float elapsedTime = 0;

        while (elapsedTime < increaseDuration)
        {
            elapsedTime = Time.time - startTime;
            fieldOfView.viewRadius = Mathf.Lerp(1f, maxValue, elapsedTime / increaseDuration);
            yield return null;
        }

        fieldOfView.viewRadius = 1;
    }

}
