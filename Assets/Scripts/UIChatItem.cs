using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIChatItem : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text content;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetTitle(string title)
    {
        this.title.text = title;
    }

    public void SetContent(string content)
    {
        this.content.text = content;
    }
}
