using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChoiceButtonScript : MonoBehaviour
{
    string[] goodOptions = { "Talk about D&D", "Share pet pics", "Ask others about their projects"};
    string[] badOptions = {"Try to sell your mixtape","Share the results of your online IQ test","Talk about your slug collection" };

    public BlendedFlocking[] flockers;
    public Button goodButton;
    public Button badButton;

    // Start is called before the first frame update
    void Start()
    {
        goodButton.GetComponentInChildren<TextMeshProUGUI>().text = goodOptions[0];
        badButton.GetComponentInChildren<TextMeshProUGUI>().text = badOptions[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attract()
    {
        foreach (BlendedFlocking f in flockers)
        {
            f.avoidObstacles = false;
        }
        goodButton.GetComponentInChildren<TextMeshProUGUI>().text = goodOptions[Random.Range(0, 2)];
    }

    public void Repel()
    {
        foreach (BlendedFlocking f in flockers)
        {
            f.avoidObstacles = true;
        }
        badButton.GetComponentInChildren<TextMeshProUGUI>().text = badOptions[Random.Range(0, 2)];
    }
}
