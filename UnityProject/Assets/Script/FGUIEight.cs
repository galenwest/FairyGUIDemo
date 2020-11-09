using UnityEngine;
using FairyGUI;

public class FGUIEight : MonoBehaviour
{
    private GComponent mainUI;
    private GTextField gTextDegree;
    private GTextField gTextStatus;
    private FGUIEight_Joystick joystick;

    // Start is called before the first frame update
    void Start()
    {
        mainUI = GetComponent<UIPanel>().ui;
        gTextDegree = mainUI.GetChild("n6").asTextField;
        gTextStatus = mainUI.GetChild("n10").asTextField;
        joystick = new FGUIEight_Joystick(mainUI);
        joystick.onMove.Add(JoystickMove);
        joystick.onEnd.Add(JoystickEnd);
    }

    private void JoystickMove(EventContext context)
    {
        string msg = (string)context.data;
        string degree = msg.Split(',')[0];
        string status = msg.Split(',')[1];
        gTextDegree.text = degree;
        gTextStatus.text = status;
    }

    private void JoystickEnd(EventContext context)
    {
        gTextDegree.text = "";
    }

}
