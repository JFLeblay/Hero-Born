using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningCurve : MonoBehaviour
{
    // Public properties
    public Transform canTransform;
    public GameObject directionLight;
    private Transform lightTransform;

    // Start is called before the first frame update
    void Start()
    {
        Character hero = new Character();
        hero.PrintStatsInfo();

        Character heroine = new Character("Agatha");
        hero.PrintStatsInfo();

        Weapon huntingBow = new Weapon("Hunting Bow", 105);

        canTransform = this.GetComponent<Transform>();
        Debug.Log(canTransform.localPosition);

        // directionLight = GameObject.Find("Directional Light");
        lightTransform = directionLight.GetComponent<Transform>();
        Debug.Log(lightTransform.localPosition);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
