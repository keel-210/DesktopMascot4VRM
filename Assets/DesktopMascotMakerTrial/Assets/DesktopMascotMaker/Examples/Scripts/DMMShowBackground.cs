using UnityEngine;

public class DMMShowBackground : MonoBehaviour
{
    public GameObject background;

    private int selGridInt = 0;
    private string[] selCaptions = new string[] {
		" Hide background ",
		" Show background ",
	};

    void Start() { }

    void OnGUI()
    {
        int selGridIntTmp = GUILayout.SelectionGrid(selGridInt, selCaptions, 1, "Toggle");

        if (selGridInt != selGridIntTmp)
        {
            selGridInt = selGridIntTmp;

            switch (selGridInt)
            {
                case 0:
                    if (background != null)
                        background.SetActive(false);
                    break;
                case 1:
                    if (background != null)
                        background.SetActive(true);
                    break;
            }
        }
    }
}
