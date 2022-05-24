using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestActivation : MonoBehaviour
{
    EventSystem m_EventSystem;
    // Start is called before the first frame update
    void Awake()
    {
        m_EventSystem = EventSystem.current;
    }

    private void OnEnable()
    {
        m_EventSystem.SetSelectedGameObject(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
