using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateAbout : MonoBehaviour {
    public Image image;
    public void ActivateAboutText() {
        if (image.isActiveAndEnabled) {
            image.gameObject.SetActive(false);
        }
        else {
            image.gameObject.SetActive(true);
        }
        
    }
}
