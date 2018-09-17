using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This Class is a basic class that contains generic funtions for use in many scipts
 */
public class GenericFunctions
{
  // Basic WASD Movement
  public static Vector3 BasePlayerMovement(float moveSpeed)
  {
    Vector3 movementDirection = new Vector3(0, 0, 0);

    if (Input.GetKey(KeyCode.W))
    {
      movementDirection += Vector3.up;

    }

    if (Input.GetKey(KeyCode.S))
    {
      movementDirection += Vector3.down;

    }

    if (Input.GetKey(KeyCode.A))
    {
      movementDirection += Vector3.left;

    }

    if (Input.GetKey(KeyCode.D))
    {
      movementDirection += Vector3.right;

    }

    return movementDirection.normalized * (moveSpeed * Time.deltaTime);

  }

  #region this will find each game object with the given tag then put it into the given list
  public static List<T> GatherComponetFromSceneByTag<T>(List<T> startingList, string tag)
  {
    foreach (GameObject gObject in GameObject.FindGameObjectsWithTag(tag))
    {
      startingList.Add(gObject.GetComponent<T>());

    }

    return startingList;

  }

  public static void GatherComponetFromSceneByTag<T>(ref List<T> startingList, string tag)
  {
    foreach (GameObject gObject in GameObject.FindGameObjectsWithTag(tag))
    {
      startingList.Add(gObject.GetComponent<T>());

    }
  }

  #endregion

  public static void GTFO()
  {
    Application.Quit();

  }

}
