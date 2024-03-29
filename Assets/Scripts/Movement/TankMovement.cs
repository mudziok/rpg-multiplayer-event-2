using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPlayerController : MonoBehaviour
{
    // Implementacja "tank controls". Postaæ porusza siê do przodu wzglêdem swojej rotacji, co pozwala zachowaæ kierunek ruchu przy zmianie pozycji aktywnej kamery
    private CharacterController controller;
    private float vSpeed = 0;
    private bool paused = false;
    private bool locked = false;

    public float speed = 5f;
    public float turnSpeed = 180f;
    // CharacterController domyœlnie nie uwzglêdnia grawitacji, bo steruje ruchem za pomoc¹ bezpoœrednich przemieszczeñ. Grawitacjê trzeba uwzglêdniæ i nanieœæ podczas wykonania Update()
    public float gravity = 9.8f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void Pause()
    {
        paused = true;
    }
    public void Unpause()
    {
        paused = false;
    }
    public void LockMovement()
    {
        locked = true;
    }
    public void UnlockMovement()
    {
        locked = false;
    }
    void Update()
    {
        if (!paused)
        {
            Vector3 moveDirection = new Vector3(0, 0, 0);

            if (!locked)
            {
                // Obrót postaci wokó³ osi Y
                transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);
                // Obliczenie ruchu do przodu
                moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
            }

            // Zastosuj grawitacjê
            vSpeed -= gravity * Time.deltaTime;
            moveDirection.y = vSpeed;
            controller.Move(moveDirection * Time.deltaTime);
        }
    }
}
