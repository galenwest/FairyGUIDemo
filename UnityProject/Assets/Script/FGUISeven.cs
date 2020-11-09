using UnityEngine;
using FairyGUI;

public class FGUISeven : MonoBehaviour
{
    public GameObject player;
    private GComponent mainUI;
    private FGUISeven_Window playerWindow;

    // Start is called before the first frame update
    void Start()
    {
        mainUI = GetComponent<UIPanel>().ui;
        playerWindow = new FGUISeven_Window(player);
        mainUI.GetChild("n0").onClick.Add(()=>{
            playerWindow.Show();
        });
    }
}
