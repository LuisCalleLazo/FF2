
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    public GameObject carGo;
    public MotorCarretera motor;
    public AudioSource SoundChoque;
    public Chronometer chronometer;
    public float turningAngle = 30f;
    public float speed = 10f;
    void Start()
    {
        carGo = FindObjectOfType<Car>().gameObject;
        SoundChoque = SoundChoque.GetComponent<AudioSource>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical);
        transform.Translate(movement * speed * Time.deltaTime);

        float turnInZ = horizontal * -turningAngle;
        carGo.transform.rotation = Quaternion.Euler(0, 0, turnInZ);


        if (vertical != 0)
        {
            float forwardAngle = vertical > 0 ? turningAngle : -turningAngle;
            carGo.transform.Rotate(0, 0, horizontal * forwardAngle * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trash"))
        {
            SoundChoque.Play();
            chronometer.time = chronometer.time - 20;
            Destroy(collision.gameObject);
        }
    }
    IEnumerator RestartSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        RestartScene();
    }
    void RestartScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
