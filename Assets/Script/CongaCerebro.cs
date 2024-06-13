using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class CongaCerebro : Agent
{
    public GameObject basura;
	public int velocidad;
	private Vector3 posInicial;

	private void Start()
	{
		posInicial = this.transform.position;
	}

	public override void OnEpisodeBegin()
	{
		this.transform.position = posInicial;

		float distanciaX = Random.Range(0.5f, 3.4f);
		float distanciaZ = Random.Range(0.5f, 3.4f);
		int sentidoX = Random.Range(0, 2);
		int sentidoZ = Random.Range(0, 2);

		Vector3 newPos;

		if (sentidoX == 0) //Positivo
		{
			newPos = posInicial + new Vector3(distanciaX, 0, 0);
		}
		else              //Negativo
		{
			newPos = posInicial + new Vector3(distanciaX*-1, 0, 0);
		}

		if (sentidoZ == 0) //Positivo
		{
			newPos = posInicial + new Vector3(0, 0, distanciaZ);
		}
		else              //Negativo
		{
			newPos = posInicial + new Vector3(0 , 0, distanciaZ*-1);
		}


		basura.transform.position = newPos;
	}

	/*public override void CollectObservations(VectorSensor sensor)
	{
		sensor.AddObservation(this.transform.position);
		sensor.AddObservation(basura.transform.position);
	}*/

	public override void OnActionReceived(ActionBuffers actions)
	{
		int movimiento = 0;
		int giro = 0;
		switch (actions.DiscreteActions[0])
		{
			case 0:
				movimiento = 1;
				break;
			case 1:
				movimiento = -1;
				break;
		}
		switch (actions.DiscreteActions[1])
		{
			case 0:
				giro = 1;
				break;
			case 1:
				giro = -1; break;
		}
		transform.position += transform.right * movimiento * velocidad * Time.fixedDeltaTime;
		transform.Rotate(Vector3.up, giro); AddReward(-0.00001f);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Objetivo"))
		{
			AddReward(1.0f);
			EndEpisode();
		}
		else if (collision.gameObject.CompareTag("Pared"))
		{
			AddReward(-1.0f);
			EndEpisode();
		}
	}
}
