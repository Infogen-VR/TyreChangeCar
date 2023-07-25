using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class addressablesceneload : MonoBehaviour
{
    public string secondSceneAddress;

    void Start()
    {
        // Start the loading process
        StartCoroutine(LoadScenes());
    }

    IEnumerator LoadScenes()
    {
        yield return new WaitForSeconds(5f);

        AsyncOperationHandle<SceneInstance> secondSceneHandle = Addressables.LoadSceneAsync(secondSceneAddress, LoadSceneMode.Single);

        // Wait until the second scene is fully loaded
        while (!secondSceneHandle.IsDone)
        {
            yield return null;
        }
    }


    }
