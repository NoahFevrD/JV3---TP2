using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AcheteWeapon : MonoBehaviour
{
    
    [Header("Arme")]
    [Space(5)] 
    [SerializeField] private Weapon currentWeapon;
    
    [Space(5)]
    [Header("Canvas")]
    [Space(5)]
    [SerializeField] private GameObject _uiPending;
    [SerializeField] private GameObject _uiAchete;
    [SerializeField] private GameObject _uiShotgun;
    [SerializeField] private GameObject _uiHandgun;
    [SerializeField] private GameObject _uiEpee;

    [Space(5)]
    [Header("Shotgun")]
    [Space(5)]
    [SerializeField] private TMP_Text _pointsShotgun;
    [SerializeField] private TMP_Text _prixShotgun;
    [SerializeField] private TMP_Text _restantShotgun;
    [SerializeField] private Button _btnShotgun;

    [Space(5)]
    [Header("Handgun")]
    [Space(5)]
    [SerializeField] private TMP_Text _pointsHandgun;
    [SerializeField] private TMP_Text _prixHandgun;
    [SerializeField] private TMP_Text _restantHandgun;

    [SerializeField] private Button _btnHandgun;


    

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {

        if(other.GetComponent<Weapon>() != null){
            currentWeapon = other.GetComponent<Weapon>();
            if(!currentWeapon.weaponInfos.owned ){
            
            }
            else{

            }
        }
             
    }
}
