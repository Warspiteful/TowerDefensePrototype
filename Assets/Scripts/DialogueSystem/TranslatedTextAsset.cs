using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DialogueSystem
{
    [CreateAssetMenu(menuName="Dialogue/Translated Text Asset")]

    public class TranslatedTextAsset : ScriptableObject
    {
        public OnValueChanged onLanguageUpdate;
        public LanguageOptionDictionary dict;
        [SerializeField] private LanguageSelection translation;

        void Start()
        {
            translation.onValueChanged += onLanguageUpdate;
        }

        public TextAsset GetTextAsset()
        {
            return dict[translation.Value];
        }

        public string GetLine(int id)
        {
            Debug.Log(GetTextAsset().text);
            return " ";
        }
    }
}
