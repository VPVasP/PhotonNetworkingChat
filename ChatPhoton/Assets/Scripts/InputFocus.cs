using System.Collections;
using System.Collections.Generic;
using UnityEngine;       
using UnityEngine.EventSystems;
                                                                            
// keeps track of chat input focus state. useful for keeping keypresses from triggering other functions when chatting. 
public class InputFocus : MonoBehaviour, ISelectHandler, IDeselectHandler {
    public static bool chatHasFocus = false;
 
    public void OnSelect(BaseEventData data) {
        Debug.Log("selected");  
        chatHasFocus = true;
    }   
    
    public void OnDeselect(BaseEventData data) {
        Debug.Log("Deselected!");     
        chatHasFocus = false;
    }
}