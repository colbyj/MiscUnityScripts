using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


static class ObjectClicked
{
    static private GameObject FetchClickedGameObjectFromRaycast(Camera camera = null)
    {
        RaycastHit hitDetails = new RaycastHit();

        if (!Input.GetMouseButtonDown(0))
        {
            return null;
        }

        if (camera == null)
        {
            camera = Camera.main;
        }

        if (camera == null)
        {
            return null;
        }

        Vector3 centerScreen = camera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, camera.nearClipPlane));

        if (!Physics.Raycast(camera.ScreenPointToRay(centerScreen), out hitDetails, 2.0f))
        {
            return null;
        }

        if (hitDetails.collider == null)
        {
            return null;
        }

        return hitDetails.collider.gameObject;
        
    }

    /// <summary>
    /// The game object must have a collider set as a trigger.
    /// </summary>
    static public bool CheckClickedGameObject(GameObject gameObjectToCheck, Camera camera = null)
    {
        GameObject clickedGameObject = FetchClickedGameObjectFromRaycast(camera);

        if (clickedGameObject && clickedGameObject == gameObjectToCheck)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// The game object must have a collider set as a trigger.
    /// </summary>
    static public bool CheckClickedGameObject(GameObject[] gameObjectsToCheck, Camera camera = null)
    {
        GameObject clickedGameObject = FetchClickedGameObjectFromRaycast(camera);

        for (int i = 0; i < gameObjectsToCheck.Length; i++)
        {
            if (clickedGameObject && clickedGameObject == gameObjectsToCheck[i])
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// The game object must have a collider set as a trigger.
    /// </summary>
    static public bool CheckClickedGameObject(string gameObjectNameToCheck, Camera camera = null)
    {
        GameObject clickedGameObject = FetchClickedGameObjectFromRaycast(camera);

        if (clickedGameObject && clickedGameObject.name == gameObjectNameToCheck)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// The game object must have a collider set as a trigger.
    /// </summary>
    static public bool CheckClickedGameObject(string[] gameObjectsNameToCheck, Camera camera = null)
    {
        GameObject clickedGameObject = FetchClickedGameObjectFromRaycast(camera);

        for (int i = 0; i < gameObjectsNameToCheck.Length; i++)
        {
            if (clickedGameObject && clickedGameObject.name == gameObjectsNameToCheck[i])
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// The game object must have a collider set as a trigger.
    /// </summary>
    static public GameObject FetchClickedGameObject(Camera camera = null)
    {
        return FetchClickedGameObjectFromRaycast(camera);
    }

    /// <summary>
    /// The game object must have a collider set as a trigger.
    /// </summary>
    static public GameObject FetchClickedGameObject(GameObject[] gameObjectsToCheck, Camera camera = null)
    {
        GameObject clickedGameObject = FetchClickedGameObjectFromRaycast(camera);

        for (int i = 0; i < gameObjectsToCheck.Length; i++)
        {
            if (clickedGameObject && clickedGameObject == gameObjectsToCheck[i])
            {
                return clickedGameObject;
            }
        }
        return null;
    }

    /// <summary>
    /// The game object must have a collider set as a trigger.
    /// </summary>
    static public GameObject FetchClickedGameObject(string[] gameObjectsNameToCheck, Camera camera = null)
    {
        GameObject clickedGameObject = FetchClickedGameObjectFromRaycast(camera);

        for (int i = 0; i < gameObjectsNameToCheck.Length; i++)
        {
            if (clickedGameObject && clickedGameObject.name == gameObjectsNameToCheck[i])
            {
                return clickedGameObject;
            }
        }
        return null;
    }
}
