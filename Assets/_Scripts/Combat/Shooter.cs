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

    private float graceTime;

    public GameObject bloodSplash;
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            if (PlayerController.instance.inAction)
            {
                Shoot();
                graceTime = Time.time;
            }
            
        }

        if (Time.time-graceTime >= 1)
        {
            PlayerController.instance.isShooting = false;
        }
        
    }

    
    void Shoot()
    {
        Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(center);
        StartCoroutine(waitBeforeFlash());
        
        Debug.DrawRay(ray.origin, ray.direction*200, Color.red, 2);
        if (Physics.Raycast(ray, out hit))
        {
            print(hit.transform.gameObject.name);
            PlayerController.instance.isShooting = true;
            var takeHit =hit.transform.GetComponent<Health>();
            if (takeHit != null)
            {
                Instantiate(bloodSplash, hit.transform.position+new Vector3(0,1,0), Quaternion.identity);
                var policeObject = hit.transform.GetComponent<Police>();
                var playerObject = hit.transform.GetComponent<PlayerController>();
                var pedsObject = hit.transform.GetComponent<PedestrianNavigationController>();
                if (takeHit != null)
                {
                    
                    if (policeObject != null)
                    {
                        
                        takeHit.TakeDamage(damage, "Police");
                    }
                    else if (playerObject != null)
                    {
                        takeHit.TakeDamage(damage, "Player");
                    }
                    else if (pedsObject != null)
                    {
                        print("Peds");
                        takeHit.TakeDamage(damage, "Peds");
                    }
                }
            }
        }

    }

    IEnumerator waitBeforeFlash()
    {
        yield return new WaitForSeconds(0.1f);
        Instantiate(muzzleFlash, flashPosition.transform.position, Quaternion.identity);
    }
}
