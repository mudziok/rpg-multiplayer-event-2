using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TreeForChopping : MonoBehaviour
{
    public UnityEvent onTreeFall = new UnityEvent();

    //todo indykator hp, animacja spadania (mo¿liwa do skipa) dŸwiêk r¹bania
    [SerializeField] float fallingSpeed = 50.0f;

    int maxHp = 3; 
    int currentHP; //how many chopps is need to cut this tree

    private void Start()
    {
        currentHP = maxHp;
    }

    private void OnTriggerEnter(Collider other)
    {
        currentHP--;
        if (currentHP == 0)
        {
            onTreeFall.Invoke();
            StartCoroutine(FallDown());
        }
    }

    public void SetDefaults()
    {
        StopAllCoroutines();
        currentHP = maxHp;
        transform.rotation = Quaternion.Euler(0.0f, Random.Range(-180.0f, 180.0f), 0.0f);
    }


    IEnumerator FallDown()
    {
        while (transform.up.y>=0.0f) //Autor: Doœæ g³upi warunek, ale jeœli "góra" drzewa wskazuje poni¿ej 0 to znaczy ¿e drzewo le¿y;
        {
            transform.rotation *= Quaternion.Euler(fallingSpeed * Time.deltaTime, 0.0f, 0.0f);
            yield return null;
        }
        Vector3 basePosition = transform.position;
        float delay = 0.5f;
        Vector3 down = Vector3.down;
        while (delay > 0)
        {
            delay -= Time.deltaTime;
            transform.position = transform.position + down * Time.deltaTime;
            yield return null;
        }
        transform.position = basePosition;
        SetDefaults();
    }
}
