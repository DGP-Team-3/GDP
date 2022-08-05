using UnityEngine;
using TMPro;

public class IDHandler : MonoBehaviour
{
    [SerializeField] TMP_Text normalText;
    [SerializeField] TMP_Text specialText;
    [SerializeField] TMP_Text rehomeText;


    public void UpdateIDData()
    {
        int normalFound = GameManager.Instance.GetNumNormalCatsFound();
        int specialFound = GameManager.Instance.GetNumSpecialCatsFound();
        int rehomed = GameManager.Instance.GetNumCatsRehomed();

        if (normalFound < 10)
        {
            normalText.text = "0" + normalFound;
        }
        else
        {
            normalText.text = "" + normalFound;
        }

        if (specialFound < 10)
        {
            specialText.text = "0" + specialFound;
        }
        else
        {
            specialText.text = "" + specialFound;
        }

        if (rehomed < 10)
        {
            rehomeText.text = "0" + rehomed;
        }
        else
        {
            rehomeText.text = "" + rehomed;
        }
    }
}
