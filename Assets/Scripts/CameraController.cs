using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private int riseTreshold = 0;
    void OnEnable()
    {
        GameManager.NextFloor += RiseUp;
    }
    void OnDisable()
    {
        GameManager.NextFloor -= RiseUp;
    }

    void RiseUp()
    {
        if (riseTreshold > 3)
        {
            StartCoroutine("Rise");
        }
        else
            riseTreshold++;
    }

    IEnumerator Rise()
    {
        float totalRise = Floor.CurrentFloor.transform.localScale.y;
        float actualRise = 0;
        float animationDuration = 0.3f;
        while (actualRise < totalRise)
        {
            var r = totalRise * Time.deltaTime * (1 / animationDuration);
            actualRise += r;
            transform.position += new Vector3(0, r, 0);
            yield return null;
        }
        transform.position += new Vector3(0, totalRise - actualRise, 0);
    }
}
