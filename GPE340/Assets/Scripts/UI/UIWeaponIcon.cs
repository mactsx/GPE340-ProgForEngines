using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponIcon : MonoBehaviour
{
    public Image weaponIconImage;

    void Update()
    {
        if (GameManager.instance.player != null
            && GameManager.instance.player.pawn != null
            && GameManager.instance.player.pawn.weapon != null
            && GameManager.instance.player.pawn.weapon.weaponSprite != null)
        {
            weaponIconImage.enabled = true;
            weaponIconImage.sprite = GameManager.instance.player.pawn.weapon.weaponSprite;
        }
        else
        {
            weaponIconImage.enabled = false;
        }
    }
}
