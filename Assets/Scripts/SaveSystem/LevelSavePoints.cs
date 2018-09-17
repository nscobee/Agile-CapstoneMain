using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This holds entry points for each level and has a function to return a points transform
 */
public class LevelSavePoints : MonoBehaviour
{
  public List<Transform> points = new List<Transform>();

  public Transform lastPointSentOut;

  // this tries to get the position of the point and if it cant then it sends an error message
  public Transform GetPositonOfPoint(int pointNumber)
  {
    Transform location = null;

    try
    {
      location = points[pointNumber];

    }

    catch
    {
      Debug.LogError("Tried to get location point that wasnt on list");

    }

    lastPointSentOut = location;

    return location;

  }
}
