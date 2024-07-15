using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LeverButtonSwitch : XRBaseInteractable
{
    // You can add a UnityEvent or any other delegate if you want to use Unity's event system
    public delegate void ButtonPressed();
    public event ButtonPressed OnButtonPressed;

    private void Start()
    {
        // Optional: Add a listener to the event
        OnButtonPressed += HandleButtonPressed;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        Debug.Log("Button Pressed!");

        // Invoke the button pressed event
        OnButtonPressed?.Invoke();
    }

    private void HandleButtonPressed()
    {
        // Add the functionality you want to trigger when the button is pressed
        Debug.Log("Button was pressed. HandleButtonPressed() triggered.");
        // Example: Call a function
        YourFunction();
    }

    private void YourFunction()
    {
        // Implement your functionality here
        Debug.Log("YourFunction was called.");
    }

    private void OnDestroy()
    {
        // Remove the listener to prevent memory leaks
        OnButtonPressed -= HandleButtonPressed;
    }
}
