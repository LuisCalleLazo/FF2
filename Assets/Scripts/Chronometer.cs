
using UnityEngine;
using UnityEngine.UI;

public class Chronometer : MonoBehaviour
{
    public GameObject motorRoadsGo;
    public MotorCarretera motorRoadController;
    public float time;
    public float distance;
    public Text textTime;
    public Text textDistance;
    public Text textDistanceFinal;

    // Start is called before the first frame update
    void Start()
    {
        motorRoadsGo = GameObject.Find("MotorCarretera");
        motorRoadController = motorRoadsGo.GetComponent<MotorCarretera>();

        time = 20;

        textTime.text = $"0 : {(int)time}";
        textDistance.text = "0";

    }

    // Update is called once per frame
    void Update()
    {
        if(motorRoadController.startGame && !motorRoadController.endGame)
            CalculateTimeDistance();

        if(time <= 0 && !motorRoadController.endGame)
        {
            motorRoadController.endGame = true;
            motorRoadController.GameOverStates();
            textDistanceFinal.text = ((int)distance).ToString() + " mts";
        }
    }

    void CalculateTimeDistance()
    {
        distance += Time.deltaTime * motorRoadController.speed;
        textDistance.text = ((int)distance).ToString();

        time -= Time.deltaTime;
        int minutes = (int) time / 60;
        int seconds = (int) time % 60;

        textTime.text = $"{minutes} : {seconds.ToString().PadLeft(2, '0')}";
    }
}
