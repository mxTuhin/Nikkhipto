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
    public GameObject bulletPrefab;
    public AudioSource shooterAudioSource;
    public AudioClip shootSFX;
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

    
    public void Shoot()
    {
        Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(center);
        shooterAudioSource.volume = Random.Range(0.1f, 0.2f);
        shooterAudioSource.PlayOneShot(shootSFX);
        StartCoroutine(waitBeforeFlash());
        
        Debug.DrawRay(ray.origin, ray.direction, Color.red,5);
        if (Physics.Raycast(ray, out hit))
        {
            // GameObject t_hole = Instantiate(bulletPrefab, hit.point+hit.normal*0.001f, Quaternion.identity) as GameObject;
            // t_hole.transform.LookAt(hit.point+hit.normal);
            // Destroy(t_hole, 2f);
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
                    // else if (playerObject != null)
                    // {
                    //     takeHit.TakeDamage(damage, "Player");
                    // }
                    else if (pedsObject != null)
                    {
                        print("Peds");
                        takeHit.TakeDamage(damage, "Peds");
                    }

                    if (hit.transform.gameObject.name == "LeaderOne")
                    {
                        takeHit.TakeDamage(damage, "LeaderOne");
                    }

                    if (hit.transform.gameObject.name == "Enemy")
                    {
                        takeHit.TakeDamage(damage, "Enemy");
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
