using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Weapon : MonoBehaviour
{
    [SerializeField] BaseWeapon currentWeapon;
    [SerializeField] List<string> m_Address;
    AsyncOperationHandle<IList<Sprite>> spriteHandle;
    SpriteRenderer spriteRenderer;
    public List<Sprite> spriteArray;
    int weaponIndex = 0;

    void Start()
    {
        spriteHandle = Addressables.LoadAssetsAsync<Sprite>(m_Address, 
            spriteAsset => 
            {
                spriteArray.Add(spriteAsset);
            },
            Addressables.MergeMode.Union,
            false
        );
        // spriteHandle.Completed += LoadSpritesWhenReady;

        spriteRenderer = GetComponent<SpriteRenderer>();
        Player_Equipment.swapWeapon = SwapWeapon;
    }

    /* Leaving this here in case for future reference */
    // void LoadSpritesWhenReady(AsyncOperationHandle<IList<Sprite>> handleToCheck)
    // { 
    //     if(handleToCheck.Status == AsyncOperationStatus.Succeeded)
    //     {   
    //         // Debug.Log(handleToCheck.Result);
    //         foreach(Sprite sprite in handleToCheck.Result)
    //         {
    //             Debug.Log(sprite.name);
    //         }
    //         // spriteArray = handleToCheck.Result;
    //     }
    // }

    void OnDestroy()
    {
        // spriteHandle.Completed -= LoadSpritesWhenReady;
        Addressables.Release(spriteHandle);
    }

    void SwapWeapon()
    {
        spriteRenderer.sprite = spriteArray[weaponIndex];
        weaponIndex++;
        if(weaponIndex > spriteArray.Count - 1)
        {
            weaponIndex = 0;
        }
    }
}
