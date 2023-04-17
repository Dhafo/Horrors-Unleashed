using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public Texture2D defaultTex;
    public Texture2D hoverTex;
    public Texture2D clickTex;
    public Texture2D denyTex;

    public bool hovered = false;
    public bool clicked = false;

    public abstract void OnMouseDown();
    public abstract void OnMouseOver();
    public abstract IEnumerator switchCursor();
    public abstract void OnMouseExit();
}
