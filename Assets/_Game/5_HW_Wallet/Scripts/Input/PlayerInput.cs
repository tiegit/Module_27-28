using UnityEngine;

public class PlayerInput
{
    public bool A1KeyPressed => Input.GetKeyDown(KeyCode.Alpha1);
    public bool A2KeyPressed => Input.GetKeyDown(KeyCode.Alpha2);
    public bool A3KeyPressed => Input.GetKeyDown(KeyCode.Alpha3);
    public bool A4KeyPressed => Input.GetKeyDown(KeyCode.Alpha4);
    public bool A5KeyPressed => Input.GetKeyDown(KeyCode.Alpha5);
    public bool A6KeyPressed => Input.GetKeyDown(KeyCode.Alpha6);
}