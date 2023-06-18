using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSystem : MonoBehaviour
{
    [SerializeField] GameObject dagger;
	[SerializeField] GameObject daggerSeath;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawWeapon()
	{
		daggerSeath.SetActive(false);
		dagger.SetActive(true);
	}

	public void SheathWeapon()
	{
		daggerSeath.SetActive(true);
		dagger.SetActive(false);
	}
}
