using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DialogueSystem
{
    [CreateAssetMenu(menuName="Dialogue/Translated Text Asset")]

    public class TranslatedTextAsset : ScriptableObject
    {
        public LanguageOptionDictionary dict;
        [SerializeField] private LanguageSelection translation;
    }
}
