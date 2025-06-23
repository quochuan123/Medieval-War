using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrainingUnitButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    public Text classText;
    private TrainningUnit tu;

    void Start()
    {
        tu = FindAnyObjectByType<TrainningUnit>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        foreach(var unit in tu.trainButtonList)
        {
            unit.classText.color = Color.gray;
        }
        classText.color = Color.white;
    }

    // Start is called before the first frame update


    public void OnPointerEnter(PointerEventData eventData)
    {
        classText.color = Color.white;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.selectedObject == gameObject)
            classText.color = Color.white;
        else
            classText.color = Color.gray;
    }
}
