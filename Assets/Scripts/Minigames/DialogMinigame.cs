using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class DialogMinigame : MinigameBase
{
    //Current state of the minigame. It changes in the following order:
    //Ready->Hit->FadeOut->FadeIn
    private enum State
    {
        Ready,
        Hit,
        FadeOut,
        FadeIn
    }

    [Header("References")]
    [SerializeField]
    private Image image;
    [SerializeField]
    private TMPro.TextMeshProUGUI messageField;
    [SerializeField]
    private AudioSource talkingSound;

    [Header("Config")]
    [SerializeField]
    private float oneLetterTypingSpeed;

    [Header("Debug")]
    [SerializeField]
    [ReadOnly]
    private State state;
    
    private float counter;
    private string message = "";

    private void Awake()
    {
        closedEvent += OnClose;
        messageField.text = "";
        talkingSound.Play();
    }

    private void OnClose()
    {
        StopAllCoroutines();
    }

    protected override void Update()
    {
        base.Update();

        if (messageField.text.Length != message.Length)
        {
            counter += Time.deltaTime;
            while(counter > oneLetterTypingSpeed)
            {
                counter -= oneLetterTypingSpeed;
                messageField.text = messageField.text + message[messageField.text.Length];
            }
        }
    }

    public override void Open()
    {
        base.Open();
    }
    public void SetMessage(string newMessage)
    {
        message = newMessage;
    }
    public void SetImage(Sprite newImage)
    {
        image.sprite = newImage;
    }
    public void SetTalkingSound(AudioClip newSound)
    {
        talkingSound.clip = newSound;
    }
}
