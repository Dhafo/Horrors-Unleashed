using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public LayerMask layerMask;
    public float angleConstraint;
    public float radius;

    public bool playerDetected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //draw the enemies field of view
        //Debug.DrawRay(transform.position,
        //    new Vector3(Mathf.Sin(angleConstraint * Mathf.Deg2Rad), 0, Mathf.Cos(angleConstraint * Mathf.Deg2Rad)) *100f, Color.red);
        //Debug.DrawRay(transform.position,
        //    new Vector3(Mathf.Sin(-angleConstraint * Mathf.Deg2Rad) , 0, Mathf.Cos(-angleConstraint * Mathf.Deg2Rad)) * 100f , Color.red); 


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
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Player")
            {

                float angle = Vector3.Angle(transform.forward, (hitCollider.transform.position - transform.position).normalized);
                
                //find the forward vector and the direction of the collider that entered the sphere
                //calculate angle between the 2 lines
                //float angleW = Mathf.Acos(Vector3.Dot(transform.forward, toTarget) / (transform.forward.magnitude * toTarget.magnitude)) * Mathf.Rad2Deg;
                //cut the sphere with our fov contraints, if angle is outside this range we will be blind to the objects
                if (-angleConstraint * 0.5f <= angle && angle <= angleConstraint * 0.5f)
                {
                    Debug.Log(angle);
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, (hitCollider.transform.position - transform.position) * radius, out hit, Mathf.Infinity, layerMask))
                    {
                        if (hit.collider.tag == "Player")
                        {
                            //if player, start tracking
                            Debug.DrawRay(transform.position, hitCollider.transform.position - transform.position, Color.red);
                            gameObject.transform.LookAt(hit.collider.transform);
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
                Debug.DrawRay(transform.position, hit.collider.transform.position - transform.position, Color.red);
                gameObject.transform.LookAt(hit.collider.transform);
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
