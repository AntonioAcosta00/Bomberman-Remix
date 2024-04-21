using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public bool On = true;

    // Update is called once per frame
    // Allows the gameobject to not be deleted when loading into a new scene
    void Update()
    {
        if (On)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
            Destroy(GetComponent<DontDestroy>());
        }
    }

}
