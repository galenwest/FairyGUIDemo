using UnityEngine;
using FairyGUI;

public class FGUISeven_Window : Window
{
    public GameObject player;
    public FGUISeven_Window(GameObject player)
    {
        this.player = player;
    }

    protected override void OnInit()
    {
        this.contentPane = UIPackage.CreateObject("Demo07", "PlayerWindow").asCom;
        GGraph holder = contentPane.GetChild("n2").asGraph;
        RenderTexture renderTexture = Resources.Load<RenderTexture>("FGUI/test7/3DRenderTexture");
        Material mat = Resources.Load<Material>("FGUI/test7/3DPlayerMat");
        Image img = new Image();
        img.texture = new NTexture(renderTexture);
        img.material = mat;
        holder.SetNativeObject(img);
        this.contentPane.GetChild("n3").onClick.Add(RotateLeft);
        this.contentPane.GetChild("n4").onClick.Add(RotateRight);
    }

    private void RotateLeft()
    {
        player.transform.Rotate(Vector3.up * 30, Space.World);
    }

    private void RotateRight()
    {
        player.transform.Rotate(-Vector3.up * 30, Space.World);
    }
}
