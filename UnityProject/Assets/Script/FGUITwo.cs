using System.Collections;
using UnityEngine;
using FairyGUI;

public class FGUITwo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GRoot.inst.SetContentScaleFactor(800, 600);
        UIPackage.AddPackage("FGUI/test2/Demo02");
        GComponent mainCom = UIPackage.CreateObject("Demo02", "Two").asCom;
        mainCom.AddRelation(GRoot.inst, RelationType.Size);
        GRoot.inst.AddChild(mainCom);

        GComponent bossCom = UIPackage.CreateObject("Demo02", "Boss").asCom;
        GGroup group = mainCom.GetChild("n3").asGroup;
        mainCom.GetChild("n0").onClick.Add(()=>PlayUI(group, bossCom));
    }

    private IEnumerator BossCome(GGroup group, GComponent targetCom)
    {
        yield return new WaitForSeconds(1);
        targetCom.AddRelation(GRoot.inst, RelationType.Size);
        GRoot.inst.AddChild(targetCom);
        Transition t = targetCom.GetTransition("t1");
        t.Play(()=>{
            GRoot.inst.RemoveChild(targetCom);
            StartCoroutine(BackStart(group));
        });
    }

    private IEnumerator BackStart(GGroup group)
    {
            yield return new WaitForSeconds(1);
            group.visible = true;
    }

    private void PlayUI(GGroup group, GComponent targetCom)
    {
        group.visible = false;
        StartCoroutine(BossCome(group, targetCom));
    }
}
