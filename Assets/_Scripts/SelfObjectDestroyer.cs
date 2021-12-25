using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfObjectDestroyer : MonoBehaviour
{
    public static SelfObjectDestroyer instance;
    public float timeOut = 1.0f;
    public bool detachChildren = false;
    public GameObject missionOneComponents;

    // Use this for initialization
    void Awake ()
    {
        instance = this;
        
    }

    public void destroyMissionOneComponents(float timer)
    {
        StartCoroutine(DestroyNow(timer, missionOneComponents));
    }
	

    IEnumerator DestroyNow (float timer, GameObject component)
    {
        if (detachChildren) { // detach the children before destroying if specified
            transform.DetachChildren ();
        }

        yield return new WaitForSeconds(timer);
        Destroy(component);
    }
}
