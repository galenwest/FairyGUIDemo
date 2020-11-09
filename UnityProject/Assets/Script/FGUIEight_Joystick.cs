using UnityEngine;
using FairyGUI;

public class FGUIEight_Joystick : EventDispatcher
{
    // 事件监听
    public EventListener onMove { get; private set; }
    public EventListener onEnd { get; private set; }

    private GObject touchArea;

    private GComponent joystick;
    private GGroup thumb;
    private Controller thumbController;
    private GImage rod;
    private GImage background;
    private GImage direction;

    // 摇杆属性
    private float initX;
    private float initY;
    private int touchID;

    enum Status { Walk, Run };

    public FGUIEight_Joystick(GComponent mainUI)
    {
        touchID = -1;

        onMove = new EventListener(this, "onMove");
        onEnd = new EventListener(this, "onEnd");
        touchArea = mainUI.GetChild("n4");
        joystick = mainUI.GetChild("joystick").asCom;
        thumb = joystick.GetChild("thumb").asGroup;
        thumbController = joystick.GetController("c1");
        rod = joystick.GetChild("center").asImage;
        background = joystick.GetChild("background").asImage;
        direction = joystick.GetChild("direction").asImage;

        thumbController.SetSelectedIndex(0);

        touchArea.onTouchBegin.Add(OnTouchBegin);
        touchArea.onTouchMove.Add(OnTouchMove);
        touchArea.onTouchEnd.Add(OnTouchEnd);

        initX = touchArea.x + touchArea.width / 2 - joystick.width / 2;
        initY = touchArea.y + touchArea.height / 2 - joystick.height / 2;
    }

    private void OnTouchBegin(EventContext context)
    {
        if (touchID == -1)
        {
            InputEvent input = (InputEvent)context.data;
            touchID = input.touchId;

            Vector2 localPos = GRoot.inst.GlobalToLocal(new Vector2(input.x, input.y));
            float posX = localPos.x;
            float posY = localPos.y;
            thumbController.SetSelectedIndex(1);

            joystick.SetXY(posX - joystick.width / 2, posY - joystick.height / 2);
            rod.SetXY(joystick.width / 2 - rod.width / 2, joystick.height / 2 - rod.height / 2);
            direction.rotation = 0;
            context.CaptureTouch();
        }
    }

    private void OnTouchMove(EventContext context)
    {
        InputEvent input = (InputEvent)context.data;
        if (touchID != -1 && input.touchId == touchID)
        {
            Vector2 localPos = GRoot.inst.GlobalToLocal(new Vector2(input.x, input.y));
            float posX = localPos.x;
            float posY = localPos.y;
            // joystick的中心点就是摇杆相对的开始位置
            float posDeltaX = posX - (joystick.x + joystick.width / 2);
            float posDeltaY = posY - (joystick.y + joystick.height / 2);
            float distance = Mathf.Sqrt(posDeltaX * posDeltaX + posDeltaY * posDeltaY) + (rod.width / 2);
            // 弧度
            float rad = Mathf.Atan2(posDeltaY, posDeltaX);
            // 角度
            float degree = (rad * 180 / Mathf.PI) + 90;
            direction.rotation = degree;
            float distanceDifference = Mathf.Abs(distance) - Mathf.Abs(background.width / 2);
            if (distanceDifference <= 0)
            {
                float rodX = posX - (joystick.x + rod.width / 2);
                float rodY = posY - (joystick.y + rod.height / 2);
                rod.SetXY(rodX, rodY);
                string msg = degree + "," + Status.Walk;
                onMove.Call(msg);
            }
            else
            {
                float deltaX = distanceDifference * Mathf.Cos(rad);
                float deltaY = distanceDifference * Mathf.Sin(rad);
                float rodX = posX - (joystick.x + rod.width / 2) - deltaX;
                float rodY = posY - (joystick.y + rod.height / 2) - deltaY;
                rod.SetXY(rodX, rodY);
                string msg = degree + "," + Status.Run;
                onMove.Call(msg);
            }
        }
    }

    private void OnTouchEnd(EventContext context)
    {
        InputEvent input = (InputEvent)context.data;
        if (touchID != -1 && input.touchId == touchID)
        {
            touchID = -1;
            thumbController.SetSelectedIndex(0);
            joystick.SetXY(initX, initY);
            rod.SetXY(joystick.width / 2 - rod.width / 2, joystick.height / 2 - rod.height / 2);
            direction.rotation = 0;
            onEnd.Call();
        }
    }
}
