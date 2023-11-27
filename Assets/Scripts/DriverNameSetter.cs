using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DriverNameSetter : MonoBehaviour
{
    [SerializeField] private TMP_InputField field;
    [SerializeField] private string sceneToLoad;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Submit();
        }
    }

    public void Submit()
    {
        string name = field.text;
        PlayerPrefs.SetString("DriverName", name);
        SceneManager.LoadScene(sceneToLoad);
    }
}
