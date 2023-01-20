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

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
