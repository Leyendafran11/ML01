using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalonController : MonoBehaviour
{

    public GameObject portero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

	private void OnCollisionEnter(Collision collision)
	{
        if (collision.gameObject.CompareTag("Porteria"))
        {
            portero.gameObject.GetComponent<PorteroCerebro>().Gol();
        }
		else if(collision.gameObject.CompareTag("Portero") || collision.gameObject.CompareTag("Pared"))
		{
			portero.gameObject.GetComponent<PorteroCerebro>().Parada();
		}
	}
}
