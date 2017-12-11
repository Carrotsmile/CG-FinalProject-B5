using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultListController : MonoBehaviour {

    public Text results;

    public void SetResults(List<string> results)
    {
        if (results.Count > 0)
            this.results.text = string.Join("\n", results.ToArray());
        else
            this.results.text = "Nothing...";
    }
}
