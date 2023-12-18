using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class AcheteWeapon : MonoBehaviour
{
    // -------------------------
    // Class
    // -------------------------

    [System.Serializable]
    public class CanvasShop
    {
        [Header("Canvas Shop")]
        public GameObject canvas;
        [Space(5)]

        public TextMeshProUGUI name;
        public TextMeshProUGUI points;
        public TextMeshProUGUI price;
        public TextMeshProUGUI left;
        [Space(5)]

        public Button purchase;
    }

    // -------------------------
    // Variables
    // -------------------------

    Weapon currentWeapon;

    [Space(5)]
    [Header("Joueur")]
    [SerializeField] private PlayerInfos _playerInfos;

    [Header("Canvas")]
    [Space(5)]
    [SerializeField] private GameObject _uiPending;
    [SerializeField] private GameObject _uiAchete;
    [Space(5)]

    [SerializeField] CanvasShop canvasShop;

    [Header("Components")]
    AudioSource purchase;

    // -------------------------
    // Functions
    // -------------------------

    void Start()
    {
        // Set Variables
        purchase = GetComponent<AudioSource>();

        // Call Functions
        EndUI();
    }

    private void EndUI()
    {
        _uiAchete.SetActive(false);
        _uiPending.SetActive(true);
        canvasShop.canvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Weapon>() != null){
            currentWeapon = other.GetComponent<Weapon>();

            if(!currentWeapon.weaponInfos.owned )
            {
                SetShop();
                CancelInvoke("EndUI");
            }
            

            else
            AlreadyOwned();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<Weapon>() != null)
        Invoke("EndUI", 3);
    }

    void SetShop()
    {
        // Set Variables
        _uiAchete.SetActive(false);
        _uiPending.SetActive(false);
        canvasShop.canvas.SetActive(true);

        canvasShop.purchase.interactable = true;
        canvasShop.name.text = currentWeapon.weaponInfos.name;
        canvasShop.points.text = "" + _playerInfos.points;
        canvasShop.price.text = "" +currentWeapon.weaponInfos.price;

        int left = _playerInfos.points - currentWeapon.weaponInfos.price;
        canvasShop.left.text = "" + left;

        if(_playerInfos.points < currentWeapon.weaponInfos.price)
        {
            canvasShop.purchase.interactable = false;
            canvasShop.left.text = "Points insuffisant.";
        }
    }

    void AlreadyOwned()
    {
        _uiAchete.SetActive(true);
        _uiPending.SetActive(false);
        canvasShop.canvas.SetActive(false);
    }

    public void BuyWeapon()
    {
        // Set Variables
        _playerInfos.currentWeapon = currentWeapon.weaponInfos;
        currentWeapon.weaponInfos.owned = true;

        _playerInfos.player.MoneyUp(-currentWeapon.weaponInfos.price);

        // Play Audio
        purchase.Play();

        // Call Functions
        AlreadyOwned();
    }
}