using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameWin : Interactable
{
    public NavMeshAgent agent;
    public int layer;

    public override void OnMouseDown()
    {
        clicked = true;
        NavMeshHit hit;
        agent.SamplePathPosition(1, 1000, out hit);
        if (hit.mask == layer) 
        {
            Cursor.SetCursor(clickTex, Vector2.zero, CursorMode.Auto);
            agent.SetDestination(gameObject.transform.position);
        }
        else 
        {
            Cursor.SetCursor(denyTex, Vector2.zero, CursorMode.Auto);
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("You Win!");
        agent.isStopped = true;
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

