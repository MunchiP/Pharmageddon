using System.Collections;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField]
    private Transform vfxHitRed;
    private Rigidbody bulletRigidbody;    

    private void Awake()
    {
        StartCoroutine(AutoDestruction());
        bulletRigidbody = GetComponent<Rigidbody>();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float speed = 40f;

        Camera mainCamera = Camera.main;
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit, 1000f))
        {
            targetPoint = hit.point; // punto exacto donde impacta el rayo
        }
        else
        {
            targetPoint = ray.GetPoint(1000f); // punto lejano en esa dirección
        }

        Vector3 direction = (targetPoint - transform.position).normalized;
        bulletRigidbody.linearVelocity = direction * speed;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("IgnorarBala") ||(other.CompareTag("IgnorarBala") ))
            return;

        Instantiate(vfxHitRed, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator AutoDestruction()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
