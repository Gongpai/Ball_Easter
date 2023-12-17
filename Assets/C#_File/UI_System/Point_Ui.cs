using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Point_Ui : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public TextMeshProUGUI point_text;

    private void OnEnable()
    {
        Point.AddPoint += UpdatePoint;
    }

    private void OnDisable()
    {
        Point.AddPoint -= UpdatePoint;
    }

    // Update is called once per frame
    private void UpdatePoint()
    {
        point_text.text = "Score : " + $"{Point.point}";
    }
}
