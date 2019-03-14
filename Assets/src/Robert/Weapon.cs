using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class Weapon : MonoBehaviour {

    //bones used for right and left gun bones
    private Transform rightGunBone;
    private Transform leftGunBone;
    private Animator animator;
    private GameObject gun;

    public string weaponType = "Rifle";


    void Awake()
    {
        //get our animator
        animator = GetComponent<Animator>();
        rightGunBone = gameObject.transform.Find("RigPistolRight");
        leftGunBone = gameObject.transform.Find("RigPistolLeft");
        SetWeapon(weaponType);
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetWeapon(string type)
    {
        //determine type of weapon
        switch(type)
        {
            case "Rifle":
                AttachWeapon("SciFiRifle");
                break;
            default:
                Debug.LogError("In Weapon.cs no weapon has been specfied when calling SetWeapon");
                break;

        }

    }

    //actually attaches the weapon to the charector rig
    private void AttachWeapon(string name)
    {
        GameObject newRightGun = (GameObject)Instantiate(Resources.Load<GameObject>("Robert/Weapons/" + name));
        Debug.Log("attached new weapon");
        Debug.Log(newRightGun);
        newRightGun.transform.parent = rightGunBone;
        newRightGun.transform.localPosition = Vector3.zero;
        newRightGun.transform.localRotation = Quaternion.Euler(90, 0, 0);
        gun = newRightGun;
        RuntimeAnimatorController controller = Resources.Load<RuntimeAnimatorController>("Robert/Controllers/rifle");
        animator.runtimeAnimatorController = controller;
    }
}


/*
using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animator))]
public class PlayerController : MonoBehaviour {

	public Transform rightGunBone;
	public Transform leftGunBone;
	public Arsenal[] arsenal;

	private Animator animator;

	void Awake() {
		animator = GetComponent<Animator> ();
		if (arsenal.Length > 0)
			SetArsenal (arsenal[0].name);
		}

	public void SetArsenal(string name) {
		foreach (Arsenal hand in arsenal) {
			if (hand.name == name) {
				if (rightGunBone.childCount > 0)
					Destroy(rightGunBone.GetChild(0).gameObject);
				if (leftGunBone.childCount > 0)
					Destroy(leftGunBone.GetChild(0).gameObject);
				if (hand.rightGun != null) {
					GameObject newRightGun = (GameObject) Instantiate(hand.rightGun);
					newRightGun.transform.parent = rightGunBone;
					newRightGun.transform.localPosition = Vector3.zero;
					newRightGun.transform.localRotation = Quaternion.Euler(90, 0, 0);
					}
				if (hand.leftGun != null) {
					GameObject newLeftGun = (GameObject) Instantiate(hand.leftGun);
					newLeftGun.transform.parent = leftGunBone;
					newLeftGun.transform.localPosition = Vector3.zero;
					newLeftGun.transform.localRotation = Quaternion.Euler(90, 0, 0);
				}
				animator.runtimeAnimatorController = hand.controller;
				return;
				}
		}
	}

	[System.Serializable]
	public struct Arsenal {
		public string name;
		public GameObject rightGun;
		public GameObject leftGun;
		public RuntimeAnimatorController controller;
	}
}
 */
