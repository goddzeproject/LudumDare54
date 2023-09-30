using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public GameObject FOVHelp;

    public Collider WinBox;

    public GameObject Player;


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

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Restart()
    {

    }

    public void ChekWin()
    {
        //Zaderjka i vizov Okna Pobedi
        Debug.Log("WIN");
    }

    private void ChekLose()
    {
        
    }


}
