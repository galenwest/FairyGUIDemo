using UnityEngine;
using FairyGUI;

public class FGUIFive : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GComponent mainUI = GetComponent<UIPanel>().ui;
        GComponent guideLayer = UIPackage.CreateObject("Demo05", "Component1").asCom;
        guideLayer.SetSize(GRoot.inst.width, GRoot.inst.height);
        guideLayer.AddRelation(GRoot.inst, RelationType.Size);

        GObject button = mainUI.GetChild("n0");
        button.onClick.Add(()=>{
            guideLayer.RemoveFromParent();
        });
        mainUI.GetChild("n1").onClick.Add(()=>{
            GRoot.inst.AddChild(guideLayer);
            Rect rect = new Rect(button.position.x, button.position.y, button.width, button.height);
            GObject theShow = guideLayer.GetChild("n1");
            theShow.size = new Vector2(rect.size.x, rect.size.y);
            theShow.TweenMove(new Vector2(rect.position.x, rect.position.y), 0.5f);
        });
    }

}
