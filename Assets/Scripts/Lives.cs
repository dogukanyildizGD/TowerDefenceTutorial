using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    public Text liveText;

    // Update is called once per frame
    void Update()
    {
        liveText.text = PlayerStats.Live + " LIVES";
    }
}
