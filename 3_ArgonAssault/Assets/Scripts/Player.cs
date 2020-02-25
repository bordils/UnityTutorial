using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("in ms^-1")]
    [SerializeField] float xSpeed = 4f;

    [Tooltip("in m")]
    [SerializeField] float xRange = 5f;

    [Tooltip("in ms^-1")]
    [SerializeField] float ySpeed = 4f;

    [Tooltip("in m")]
    [SerializeField] float yRange = 5f;

    [SerializeField] float positionPitchFactor = 0.5f;
    [SerializeField] float controlPitchFactor = 30f;
    [SerializeField] float positionyawFactor = 0.5f;
    [SerializeField] float controlrollFactor = 30f;

    float xThrow, yThrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        float pitch = - transform.localPosition.y * positionPitchFactor - yThrow * controlPitchFactor;
        float yaw = transform.localPosition.x * positionyawFactor;
        float roll =  - xThrow * controlrollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float yOffset = yThrow * ySpeed * Time.deltaTime;

        float xPos = xOffset + transform.localPosition.x;
        float yPos = yOffset + transform.localPosition.y;

        xPos = Mathf.Clamp(xPos, -xRange, xRange);
        yPos = Mathf.Clamp(yPos, -yRange, yRange);

        transform.localPosition = new Vector3(xPos, transform.localPosition.y, transform.localPosition.z);
        transform.localPosition = new Vector3(transform.localPosition.x, yPos, transform.localPosition.z);
    }
}
