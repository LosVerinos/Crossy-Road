using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaracterChoosing : MonoBehaviour
{

    [SerializeField] private List<SkinData> skinData = new();
    [SerializeField] private Transform parentTransform;
    // Start is called before the first frame update
    void Start()
    {
        int whichSkin = Random.Range(0, skinData.Count);
        GameObject player = Instantiate(skinData[whichSkin].Model, parentTransform);
        Debug.Log(skinData[whichSkin].Model.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
