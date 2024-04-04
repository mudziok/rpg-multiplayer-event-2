using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatsManager : MonoBehaviour
{
    // Singleton
    static CheatsManager main = null;
    private void Awake()
    {
        if (main != null)
            Destroy(this);
        else main = this;
    }

#if UNITY_EDITOR
    public GameResource resource;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) GameObject.Find("PlayerTransform").GetComponent<PlayerBackPack>().TransferIn(resource, 1);
    }
#endif
}
