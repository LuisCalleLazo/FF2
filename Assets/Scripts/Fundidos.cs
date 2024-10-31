
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fundidos : MonoBehaviour
{
    public Image fundido;
    public string[] scenes;

    void Start()
    {
        fundido.CrossFadeAlpha(0, 0.5f, false);
    }

    public void FadeOut(int n)
    {
        fundido.CrossFadeAlpha(1, 0.5f, false);
        StartCoroutine(ChangeScene(scenes[n]));
    }

    IEnumerator ChangeScene(string name)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(name);

    }
}
