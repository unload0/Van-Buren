using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    private int selectedWeaponIndex = 0;
    private List<GameObject> weaponCache = new List<GameObject>();

    void Awake()
    {
        InitializeWeapons();
        PopulateWeaponCache();
        SelectWeapon(selectedWeaponIndex);
    }

    void InitializeWeapons()
    {
        GameObject firstGun = this.transform.GetChild(0).gameObject;
        firstGun.SetActive(true);

        foreach (Transform child in transform)
        {
            if (child != firstGun)
                child.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        int previousIndex = selectedWeaponIndex;

        HandleScrollInput();
        HandleNumberInput();

        if (previousIndex != selectedWeaponIndex)
        {
            SelectWeapon(selectedWeaponIndex);
        }
    }

    private void PopulateWeaponCache()
    {
        weaponCache.Clear();
        foreach (Transform child in transform)
        {
            weaponCache.Add(child.gameObject);
        }
    }

    private void HandleScrollInput()
    {
        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            selectedWeaponIndex = (selectedWeaponIndex + 1) % weaponCache.Count;
        }
        else if (scroll < 0f)
        {
            selectedWeaponIndex = (selectedWeaponIndex - 1 + weaponCache.Count) % weaponCache.Count;
        }
    }

    private void HandleNumberInput()
    {
        if (Input.anyKeyDown)
        {
            for (int i = 0; i < weaponCache.Count; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                {
                    selectedWeaponIndex = i;
                    break;
                }
            }
        }
    }

    private void SelectWeapon(int index)
    {
        if (index < 0 || index >= weaponCache.Count) return;

        for (int i = 0; i < weaponCache.Count; i++)
        {
            if (weaponCache[i].TryGetComponent<Weapon>(out Weapon weapon))
            {
                if (weapon.owned)
                    weaponCache[i].SetActive(i == index);
            }
        }
    }
}
