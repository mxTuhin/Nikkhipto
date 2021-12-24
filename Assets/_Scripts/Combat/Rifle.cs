
using UnityEngine;

public class Rifle : MonoBehaviour
{
    public float damage = 10;

    public float range = 10000;

    public Camera fpsCam;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
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
