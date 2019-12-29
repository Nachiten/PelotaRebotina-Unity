using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movimientoPelota : MonoBehaviour
{
    public float velocidadX;
    public float velocidadY;

    // Start is called before the first frame update
    void Start()
    {
        velocidadX = generarNumeroRandom();
        velocidadY = 3 - generarNumeroRandom();
    }

    float generarNumeroRandom()
    {
        var random = new Random();
        float numeroRandom = Random.Range(1f, 3f);

        return numeroRandom;
    }


    // Update is called once per frame
    void Update()
    {
    }

    public void aumentarVelocidad() {

        float aumentoVelocidad = 0.3f;

        if (velocidadX > 0) velocidadX += aumentoVelocidad; else velocidadX -= aumentoVelocidad;

        if (velocidadY > 0) velocidadY += aumentoVelocidad; else velocidadY -= aumentoVelocidad;
    }


    void FixedUpdate()
    {
        if (detectarClicks.perdio) {

            cambiarVelocidadA(0, 0);
            return;

        }

        cambiarVelocidadA(velocidadX, velocidadY);

        //Debug.Log("Velocidad X: " + velocidadX + " | Velocidad Y: " + velocidadY);
    }

    void cambiarVelocidadA(float valorX, float valorY) {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(valorX, valorY);
    }

    // Cuando se golpea 
    private void OnTriggerEnter2D(Collider2D elementoGolpeado)
    {

        string nombreGolpe = elementoGolpeado.gameObject.name;

        switch (nombreGolpe) 
        {
            case "Pared Arriba":
            case "Pared Abajo":
                velocidadY *= -1;
                break;

            case "Pared Izquierda":
            case "Pared Derecha":
                velocidadX *= -1;
                break;
        }

        //Debug.Log("Entro en OnTriggerEnter2D");
    }

    public void perder() {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }
}
