using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DialogueObject;
using UnityEngine.Events;
using System;
using System.Runtime.InteropServices;

public class DialogueViewer : MonoBehaviour
{
    [SerializeField] Transform parentOfResponses;
    [SerializeField] Button prefab_btnResponse;
    [SerializeField] Button btnSpeedyProgress;
    DialogueController controller;

    [DllImport("__Internal")]
    private static extern void openPage(string url);

    private void Start()
    {
        controller = GetComponent<DialogueController>();
        controller.onEnteredNode += OnNodeEntered;
        controller.InitializeDialogue();
    }

    public static void KillAllChildren(UnityEngine.Transform parent)
    {
        UnityEngine.Assertions.Assert.IsNotNull(parent);
        for (int childIndex = parent.childCount - 1; childIndex >= 0; childIndex--)
        {
            UnityEngine.Object.Destroy(parent.GetChild(childIndex).gameObject);
        }
    }

    private void OnNodeSelected(int indexChosen)
    {
        Debug.Log("Chose: " + indexChosen);
        controller.ChooseResponse(indexChosen);
    }

    private void OnNodeEntered(Node newNode)
    {
        //txtTitle.Clear();
        //txtMessage.Clear();
        KillAllChildren(parentOfResponses);

        UnityAction typeResponsesAfterMessage = delegate
        {
            for (int i = newNode.responses.Count - 1; i >= 0; i--)
            {
                int currentChoiceIndex = i;
                var response = newNode.responses[i];
                var responceButton = Instantiate(prefab_btnResponse, parentOfResponses);
                responceButton.onClick.AddListener(delegate { OnNodeSelected(currentChoiceIndex); });
            }
        };

       /* UnityAction typeMessageAfterTitle = delegate
        {
            txtMessage.Begin(newNode.text, typeResponsesAfterMessage);
        }; */
    }

    public static Sprite Texture2DToSprite(Texture2D t)
    {
        return Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f));
    }

    private void ShowContinueButton(UnityAction onContinuePressed)
    {
        var responceButton = Instantiate(prefab_btnResponse, parentOfResponses);
        responceButton.onClick.AddListener(delegate {
            KillAllChildren(parentOfResponses);
            onContinuePressed();
        });
    }
}
