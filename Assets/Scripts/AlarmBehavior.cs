using System.Collections;
using UnityEngine;

public class AlarmBehaviour : MonoBehaviour
{
    public GameObject activated;
    public GameObject notActivated;

    // Méthode pour faire disparaître 'lap-train' et apparaître 'not-lap-train'
    public IEnumerator Alarm()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return StartCoroutine(Activate());
        }
    }

    private IEnumerator Activate()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("oui");
        activated.SetActive(false);
    }

    private IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(0.5f);
        activated.SetActive(true);
        notActivated.SetActive(false);
    }
}
