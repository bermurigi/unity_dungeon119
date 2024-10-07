using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class start : MonoBehaviour
{
   

    public NPCConversation Conversation;
    private void Start()
    {
        ConversationManager.Instance.StartConversation(Conversation);
    }
    
    



}
