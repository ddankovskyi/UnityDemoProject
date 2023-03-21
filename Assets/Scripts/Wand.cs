using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour
{

    [SerializeField] Projectile projectailPrefab;
    [SerializeField] Transform firePoint;

    [SerializeField] SpellsPool spells;
    [SerializeField] float castDelay = 0.1f;
    [SerializeField] float rechargeTime = 0.5f;
    [SerializeField] int spellsPerShoot = 1;
    bool _readyToShoot = true;

    public void Shoot()
    {
        if (!_readyToShoot)
            return;
        
        bool shouldRecharge = false;
        for (int i = 0; i < spellsPerShoot; i++)
        {
            Instantiate(spells.NextSpell(ref shouldRecharge), firePoint.position, transform.rotation); // TODO use pooling
            if (shouldRecharge)
                break;
        }
        StartCoolDown(shouldRecharge ? rechargeTime : castDelay);
    }

    void StartCoolDown(float delaySec)
    {
        StartCoroutine(StartCoolDownCoroutine(delaySec));
    }

    IEnumerator StartCoolDownCoroutine(float delaySec)
    {
        _readyToShoot = false;
        yield return new WaitForSeconds(delaySec);
        _readyToShoot = true;
    }
}
