using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ladder : Interactable
{
    public NavMeshAgent agent;
    private OffMeshLink link;

    // Start is called before the first frame update
    void Start()
    {
        link = GetComponent<OffMeshLink>(); 
    }

    public void Teleport(bool goingUp) 
    {
        link.activated = true;
        if (goingUp) 
        {
            agent.SetDestination(link.endTransform.position);
        }
        else 
        {
            agent.SetDestination(link.startTransform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (link.activated && agent.isOnOffMeshLink)
        {
            link.activated = false;
        }
        if(!hovered && Input.GetKeyDown(KeyCode.Mouse0))
        {
            link.activated = false;
        }
    }

    public override void OnMouseDown()
    {
        Cursor.SetCursor(clickTex, Vector2.zero, CursorMode.Auto);
        clicked = true;
        if (agent)
        {
            NavMeshHit hit;
            agent.SamplePathPosition(1, 1000, out hit);
            if(hit.mask == 1) //on walkable layer
            {
                Teleport(true);
            }
            else if (hit.mask == 8) //on level 2 layer
            {
                Teleport(false);
            }
        }
        StartCoroutine(switchCursor());
    }

    public override void OnMouseOver()
    {
        hovered = true;
        if (!clicked) 
        {
            Cursor.SetCursor(hoverTex, Vector2.zero, CursorMode.Auto);
        }        
    }

    public override IEnumerator switchCursor()
    {
        yield return new WaitForSeconds(0.175f);
        Cursor.SetCursor(hoverTex, Vector2.zero, CursorMode.Auto);
        clicked = false;
    }

    public override void OnMouseExit()
    {
        hovered = false;
        Cursor.SetCursor(defaultTex, Vector2.zero, CursorMode.Auto);
    }
}
