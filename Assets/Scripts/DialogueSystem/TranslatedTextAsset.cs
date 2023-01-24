using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace DialogueSystem
{
    [CreateAssetMenu(menuName="Dialogue/Translated Text Asset")]

    public class TranslatedTextAsset : ScriptableObject
    {
        public OnValueChanged onLanguageUpdate;
        public LanguageOptionDictionary dict;
        [SerializeField] private LanguageSelection translation;
        [SerializeField] private DialogueTable dialogueTable;
        void OnEnable()
        {
            translation.onValueChanged += onLanguageUpdate;
            LoadLanguage();
        }

        public void LoadLanguage()
        {
            dialogueTable = new DialogueTable();

            string[] rows = GetTextAsset().text.Split("\n").Skip(1).ToArray();
            foreach (string row in rows)
            {
                string[] columns = row.Split(",");
                dialogueTable.Add(int.Parse(columns[0]), columns[1]);
            }
        }

        public TextAsset GetTextAsset()
        {
            return dict[translation.Value];
        }

        public string GetLine(int id)
        {
                       
            return dialogueTable[id];
        }
    }
}
