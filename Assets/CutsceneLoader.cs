using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneLoader : MonoBehaviour
{
    [Header("The scene this script is refering to must be in Hierarchy window, unloaded \n")]
    [SerializeField]
    private string sceneName;

    void Start()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
