using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    
    // Update is called once per frame
    public Camera cam;
    private int damage = 10;

    public GameObject muzzleFlash;
    public GameObject flashPosition;
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        
    }
    
    void Shoot()
    {
        Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(center);
        Instantiate(muzzleFlash, flashPosition.transform.position, Quaternion.identity);
        
        Debug.DrawRay(ray.origin, ray.direction*200, Color.red, 2);
        if (Physics.Raycast(ray, out hit))
        {
            print(hit.transform.gameObject.name);
            var takeHit =hit.transform.GetComponent<Health>();
            if (takeHit != null)
            {
                takeHit.TakeDamage(damage);
            }
        }

    }
}
