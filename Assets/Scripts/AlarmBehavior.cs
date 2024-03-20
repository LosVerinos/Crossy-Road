using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AlarmBehaviour : MonoBehaviour
{
    public GameObject activated;
    public GameObject notActivated;

    // Méthode pour faire disparaître 'lap-train' et apparaître 'not-lap-train'
    public void Alarm()
    {

        for(int i=0; i<10; i++){
            Activate();

        }
    }

    private IEnumerator Activate(){
        yield return new WaitForSeconds(0.5f);
        activated.IsDestroyed();

    }
    private IEnumerator Deactivate(){
        yield return new WaitForSeconds(0.5f);
        activated.SetActive(true);
        notActivated.SetActive(false);
    }
}
