using UnityEngine;
using FairyGUI;

public class FairyGUITest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GRoot.inst.SetContentScaleFactor(500, 300);
        UIPackage.AddPackage("FGUI/test1/DemoTest");
        GComponent component = UIPackage.CreateObject("Demo01", "One").asCom;
        // component.MakeFullScreen();
        component.AddRelation(GRoot.inst, RelationType.Size);
        GRoot.inst.AddChild(component);
    }

}
