using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DialogueTablePropertyDrawer))]
public class DialogueTablePropertyDrawer : SerializableDictionaryPropertyDrawer {}