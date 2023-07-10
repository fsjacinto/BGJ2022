using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : EnvironmentObject
{
    protected virtual void Update() {
        if(!CheckInteractInput()) return;

        if (CheckInteractConditions()) {
            Interact();
        }
    }

    public bool CheckInteractInput() {
        if (Input.GetKeyDown(KeyCode.Space) &&
            isPlayerColliding)
            return true;
        return false;
    }

    public virtual bool CheckInteractConditions() {
        if (GameManager.Instance.currentState == GameState.Exploration)
            return true;
        return false;
    }

    public virtual void Interact() {
        
    }
}
