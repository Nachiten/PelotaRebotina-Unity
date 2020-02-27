using System.Collections;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    static GameObject levelLoader;
    static Slider slider;
    static Text textoProgreso;
    static Text textoNivel;

    public bool DeleteKeys = false;
    static bool flag = true;

    /* -------------------------------------------------------------------------------- */

    void Start()
    {
        if (DeleteKeys) {
            borrarTodasLasKeys();
        }

        if (flag) { 
            // Aisgnar variables
            levelLoader = GameObject.Find("Panel Carga");
            textoProgreso = GameObject.Find("TextoProgreso").GetComponent<Text>();
            slider = GameObject.Find("Barra Carga").GetComponent<Slider>();

            textoNivel = GameObject.Find("Texto Cargando").GetComponent<Text>();

            flag = false;
        }


        // Ocultar pantalla de carga
        levelLoader.SetActive(false);
    }

    /* -------------------------------------------------------------------------------- */

    // Llamar a Corutina
    public void cargarNivel(int index)
    {
        StartCoroutine(cargarAsincronizadamente(index));
        textoNivel.text ="Cargando " + SceneManager.GetSceneByBuildIndex(index).name + " ...";

        /* Analytics
        if (index != 7) { 
            AnalyticsResult result =  AnalyticsEvent.Custom("Ingreso_" + SceneManager.GetSceneByBuildIndex(index).name);
            Debug.Log("Analytics Result: " + result + " | DATA: " + "Ingreso_" + SceneManager.GetSceneByBuildIndex(index).name);
        }*/
    }

    /* -------------------------------------------------------------------------------- */

    // Iniciar Corutina para cargar nivel en background
    IEnumerator cargarAsincronizadamente (int index)
    {
        // Iniciar carga de escena
        AsyncOperation operacion = SceneManager.LoadSceneAsync(index);

        // Mostrar pantalla de carga
        levelLoader.SetActive(true);

        Debug.Log("Cargando escena: " + index);

        // Mientras la operacion no este terminada
        while (!operacion.isDone)
        {
            // Generar valor entre 0 y 1
            float progress = Mathf.Clamp01(operacion.progress / .9f);
            // Modificar Slider
            slider.value = progress;
            // Modificar texto progreso
            textoProgreso.text = progress * 100f + "%";

            yield return null;
        }
    }

    /* -------------------------------------------------------------------------------- */

    public void salir() { Application.Quit(); }

    /* -------------------------------------------------------------------------------- */

    public void borrarTodasLasKeys() {
        Debug.LogError("BORRANDO TODAS LAS KEYS !!!!");
        PlayerPrefs.DeleteAll();
    }
}
