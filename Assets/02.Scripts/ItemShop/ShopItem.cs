using UnityEngine;
using UnityEngine.Assertions.Must;

public class ShopItem
{
    private IUnityServiceManager UnityService;
    public GameObject[] guns;
    public int costLowerRange = 900;
    public int costUpperRange = 1100;
    public int money;
    public ImItem item;

    public ShopItem Instantiate()
    {
        UnityService = new UnityServiceManager();
        Debug.Log(UnityService.ToString());
        this.item = CreateWeapon();
        this.money = UnityService.UnityRandomRange(costLowerRange, costUpperRange);
        return this;
    }

    public ImWeapon CreateWeapon()
    {
        var gun = guns[1].GetComponent<ImWeapon>();
        gun.Damage += UnityService.UnityRandomRange(0, (int)(gun.Damage * 0.1f));
        gun.MaxBullet += UnityService.UnityRandomRange(0, (int)(gun.MaxBullet * 0.1f));
        gun.InstanceID = gun.EntityID * 1000 + (int)gun.Damage;

        return gun;
    }
}
