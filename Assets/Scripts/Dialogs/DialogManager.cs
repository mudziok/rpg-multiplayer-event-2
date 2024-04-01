using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;

    GameObject currentDialog;

    private void Awake()
    {
        if(Instance!=null) { Destroy(this); }
        else { Instance = this; }
    }

    public DialogBase InitDialog(GameObject dialogPrefab)
    {
        if (currentDialog != null)
        {
            Destroy(currentDialog);
        }
        currentDialog = Instantiate(dialogPrefab, transform);
        if (currentDialog.TryGetComponent(out DialogBase dialog))
        {
            dialog.closedEvent += () => currentDialog= null;
            return dialog;
        }
        Debug.LogErrorFormat("No dialog script in dialog prefab {0}", dialogPrefab.name);
        currentDialog = null;
        return null;
    }
}
