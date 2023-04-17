using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTutorial : MonoBehaviour
{
    public LayerMask layerMask;
    private bool playerDetected;
    public float radius;
    public float angleConstraint;

    // Update is called once per frame
    void Update()
    {
        if (!playerDetected)
        {
            Search();

        }
        else
        {
            TrackPlayer();
        }
    }

    void Search()
    {
        //Create a sphere around the enemy
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Player")
            {
                float angle = Vector3.Angle(transform.forward, (hitCollider.transform.position - transform.position).normalized);
                if (-angleConstraint * 0.5f <= angle && angle <= angleConstraint * 0.5f)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, (hitCollider.transform.position - transform.position).normalized * radius, out hit, Mathf.Infinity, layerMask))
                    {
                        if (hit.collider.tag == "Player")
                        {
                            Debug.DrawRay(transform.position, (hit.collider.transform.position - transform.position).normalized * hit.distance, Color.red);
                            //if player, start tracking
                            transform.LookAt(hit.collider.transform);
                            playerDetected = true;
                        }
                    }
                }
            }
        }
    }

    void TrackPlayer()
    {
        //keep tracking the player, if we lose line of sight then stop tracking
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.tag == "Player" && hit.distance <= radius)
            {
                Debug.DrawRay(transform.position, (hit.collider.transform.position - transform.position).normalized * hit.distance, Color.red);
                transform.LookAt(hit.collider.transform);
            }
            else
            {
                playerDetected = false;
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + (Quaternion.AngleAxis(angleConstraint * 0.5f, Vector3.up) * transform.forward) * radius);
        Gizmos.DrawLine(transform.position, transform.position + (Quaternion.AngleAxis(-angleConstraint * 0.5f, Vector3.up) * transform.forward) * radius);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
