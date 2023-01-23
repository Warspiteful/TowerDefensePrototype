using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DialogueSystem
{
    [CreateAssetMenu(menuName="Dialogue/Dialogue Script")]
    public class DialogueScript : ScriptableObject
    {

        [SerializeField]
        private TranslatedTextAsset text;
        [SerializeField] private TextAsset ConversationController;
        private TextAsset translatedText;

        [SerializeField] private CurrentDialogue dialogueSetter;
        
        [SerializeField] private ScriptStruct conversation;

        void Start()
        {
            
            text.onLanguageUpdate = LoadDialogue;
            LoadDialogue();
        }
        
        private void LoadDialogue()
        {
            
            Debug.Log(ConversationController.text);
            conversation = JsonUtility.FromJson<ScriptStruct>(ConversationController.text);

        }

        private int conversationID;

        public void LoadConversation(int id)
        {
        
                LoadDialogue();
       
            conversationID = id;
            

            Debug.Log(conversation.Script.Count);
            
                foreach (DialogueStruct ds in conversation.Script[conversationID].Lines)
                {
                    Debug.Log(ds.Speaker +": " + ds.LineID);
                }
            




        }
    }
}
