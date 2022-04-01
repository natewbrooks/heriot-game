using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Slider slider;
    [SerializeField] Health objHealth;
    void Start()
    {
        slider.maxValue = objHealth.MaxVigor;
        slider.value = objHealth.Vigor;
        transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y + 1f, transform.parent.position.z);
    }

    void Update()
    {
        slider.value = objHealth.Vigor;
        if(!objHealth.Kinetic) {
            transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y + 1f, transform.parent.position.z);
        }

    }
}
