using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class detectarClicks : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    int strikes = 0;
    int correctas = 0;

    float velocidadX;
    float velocidadY;

    public float incrementoVelocidad = 0.3f;
    public float disminucionVelocidad = -0.5f;

    public static bool perdio = false;

    // Update is called once per frame
    void Update()
    {

        if (perdio) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            // No toca ningun objeto (afuera del mapa)
            if (hit.collider == null)
            {
                Debug.Log("Pegaste AFUERA DEL MAPA");
                return;
            }

            bool golpeCorrecto = checkearGolpeCorrectoDe(hit.collider.gameObject.tag);

            if (golpeCorrecto) sumarUnaCorrecta();

            else sumarUnStrike();

        }
    }

    bool checkearGolpeCorrectoDe(string unTag) {
        bool golpeCorrecto = false;

        // Tag del objeto
        switch (unTag)
        {
            case "PelotaCorrecta":

                // Aumentar velocidad de las pelotas CORRECTAS
                foreach (GameObject unObjecto in GameObject.FindGameObjectsWithTag("PelotaCorrecta"))
                {
                    unObjecto.GetComponent<movimientoPelota>().modificarVelocidadEn(incrementoVelocidad);
                }

                golpeCorrecto = true;

                Debug.Log("Le pegaste a una PELOTA CORRECTA");
                break;

            case "PelotaIncorrecta":
                Debug.Log("Le pegaste a una PELOTA INCORRECTA");
                break;

            case "ParedMapa":
                Debug.Log("Le pegaste a una PARED DEL MAPA");
                break;

            case "FondoMapa":
                Debug.Log("Le pegaste al FONDO DEL MAPA");
                break;

            case "PelotaBoost":

                // Aumentar velocidad de las pelotas CORRECTAS
                foreach (GameObject unObjecto in GameObject.FindGameObjectsWithTag("PelotaCorrecta"))
                {
                    unObjecto.GetComponent<movimientoPelota>().modificarVelocidadEn(disminucionVelocidad);
                }

                golpeCorrecto = true;

                break;
            
        }

        return golpeCorrecto;
    }



    void sumarUnaCorrecta()
    {
        correctas++;

        GameObject.Find("Cant Correctas").GetComponent<Text>().text = correctas.ToString();
        //Debug.Log("Cantidad de Correctas: " + strikes);
    }

    void sumarUnStrike()
    {

        strikes++;

        if (strikes == 3)
        {
            perdio = true;
        }
        GameObject.Find("Cant Strikes").GetComponent<Text>().text = strikes.ToString();
        //Debug.Log("Cantidad de STIKES: " + strikes);
    }
}
