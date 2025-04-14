using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Grid gridController;
    
    // Start is called before the first frame update
    void Start()
    {
        gridController.Init(4, 4);   
    }
}
