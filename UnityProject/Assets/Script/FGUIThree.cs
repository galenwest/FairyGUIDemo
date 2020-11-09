using System.Collections;
using UnityEngine;
using FairyGUI;

public class FGUIThree : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GComponent mainUI = GetComponent<UIPanel>().ui;
        // GGroup button = mainUI.GetChild("n2").asGroup;
        // button.onClick.Add(MyClick);
        mainUI.GetChild("n0").onClick.Add(MyClick);
    }

    private void MyClick()
    {
        print("MyClick");
    }

}
