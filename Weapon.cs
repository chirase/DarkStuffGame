using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject Owner;
    public float damage;
    public enum Rarity { junk, normal, uncommun, rare, legendary};
    public enum AttackType { Swipe, Thrust };

    public TrailRenderer tr;
    public Vector2 force;
    public AttackType attType;
    public Rarity rarity;
    public GameObject infoPanel;

    public GameObject visuals;

    public int level;


    // Start is called before the first frame update
    void Start()
    {
        if (Owner != null) { 
           HideSelf();
           GetComponent<Rigidbody2D>().gravityScale = 0;
           infoPanel.GetComponent<ShowInfos>().owned = true;
           GetComponent<Animator>().enabled = true;
           GetComponent<CapsuleCollider2D>().enabled = false;
           infoPanel.GetComponent<CircleCollider2D>().enabled = true;
        }

        GetComponent<BoxCollider2D>().size = visuals.GetComponent<WeaponVisualsDatas>().weaponColliderSize;
    }

    // Update is called once per frame
    void Update()
    {
        infoPanel.transform.rotation = Quaternion.Euler(0.0f, 0.0f, gameObject.transform.rotation.z * -1.0f);
    }

    public void ShowSelf() {


        GetComponent<PlaySounds>().playSound(0);

        GetComponent<BoxCollider2D>().enabled = true;
        visuals.GetComponent<HideAndShowCollection>().ShowAll();

    }

    public void StopStaticAttack() {
        Owner.GetComponent<Player>().StopStaticAttack();
    }

    public void HideSelf() {
        GetComponent<BoxCollider2D>().enabled = false;
        visuals.GetComponent<HideAndShowCollection>().HideAll();
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (Owner != null) { 
            if (coll.tag == "Ennemy") {

                Vector2 modifiedForce = force;
                if (coll.transform.position.x < transform.position.x)
                {
                    modifiedForce.x *= -1;
                }

                modifiedForce.y = 0;

                coll.gameObject.GetComponent<Ennemy>().ReceiveDamage(damage * (1 + (level * 0.15f)), modifiedForce);

            } else if (coll.tag == "breakable") {
                coll.gameObject.GetComponent<Breakable>().Break();
            }

            if (coll.tag == "rope") {
                coll.gameObject.GetComponent<ActiveGravity>().ActivateGravity();
            }
        }
    }

    // Création d'une nouvelle arme Random dont l'importance des stats dépendent du param value
    public void CreateSelf(float weaponValue)
    {
        // SetRandomSkin -> Resource.Load("WeaponVisual" + randomNumber.ToString())
        // SetRandomBaseDamage
        // SetRandomLevel
        // SetRandomRarity
        // SetRandomDamage
        // SetRandomForce
        // SetInfoPanelColor(rarity)
    }

    // Appelé par l'owner au premier loading
    public void SetWeapon(GameObject skin, AttackType attType, int level, Rarity rarity, float damage, Vector2 force) { 
       // SetSkin
       // SetAttType
       // SetLevel
       // SetRarity
       // SetDamage
       // SetForce
       // SetInfoPanelColor(rarity)
    }

}
