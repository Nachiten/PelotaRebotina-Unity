using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generarPelotas : MonoBehaviour
{
    GameObject pelotaBoost;

    // Start is called before the first frame update
    void Start()
    {

        var random = new Random();
        float numeroRandom = Random.Range(30f, 60f);

        generarLasPelotas();

        pelotaBoost = GameObject.Find("Pelota Boost");

        pelotaBoost.SetActive(false);

        InvokeRepeating("generarPelotaBoost", .01f, numeroRandom);
        InvokeRepeating("eliminarPelotaBoost", .01f, numeroRandom + 5f);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void generarLasPelotas() {

        for (int i = 0; i < 4; i++) { 

            // Crear el clon
            GameObject pelotaInstanciada = Instantiate(Resources.Load("Pelota", typeof(GameObject))) as GameObject;

            var random = new Random();
            float numeroRandomX = Random.Range(-2.5f, 4.5f);
            float numeroRandomY = Random.Range(-3.2f, 2f);

            pelotaInstanciada.transform.position = new Vector3(numeroRandomX, numeroRandomY, 0);

        }
    }

    void generarPelotaBoost() {
        pelotaBoost.SetActive(true);
    }

    void eliminarPelotaBoost() {
        pelotaBoost.SetActive(false);
    }
   
}
