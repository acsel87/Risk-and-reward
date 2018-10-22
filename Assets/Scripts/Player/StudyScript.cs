using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudyScript : MonoBehaviour {
        
    public Slider gradesSlider;

    [SerializeField]
    private float studySpeed;

    public static float grades = 50f;

    private void Update()
    {
        gradesSlider.value = grades;

        StudyGrades();
    }

    private void StudyGrades()
    {
        if (Input.GetKey(KeyCode.LeftShift) && grades < gradesSlider.maxValue)
        {
            grades += studySpeed;
        }
        else if (grades > 0)
        {
            grades -= studySpeed * 0.5f;
        }
    }
}
