using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHint : MonoBehaviour {

    public GameObject board;
    public GameObject Interact;
    public GameObject Jump;
    public GameObject Minimap;

    public void setInteractVisible(bool isVisible)
    {
        board.SetActive(isVisible);
        Interact.SetActive(isVisible);
    }

    public void setJumpVisible(bool isVisible)
    {
        board.SetActive(isVisible);
        Jump.SetActive(isVisible);
    }

    public void setMinimapVisible(bool isVisible)
    {
        board.SetActive(isVisible);
        Minimap.SetActive(isVisible);
    }
}
