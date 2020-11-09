using UnityEngine;
using FairyGUI;
using DG.Tweening;

public class FGUIFore : MonoBehaviour
{
    private int startValue;
    private int endValue;

    // Start is called before the first frame update
    void Start()
    {
        GComponent mainUI = GetComponent<UIPanel>().ui;
        GComponent attackUP = UIPackage.CreateObject("Demo04", "AttackUp").asCom;
        attackUP.GetTransition("t1").SetHook("AddValue", ()=>AddAttackValue(attackUP));
        mainUI.GetChild("n0").onClick.Add(()=>PlayUI(mainUI, attackUP));
    }

    private void PlayUI(GComponent mainUI, GComponent attackUP)
    {
        mainUI.GetChild("n0").visible = false;
        GRoot.inst.AddChild(attackUP);
        Transition t = attackUP.GetTransition("t1");
        startValue = 100000;
        int add = Random.Range(1000, 3000);
        endValue = startValue + add;
        attackUP.GetChild("n9").text = startValue.ToString();
        attackUP.GetChild("n11").text = add.ToString();
        t.Play(() => {
            mainUI.GetChild("n0").visible = true;
            GRoot.inst.RemoveChild(attackUP);
        });
    }

    private void AddAttackValue(GComponent attackUP)
    {
        DOTween.To(
            ()=>startValue,
            x=> {
                attackUP.GetChild("n9").text = Mathf.Floor(x).ToString();
            },
            endValue, 0.3f)
            .SetEase(Ease.Linear)
            .SetUpdate(true);
    }
}
