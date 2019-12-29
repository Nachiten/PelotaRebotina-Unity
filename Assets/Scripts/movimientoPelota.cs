using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movimientoPelota : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    int strikes = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider == null) {
                sumarUnStrike();
                return;
            }

            if (hit.collider.gameObject.name == "Pelota")
            {
                if (velocidadX > 0) velocidadX += 1f; else velocidadX -= 1f;

                if (velocidadY > 0) velocidadY += 1f; else velocidadY -= 1f;
            }
            else {
                sumarUnStrike();
            }
        }

        
    }

    void sumarUnStrike() {

        if (strikes == 4) return;

        strikes++;
        GameObject.Find("Cant Strikes").GetComponent<Text>().text = strikes.ToString();
        Debug.Log("Cantidad de STIKES: " + strikes);
    }

    float velocidadX = 3;
    float velocidadY = 3;

    void FixedUpdate()
    {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadX, velocidadY);

        //Debug.Log("Velocidad X: " + velocidadX + " | Velocidad Y: " + velocidadY);
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
