using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DialogueController : MonoBehaviour
{

    [SerializeField] private GameObject speakerPrefab;
    [SerializeField] private GameObject dialogueOverlay;

    [SerializeField] private TextMeshProUGUI speakerText;
    
    [SerializeField] private TextMeshProUGUI dialogueText;
    
    [SerializeField] private Dictionary<string, GameObject> availableSpeakers;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ParseDialogue()
    {
        
    }

    void MoveImage(GameObject speake, int x, int y)
    {
        
    }
}
