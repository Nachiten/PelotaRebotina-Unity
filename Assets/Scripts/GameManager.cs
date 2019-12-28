using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour
{

    // Boton "Comenzar"
    GameObject boton;
    Text textoBoton;

    // Cantidad de bloques
    public int filas = 3;
    public int columnas = 3;

    // Numero de nivel
    Text textoNivel;

    // Numero de escena actual
    int index;

    /* -------------------------------------------------------------------------------- */

    void Start()
    {
        index = SceneManager.GetActiveScene().buildIndex;
        string name = SceneManager.GetActiveScene().name;

        GameObject.Find("Nivel").GetComponent<Text>().text = name;

        // Asignar variables
        boton = GameObject.Find("Boton");
        textoBoton = GameObject.Find("TextoBoton").GetComponent<Text>();

        // Modificar texto
        textoBoton.text = "Comenzar Nivel";
    }

    /* -------------------------------------------------------------------------------- */

    void FixedUpdate() {

        GameObject pelota = GameObject.Find("Pelota");

        Transform transformPelota = pelota.GetComponent<Transform>();

        Vector3 posicionPelota = transformPelota.position;

        Debug.Log(posicionPelota);

        posicionPelota = new Vector3(posicionPelota.x + 0.05f , posicionPelota.y + 0.05f , posicionPelota.z);

        transformPelota.position = posicionPelota;

        // Se modifica el vector posicion con la posicion correspondiente
        //Vector3 posicionVector = new Vector3(posicion.position.x + offsetX, posicion.position.y, posicion.position.z + offsetZ);

        // Se aplica la posicion
        //posicion.position = posicionVector;



    }

    /* -------------------------------------------------------------------------------- */



    /* -------------------------------------------------------------------------------- */


    /* -------------------------------------------------------------------------------- */



    /* -------------------------------------------------------------------------------- */

    Renderer modelo;
    Transform modeloTransform;

    // Ajustar posiciones y offsets de imagenes
    public void ajustarPosiciones()
    {
        // Textura de imagen modelo
        modelo = GameObject.Find("Bloque Modelo").GetComponent<Renderer>();

        // Transform de iamgen modelo
        modeloTransform = GameObject.Find("Bloque Modelo").GetComponent<Transform>();

        Debug.Log("Posicion Antes | " + modeloTransform.position.ToString());
        
        index = SceneManager.GetActiveScene().buildIndex;
        modeloTransform.position = new Vector3(modeloTransform.position.x - (index - 1) * 1.5111f, modeloTransform.position.y - (index - 1) * 2f, modeloTransform.position.z);

        Debug.Log("Posicion Despues | " + modeloTransform.position.ToString());
        Debug.Log("Index | " + index.ToString());

        // Ajustar tamaño de imagen modelo a nivel actual
        modeloTransform.localScale = new Vector3(1.5f * columnas, modeloTransform.localScale.y , 1.5f * filas );

        Renderer objeto;

        int contador;

        float scaleX = 1f / columnas;
        float scaleY = 1f / filas;

        float offsetX = 0f;
        float offsetY = scaleY * (filas * columnas - 1);
        contador = 1;

        for (int i = 0; i < filas; i++) {
            for (int j = 0; j < columnas; j++) {

                if (contador < filas * columnas || SceneManager.GetActiveScene().buildIndex > 12)
                {
                    // Asignar renderer
                    objeto = GameObject.Find(contador.ToString()).GetComponent<Renderer>();
                    // Cambiar "Tiling" de textura
                    objeto.material.mainTextureScale = new Vector2(scaleX, scaleY);
                    // Ajustar "Offeset" de textura
                    objeto.material.mainTextureOffset = new Vector2(offsetX, offsetY);
                    // Cambiar la textura al modelo
                    objeto.material.mainTexture = modelo.material.mainTexture;

                    contador++;
                }
                offsetX += scaleX;
            }
            offsetX = 0;
            offsetY -= scaleY;
        }
    }

    Transform referenciaAjuste;
    Transform plataforma;

    // Determinar posicion correcta de juego
    public void ajustarUbicacion()
    {
        if (index < 12)
        { 
            plataforma = GameObject.Find("Piso Mapa").GetComponent<Transform>();
            plataforma.localScale = new Vector3((5 * columnas) + 2, plataforma.localScale.y, (5 * filas) + 2);
        }

        int contador = 1;
        Transform objeto;

        float offsetX = 5;
        float offsetZ = 0;

        float posX = 0;
        float posZ = 0;

        for (int i = 0; i < filas; i++)
        {
            for (int j = 0; j < columnas; j++)
            {
                if (i == 0 && j == 0)
                {
                    // Primer bloque es la referencia
                    referenciaAjuste = GameObject.Find(contador.ToString()).GetComponent<Transform>();

                    posX = referenciaAjuste.position.x;
                    posZ = referenciaAjuste.position.z;

                    determinarPos(ref posX, ref posZ);
                    referenciaAjuste.position = new Vector3(posX, referenciaAjuste.position.y, posZ);
                }
                else if (!(i == filas - 1 && j == columnas - 1))
                {
                    objeto = GameObject.Find(contador.ToString()).GetComponent<Transform>();

                    objeto.position = new Vector3(referenciaAjuste.position.x + offsetX, objeto.position.y, referenciaAjuste.position.z + offsetZ);

                    offsetX += 5;
                }
                contador++;
            }
            offsetX = 0;
            offsetZ -= 5;
        }
    }

    int mayor;

    // Hijo de ajustarUbicacion
    void determinarPos(ref float posicionX, ref float posicionZ)
    {
        if (filas > columnas) mayor = filas;
        else mayor = columnas;

        float valorX = (columnas - 3) * 2.5f;
        float valorZ = (filas - 3) * 2.5f;

        posicionX -= valorX;
        posicionZ += valorZ;

        float offset = (mayor - 3) * 2.5f;

        Transform modelo = GameObject.Find("Modelo").GetComponent<Transform>();
        modelo.position = new Vector3(modelo.position.x, modelo.position.y + offset, modelo.position.z);

        Transform camara = GameObject.Find("Main Camera").GetComponent<Transform>();
        camara.position = new Vector3(camara.position.x, camara.position.y + offset, camara.position.z);

        //Debug.Log("VALORX: " + valorX + " | VALOR Z:" + valorZ);
    }
}
