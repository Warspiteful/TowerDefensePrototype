using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(AnimationOverridesDictionary))]
public class AnimationOverridePropertyDrawer : SerializableDictionaryPropertyDrawer {}