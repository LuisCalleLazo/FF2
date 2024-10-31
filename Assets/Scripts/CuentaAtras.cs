using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuentaAtras : MonoBehaviour
{
    public GameObject motorRoadGo;
    public MotorCarretera motorController;
    public Sprite[] numbers;
    
    public GameObject counterNumGo;
    public SpriteRenderer counterNumComp;

    public GameObject cocheControllerGo;
    public GameObject cocheGo;

    void Start()
    {
        InitComponent();
    }

    void InitComponent()
    {
        motorRoadGo = GameObject.Find("MotorCarretera");
        motorController = motorRoadGo.GetComponent<MotorCarretera>();

        counterNumGo = GameObject.Find("Contador");
        counterNumComp = counterNumGo.GetComponent<SpriteRenderer>();

        cocheGo = GameObject.Find("Car");
        cocheControllerGo = GameObject.Find("ControllerCoche");

        InitCountdown();
    }
    void InitCountdown()
    {
        StartCoroutine(Counting());
    }

    IEnumerator Counting()
    {
        cocheControllerGo.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(2);
        int numFinal = 5;

        for(int i = 1; i < numFinal; i++)
        {
            counterNumComp.sprite = numbers[i];
            counterNumComp.transform.localScale = GetUniformScale(counterNumComp.sprite);
            this.gameObject.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(1f);
        }
        

        yield return new WaitForSeconds(2.5f);

        counterNumComp.sprite = numbers[numFinal];
        motorController.startGame = true;
        cocheGo.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(2f);
        this.gameObject.GetComponent<AudioSource>().Stop();

        counterNumGo.SetActive(false);
    }

    // Método que ajusta el tamaño del sprite
    Vector3 GetUniformScale(Sprite sprite)
    {
        float desiredWidth = 2f;
        float desiredHeight = 2f;

        float spriteWidth = sprite.bounds.size.x;
        float spriteHeight = sprite.bounds.size.y;

        float scaleX = desiredWidth / spriteWidth;
        float scaleY = desiredHeight / spriteHeight;

        return new Vector3(scaleX, scaleY, 1f);
    }
}
