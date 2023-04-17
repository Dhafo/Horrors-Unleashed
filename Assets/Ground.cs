using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ground : Interactable
{
    private Camera cam;
    public NavMeshAgent agent;
    private void Start()
    {
        cam = Camera.main;
    }

    public override void OnMouseDown()
    {
        Ray ray = cam.ScreenPointToRay( Input.mousePosition );
        if(Physics.Raycast(ray, out RaycastHit hit)) 
        {
            Cursor.SetCursor(clickTex, Vector2.zero, CursorMode.Auto);
            agent.SetDestination(hit.point);
            StartCoroutine(switchCursor());
        }
    }

    public override IEnumerator switchCursor() 
    {
        yield return new WaitForSeconds(0.175f);
        Cursor.SetCursor(defaultTex, Vector2.zero, CursorMode.Auto);
    }

    public override void OnMouseOver()
    { }

    public override void OnMouseExit()
    { }
}
