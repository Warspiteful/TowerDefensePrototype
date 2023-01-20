using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    
    public enum Language
    {
        ENGLISH, TAGALOG
    }
    
    [CreateAssetMenu(menuName="Dialogue/Language Selector")]

    public class LanguageSelection : ScriptableObject
    {


        [SerializeField] private Language selectedLanguage;
        
    }
}