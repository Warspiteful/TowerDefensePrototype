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

        void Start()
        {
            
            text.onLanguageUpdate = LoadDialogue;
            LoadDialogue();
            ConversationStruct s = JsonUtility.FromJson<ConversationStruct>(ConversationController.text);
            Debug.Log(s);
        }
        private void LoadDialogue()
        {
            translatedText = text.GetTextAsset();
        }

        private int conversationID;

        public void LoadConversation(int id)
        {
            conversationID = id;


            
        }
    }
}
