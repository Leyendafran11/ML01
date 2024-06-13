using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.UIElements;

public class PorteroCerebro: Agent
{
    public GameObject pelota;
	private Vector3 puntoPenalti;
	public float velocidad;
	private Vector3 posPortero;

	private void Start()
	{
		posPortero = this.transform.position;
		puntoPenalti = pelota.transform.position;
	}

	public override void OnEpisodeBegin()
	{
		this.transform.position = posPortero;

		pelota.GetComponent<Rigidbody>().velocity = Vector3.zero;
		pelota.GetComponent <Rigidbody>().angularVelocity = Vector3.zero;
		pelota.transform.position = puntoPenalti;


		Vector3 patada = new Vector3(7.185f, Random.Range(0.0f, 2.0f), Random.Range(-1.5f, 1.5f));

		pelota.GetComponent<Rigidbody>().AddForce(patada * 25);

	}

	public override void CollectObservations(VectorSensor sensor)
	{
		sensor.AddObservation(this.transform.position);
	}

	public override void OnActionReceived(ActionBuffers actions)
	{
		float movimiento = actions.ContinuousActions[0];
		
		this.transform.position = this.transform.position + this.transform.right * movimiento * velocidad * Time.deltaTime;

		AddReward(-0.0001f);

		if (Vector3.Distance(this.transform.position, posPortero) > 1.8f)
		{
			AddReward(-0.5f);
		}
	}

	public void Gol()
	{
		AddReward(-1.0f);
		EndEpisode();
	}

	public void Parada()
	{
		AddReward(1.0f);
		EndEpisode();
	}

	
}
