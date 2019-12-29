using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movimientoPelota : MonoBehaviour
{
    float velocidadX;
    float velocidadY;

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

    int strikes = 0;
    int correctas = 0;

    bool perdio = false;

    // Update is called once per frame
    void Update()
    {

        if (perdio) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider == null) {
                sumarUnStrike();
                return;
            }

            if (hit.collider.gameObject.tag == "PelotaCorrecta")
            {
                if (velocidadX > 0) velocidadX += 1f; else velocidadX -= 1f;

                if (velocidadY > 0) velocidadY += 1f; else velocidadY -= 1f;

                sumarUnaCorrecta();
            }
            else {
                sumarUnStrike();
            }
        }


    }

    void sumarUnaCorrecta() {
        correctas++;

        GameObject.Find("Cant Correctas").GetComponent<Text>().text = correctas.ToString();
        //Debug.Log("Cantidad de Correctas: " + strikes);
    }

    void sumarUnStrike() {

        strikes++;

        if (strikes == 3)
        {
            perdio = true;
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        GameObject.Find("Cant Strikes").GetComponent<Text>().text = strikes.ToString();
        //Debug.Log("Cantidad de STIKES: " + strikes);
    }

    void FixedUpdate()
    {
        if (perdio) return;

        this.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadX, velocidadY);

        Debug.Log("Velocidad X: " + velocidadX + " | Velocidad Y: " + velocidadY);
    }

    private void OnCollisionEnter2D(Collision2D elementoGolpeado)
    {

        string nombreGolpe = elementoGolpeado.gameObject.name;

        if (nombreGolpe == "Pared Arriba" || nombreGolpe == "Pared Abajo")
        velocidadY *= -1;

        else
        velocidadX *= -1;
        
        //Debug.Log(elementoGolpeado.gameObject.name);
    }
}
