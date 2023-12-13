using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AcheteWeapon : MonoBehaviour
{
    
    private Weapon currentWeapon;

    [Space(5)]
    [Header("Joueur")]
    [SerializeField] private PlayerInfos _playerInfos;
    
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

    [Space(5)]
    [Header("Sword")]
    [Space(5)]
    [SerializeField] private TMP_Text _pointsSword;
    [SerializeField] private TMP_Text _prixSword;
    [SerializeField] private TMP_Text _restantSword;

    [SerializeField] private Button _btnSword;


    

    

    // Start is called before the first frame update
    void Start()
    {
        finUI();
        SetNumberHandgun();
        SetNumberShotgun();
        SetNumberSword();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void finUI(){
        _uiAchete.SetActive(false);
        _uiPending.SetActive(true);
        _uiShotgun.SetActive(false);
        _uiHandgun.SetActive(false);
        _uiEpee.SetActive(false);
    }
    private void OnCollisionEnter(Collision other) {
        
        if(other.transform.GetComponent<Weapon>() != null){
            currentWeapon = other.transform.GetComponent<Weapon>();
            if(!currentWeapon.weaponInfos.owned ){
                if(currentWeapon.weaponInfos.weaponName=="Shotgun"){
                    _uiAchete.SetActive(false);
                    _uiPending.SetActive(false);
                    _uiShotgun.SetActive(true);
                    _uiHandgun.SetActive(false);
                    _uiEpee.SetActive(false); 
                }
                else if(currentWeapon.weaponInfos.weaponName=="Handgun"){
                    _uiAchete.SetActive(false);
                    _uiPending.SetActive(false);
                    _uiShotgun.SetActive(false);
                    _uiHandgun.SetActive(true);
                    _uiEpee.SetActive(false); 
                }
                else if(currentWeapon.weaponInfos.weaponName=="Sword"){
                    _uiAchete.SetActive(false);
                    _uiPending.SetActive(false);
                    _uiShotgun.SetActive(false);
                    _uiHandgun.SetActive(false);
                    _uiEpee.SetActive(true); 
                }
            }
            else if(currentWeapon.weaponInfos.owned ){
                    _uiAchete.SetActive(true);
                    _uiPending.SetActive(false);
                    _uiShotgun.SetActive(false);
                    _uiHandgun.SetActive(false);
                    _uiEpee.SetActive(false); 
            }
            
        
        }
        
        
    }

    private void OnCollisionExit(Collision other) {
        Invoke("finUI", 3);
    }

    private void SetNumberShotgun(){
        _pointsShotgun = _playerInfos.points + "";
        _restantShotgun = _playerInfos.points - 100 + "";
        if(_restantShotgun<0){
            _restantShotgun = "Pas assez de points";
        } 
    }
    private void SetNumberHandgun(){
        _pointsHandgun = _playerInfos.points + "";
        _restantHandgun = _playerInfos.points - 50 + "";
        if(_restantHandgun<0){
            _restantHandgun = "Pas assez de points";
        }
    }
    private void SetNumberSword(){
        _pointsSword = _playerInfos.points + "";
        _restantSword = _playerInfos.points - 10000 + "";
        if(_restantSword<0){
            _restantSword = "Pas assez de points";
        }
    }
}
