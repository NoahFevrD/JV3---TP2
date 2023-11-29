using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoadScene : MonoBehaviour
{
    [SerializeField] private SceneExtManager _sceneExtManagerDepart;
    [SerializeField] private Vector3 _positionPop;
    [SerializeField] private GameObject _positionMenu;
    [SerializeField] private GameObject _positionMag;
    [SerializeField] private GameObject _positionBoss;
    [SerializeField] private GameObject _positionDonj;
    private Transform menu;
    private Transform mag;
    private Transform boss;
    private Transform donjon;
    [SerializeField] private string _arriveDe;
    // Start is called before the first frame update
    void Start()
    {
        donnePosition();
        _arriveDe = _sceneExtManagerDepart.depart;
        if(_arriveDe == "menu"){
            _positionPop = menu;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void donnePosition(){
       menu = _positionMenu.transform;
       mag = _positionMag.transform;
       boss = _positionBoss.transform;
       donjon = _positionDonj.transform;
    }
}
