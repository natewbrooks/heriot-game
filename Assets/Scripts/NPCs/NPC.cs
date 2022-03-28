using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    private GameObject chatPrompt;
    private GameObject chat;

    private Vector2 chatPromptPos;
    private Vector2 chatPos;

    private bool inRange;

    // Start is called before the first frame update
    void Start()
    {
        chatPrompt = gameObject.transform.GetChild(0).GetChild(0).gameObject;
        chat = gameObject.transform.GetChild(0).GetChild(1).gameObject;

        chatPromptPos = new Vector2(gameObject.transform.position.x, (gameObject.transform.position.y - 2f));
        chatPos = new Vector2(gameObject.transform.position.x, (gameObject.transform.position.y + 3f));

        chatPrompt.transform.position = chatPromptPos;
        chat.transform.position = chatPos;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inRange) {
            chatPrompt.SetActive(false);
            chat.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        inRange = true;
    }

    void OnTriggerExit2D(Collider2D other) {
        chatPrompt.SetActive(true);
        chat.SetActive(false);
    }
}
