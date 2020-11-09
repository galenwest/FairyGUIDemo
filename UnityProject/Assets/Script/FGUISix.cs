using UnityEngine;
using FairyGUI;

public class FGUISix : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GComponent mainUI = GetComponent<UIPanel>().ui;
        GList list = mainUI.GetChild("n0").asList;
        list.SetVirtualAndLoop();
        list.itemRenderer=RanderItem;
        list.numItems = 5;
        list.scrollPane.onScroll.Add(()=>DoSpecialEffect(mainUI, list));

        DoSpecialEffect(mainUI, list);
    }

    void DoSpecialEffect(GComponent _mainView, GList _list)
    {
        //change the scale according to the distance to middle
        float midX = _list.scrollPane.posX + _list.viewWidth / 2;
        // 屏幕上显示的对象
        int cnt = _list.numChildren;
        for (int i = 0; i < cnt; i++)
        {
            GObject obj = _list.GetChildAt(i);
            // 当前item跟最左边的距离
            float dist = Mathf.Abs(midX - obj.x - obj.width / 2);
            if (dist > obj.width) //no intersection
                obj.SetScale(0.8f, 0.8f);
            else
            {
                float ss = 0.8f + (1 - dist / obj.width) * 0.20f;
                obj.SetScale(ss, ss);
            }
        }

        // _mainView.GetChild("n3").text = "" + ((_list.GetFirstChildInView() + 1) % _list.numItems);
    }

    private void RanderItem(int index, GObject obj)
    {
        GButton button = obj.asButton;
        // 设置缩放锚点在中心
        button.SetPivot(0.5f, 0.5f);
        button.icon = UIPackage.GetItemURL("Demo06", "n"+(index+1));
    }

}
