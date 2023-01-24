using System;

namespace DialogueSystem
{
    [Serializable]
    public class SpeechStruct
    {

        public SpeechStruct(DialogueStruct ds, string line)
        {
            speech = line;
            Speaker = ds.Speaker;
            LeftSide = ds.LeftSide;
            RightSide = ds.RightSide;
            LeftExpression = ds.LeftExpression;
            RightExpression = ds.RightExpression;
            Sound = ds.Sound;
        }
        public string Speaker;
        public string speech;
        public string LeftSide;
        public string RightSide;
        public string LeftExpression;
        public string RightExpression;
        public string Sound;

    }
}