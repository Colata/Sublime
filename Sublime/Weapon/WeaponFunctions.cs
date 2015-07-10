﻿using GTA;
using GTA.Native;
using GTA.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

class WeaponFunctions : Script
{
    public static bool HasWeaponInfiniteAmmo { get; set; }

    public WeaponFunctions()
    {
        Tick += OnTick;
        Game.Player.Character.Weapons.Current.InfiniteAmmoClip = false;
    }
    private void OnTick(object sender, EventArgs e)
    {
        if (HasWeaponInfiniteAmmo)
        {
            Weapon playerWeapon = Game.Player.Character.Weapons.Current;

            playerWeapon.InfiniteAmmoClip = true;
            if (playerWeapon.AmmoInClip != playerWeapon.MaxAmmoInClip)
            {
                playerWeapon.AmmoInClip = playerWeapon.MaxAmmoInClip;
            }
        }

    }

    internal static void GiveAllWeapons()
    {
        foreach (WeaponHash weaponHash in Enum.GetValues(typeof(WeaponHash)))
        {
            bool hasPedGotWeapon = Function.Call<bool>(Hash.HAS_PED_GOT_WEAPON, Game.Player.Character, (int) weaponHash, false);

            if (!hasPedGotWeapon)
            {
                Game.Player.Character.Weapons.Give(weaponHash, 9999, false, false);
            }
            else
            {
                Function.Call(Hash.SET_PED_AMMO, Game.Player.Character, (int) weaponHash, 9999);
            }
        }
        
        Sublime.DisplayMessage("All weapons received");
    }

    internal static void ToggleInfiniteAmmo()
    {
        HasWeaponInfiniteAmmo = !HasWeaponInfiniteAmmo;

        if (!HasWeaponInfiniteAmmo)
            Game.Player.Character.Weapons.Current.InfiniteAmmoClip = false;

        Sublime.DisplayMessage("Infinite Ammo", HasWeaponInfiniteAmmo);
    }

    internal static void GiveWeaponComponent(WeaponHash weaponHash, int weaponComponentHash)
    {
        Function.Call(Hash.GIVE_WEAPON_COMPONENT_TO_PED, Game.Player.Character, (int) weaponHash, weaponComponentHash);
    }

}

