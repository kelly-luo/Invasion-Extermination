using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 20f;
    public float range = 100f;
    public Camera cam;

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
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, this.range))
        {
            Debug.Log(hit.transform.name);

            Target enemy = hit.transform.GetComponent<Target>();
            if (enemy != null)
            {
                enemy.TakeDamage(this.damage);
            }
        }
    }
}
