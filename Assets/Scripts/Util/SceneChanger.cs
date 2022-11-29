using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class SceneChanger : ScriptableObject
{
  public void StartLevel1()
  {
      SceneManager.LoadScene(1);
  }
}
