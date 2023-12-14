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
    private void OnTriggerEnter(Collider other) {
        Debug.Log("ffgbhgfcv");
        if(other.GetComponent<Weapon>() != null){
            currentWeapon = other.GetComponent<Weapon>();
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

    private void OnTriggerExit(Collider other) {
        Invoke("finUI", 3);
    }

    private void SetNumberShotgun(){
        _pointsShotgun.text = _playerInfos.points.ToString();
        _restantShotgun.text = (_playerInfos.points - 100).ToString();
        if((_playerInfos.points - 100)<0){
            _restantShotgun.text = "Pas assez de points";
        } 
    }
    private void SetNumberHandgun(){
        _pointsHandgun.text = _playerInfos.points.ToString();
        _restantHandgun.text = (_playerInfos.points - 50).ToString();
        if((_playerInfos.points - 50)<0){
            _restantHandgun.text = "Pas assez de points";
            
        }
    }
    private void SetNumberSword(){
        _pointsSword.text = _playerInfos.points.ToString();
        _restantSword.text = (_playerInfos.points - 10000).ToString();
        if((_playerInfos.points - 10000)<0){
            _restantSword.text = "Pas assez de points";
        }
    }

    private void BuyShotgun(){
        if((_playerInfos.points - 100)>=0){
          currentWeapon.weaponInfos.owned = true;
        _uiAchete.SetActive(true);
        _uiPending.SetActive(false);
        _uiShotgun.SetActive(false);
        _uiHandgun.SetActive(false);
        _uiEpee.SetActive(false);  
        _playerInfos.points = _playerInfos.points - 100;
        }
        
    }

    private void BuyHandgun(){
        if((_playerInfos.points - 50)>=0){
          currentWeapon.weaponInfos.owned = true;
        _uiAchete.SetActive(true);
        _uiPending.SetActive(false);
        _uiShotgun.SetActive(false);
        _uiHandgun.SetActive(false);
        _uiEpee.SetActive(false);  
        _playerInfos.points = _playerInfos.points - 50;
        }
    }

    private void BuySword(){
        if((_playerInfos.points - 10000)>=0){
          currentWeapon.weaponInfos.owned = true;
        _uiAchete.SetActive(true);
        _uiPending.SetActive(false);
        _uiShotgun.SetActive(false);
        _uiHandgun.SetActive(false);
        _uiEpee.SetActive(false);  
        _playerInfos.points = _playerInfos.points - 10000;
        }
    }
}
