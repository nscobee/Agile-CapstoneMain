using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This holds entry points for each level and has a function to return a points transform
 */
public class LevelEntryPoints : MonoBehaviour
{
  public List<GameObject> points = new List<GameObject>();

  public Transform GetPositonOfPoint(int pointNumber)
  {
    Transform location = null;

    try
    {
      location = points[pointNumber - 1].transform;

    }

    catch
    {
      Debug.LogError("Tried to get location point that wasnt on list");

    }

    return location;

  }

}
