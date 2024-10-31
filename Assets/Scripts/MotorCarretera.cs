using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorCarretera : MonoBehaviour
{
    public GameObject containerCallesGo;
    public GameObject[] containerCallesArray;
    public float speed;
    public bool startGame;
    public bool endGame;
    int contCalles = 0;
    int numSelectorCalles;

    public Vector3 extendScreen;
    public bool exitScreen;
    public GameObject mCamGo;
    public Camera mCamComp;

    public GameObject calleLast;
    public GameObject calleNew;

    public float sizeCalle;

    public GameObject cocheGo;
    public GameObject audioFxGo;
    public AudioFx audioFxScript;
    public GameObject bgFinalGo;
    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        containerCallesGo = GameObject.Find("ContainerCalle");
        
        mCamGo = GameObject.Find("Main Camera");
        mCamComp = mCamGo.GetComponent<Camera>();

        bgFinalGo = GameObject.Find("PanelGameOver");
        bgFinalGo.SetActive(false);

        audioFxGo = GameObject.Find("AudioFx");
        audioFxScript = audioFxGo.GetComponent<AudioFx>();

        cocheGo = GameObject.FindObjectOfType<Car>().gameObject;

        speedMotorRoad();
        ExtendScreen();
        SearchCalle();
    }
    
    void createCalle() 
    {
        contCalles ++;
        numSelectorCalles = Random.Range(0, containerCallesArray.Length);
        var calle = Instantiate(containerCallesArray[numSelectorCalles]);
        calle.SetActive(true);
        calle.name = "Calle" + contCalles;
        calle.transform.parent = gameObject.transform; 
        positionCalle();
    }
    void SearchCalle() 
    {
        containerCallesArray = GameObject.FindGameObjectsWithTag("Calle");
        int cont = 0;
        foreach(var calle in containerCallesArray)
        {
            calle.gameObject.transform.parent = containerCallesGo.transform;
            calle.gameObject.SetActive(false);
            calle.gameObject.name = "CalleOFF_" + cont++;
        }

        createCalle();
    }
    void positionCalle()
    {
        calleLast = GameObject.Find("Calle" + (contCalles-1));
        calleNew = GameObject.Find("Calle" + contCalles);
        
        measureCalle();
        calleNew.transform.position = new Vector3(
            calleLast.transform.position.x, 
            calleLast.transform.position.y + sizeCalle - 5,
            0
        );

        exitScreen = false;
            
    }
    
    void measureCalle()
    {
        for(int i = 0; i < calleLast.transform.childCount; i++)
        {
            if(calleLast.transform.GetChild(i).gameObject.GetComponent<Pieza>() != null)
            {
                float sizePart = calleLast.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
                sizeCalle += sizePart;
            }
        }
    }

    void speedMotorRoad() => speed = 10;


    void ExtendScreen()
    {
        extendScreen = new Vector3(0, mCamComp.ScreenToWorldPoint(new Vector3(0,0,0)).y - 0.5f, 0);
    }
    void Update()
    {
        if(startGame && !endGame)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            
            if(calleLast.transform.position.y < extendScreen.y && !exitScreen)
            {
                exitScreen = true;
                DestroyCalles();
            }
        }
    }

    void DestroyCalles()
    {
        Destroy(calleLast);
        sizeCalle = 0;
        calleLast = null;
        createCalle();
    }
}
