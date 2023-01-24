using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace DialogueSystem
{
    public class DialogueController : MonoBehaviour
    {

        [SerializeField] private GameObject speakerPrefab;
        [SerializeField] private GameObject dialogueOverlay;

        [SerializeField] private TextMeshProUGUI speakerText;

        [SerializeField] private TextMeshProUGUI dialogueText;

        [SerializeField] private Dictionary<string, GameObject> availableSpeakers;

        [SerializeField] private CurrentDialogue activeDialogue;

        // Start is called before the first frame update
        void Start()
        {
            activeDialogue.onValueChanged += HandleDialogue;
        }


        private void HandleDialogue()
        {
            StartCoroutine(placeholder());
        }
        void ParseDialogue(DialogueScript script)
        {
            
        }

        void MoveImage(GameObject speake, int x, int y)
        {

        }

        IEnumerator placeholder()
        {
            foreach(SpeechStruct ds in activeDialogue.Value)
            {
                dialogueText.text = ds.speech;
                speakerText.text = ds.Speaker;
                yield return new WaitForSeconds(1);
            }
        }
    }
}
