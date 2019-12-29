using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoPelota : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float velocidadX = 3;
    float velocidadY = 3;

    void FixedUpdate()
    {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadX, velocidadY);
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
