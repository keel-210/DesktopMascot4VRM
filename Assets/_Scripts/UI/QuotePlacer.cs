using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuotePlacer : MonoBehaviour
{
    List<RectTransform> QuoteList = new List<RectTransform>();
    void Update()
    {

    }
    void PlaceQuote(Animator animator, HumanBodyBones PlaceBone, RectTransform panel, Vector3 PlaceOffset)
    {
        QuoteList.Add(panel);
        if (PlaceBone != HumanBodyBones.LastBone)
        {
            panel.position = Camera.main.WorldToScreenPoint(animator.GetBoneTransform(PlaceBone).position) + PlaceOffset;
        }
    }
    public void MakeQuote(Animator animator, string QuoteString)
    {
        GameObject imObj = new GameObject();
        imObj.AddComponent<RectTransform>();
        imObj.transform.parent = GameObject.Find("Canvas").transform;
        Image image = imObj.AddComponent<Image>();
        image.color = Color.white;

        GameObject texObj = new GameObject();
        RectTransform tra = texObj.AddComponent<RectTransform>();
        texObj.transform.parent = imObj.transform;
        Text text = texObj.AddComponent<Text>();
        text.supportRichText = true;
        text.font = Font.CreateDynamicFontFromOSFont("Arial", 20);
        text.text = QuoteString;
        text.color = Color.black;
        text.rectTransform.sizeDelta = new Vector2(text.preferredWidth, text.preferredHeight);
        text.rectTransform.sizeDelta = new Vector2(text.preferredWidth, text.preferredHeight);
        image.rectTransform.sizeDelta = text.rectTransform.sizeDelta + new Vector2(10, 10);

        PlaceQuote(animator, HumanBodyBones.Head, imObj.GetComponent<RectTransform>(), new Vector3(-100, 0, 0));
    }
    public void MakeQuote(Animator animator, string QuoteString,Color PanelColor)
    {
        GameObject imObj = new GameObject();
        imObj.AddComponent<RectTransform>();
        imObj.transform.parent = GameObject.Find("Canvas").transform;
        Image image = imObj.AddComponent<Image>();
        image.color = PanelColor;

        GameObject texObj = new GameObject();
        RectTransform tra = texObj.AddComponent<RectTransform>();
        texObj.transform.parent = imObj.transform;
        Text text = texObj.AddComponent<Text>();
        text.supportRichText = true;
        text.font = Font.CreateDynamicFontFromOSFont("Arial", 20);
        text.text = QuoteString;
        text.color = Color.black;
        text.rectTransform.sizeDelta = new Vector2(text.preferredWidth, text.preferredHeight);
        text.rectTransform.sizeDelta = new Vector2(text.preferredWidth, text.preferredHeight);
        image.rectTransform.sizeDelta = text.rectTransform.sizeDelta + new Vector2(10, 10);

        PlaceQuote(animator, HumanBodyBones.Head, imObj.GetComponent<RectTransform>(), new Vector3(-100,0,0));
    }
    public void MakeQuote(Animator animator, string QuoteString, Color PanelColor, Color TextColor, HumanBodyBones PlaceBone, Vector2 PlaceOffset, Vector2 PanelCollar,AnchorPresets anchor)
    {
        GameObject imObj = new GameObject();
        imObj.AddComponent<RectTransform>();
        imObj.transform.parent = GameObject.Find("Canvas").transform;
        Image image = imObj.AddComponent<Image>();
        image.color = PanelColor;

        GameObject texObj = new GameObject();
        RectTransform tra = texObj.AddComponent<RectTransform>();
        texObj.transform.parent = imObj.transform;
        Text text = texObj.AddComponent<Text>();
        text.supportRichText = true;
        text.font = Font.CreateDynamicFontFromOSFont("Arial", 20);
        text.text = QuoteString;
        text.color = TextColor;


        text.rectTransform.sizeDelta = new Vector2(text.preferredWidth, text.preferredHeight);
        text.rectTransform.sizeDelta = new Vector2(text.preferredWidth, text.preferredHeight);
        image.rectTransform.sizeDelta = text.rectTransform.sizeDelta + PanelCollar;

        PlaceQuote(animator, PlaceBone, imObj.GetComponent<RectTransform>(), PlaceOffset);
    }
}