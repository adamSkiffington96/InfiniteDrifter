using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnyScript : MonoBehaviour
{
    public Text debugText;

    private Rigidbody rigidbody;


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        Example1();

        Example2();
    }

    private void Example1()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            debugText.text = "Pressing Space!";
        }
    }

    private void Example2()
    {
        string nextText = "velocity: " + rigidbody.velocity.ToString();

        debugText.text = nextText;
    }
}
