using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("The scene this script is refering to must be in Hierarchy window, unloaded \n")]
    [SerializeField]
    private string sceneName;

    private void Awake()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
