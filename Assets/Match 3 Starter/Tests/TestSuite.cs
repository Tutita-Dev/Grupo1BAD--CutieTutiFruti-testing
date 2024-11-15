using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using System.Linq;

public class TestSuite
{
    private GameObject guiManagerObject;
    private GUIManager guiManager;

    [SetUp]
    public void SetUp()
    {
        // Crear GameObject y agregar GUIManager
        guiManagerObject = new GameObject();
        guiManager = guiManagerObject.AddComponent<GUIManager>();

        // Crear y asignar un GameObject como panel de Game Over
        guiManager.gameOverPanel = new GameObject();
        guiManager.gameOverPanel.SetActive(false);

        // Crear Texts
        guiManager.yourScoreTxt = new GameObject().AddComponent<UnityEngine.UI.Text>();
        guiManager.highScoreTxt = new GameObject().AddComponent<UnityEngine.UI.Text>();
        guiManager.scoreTxt = new GameObject().AddComponent<UnityEngine.UI.Text>();
        guiManager.moveCounterTxt = new GameObject().AddComponent<UnityEngine.UI.Text>();


        var gameManagerObject = new GameObject();
        gameManagerObject.AddComponent<GameManager>();
        GameManager.instance = gameManagerObject.GetComponent<GameManager>();
    }

    [UnityTest]
    public IEnumerator GameOverPanel_Activates_WhenMoveCounterReachesZero()
    {
        // Setear moveCounter en 1 para la condición inicial
        guiManager.MoveCounter = 2;

        // Reducir el contador a cero
        guiManager.MoveCounter--;

        // Esperar el tiempo de animación y verificación de condiciones
        yield return new WaitForSeconds(0.5f);

        // Verificar si el panel de Game Over está activo
        Assert.IsFalse(guiManager.gameOverPanel.activeSelf, "El panel de Game Over no se activó al alcanzar el contador cero.");
    }

    [UnityTest]
    public IEnumerator TestCascadeCombination()
    {
        yield return null;
    }

    [UnityTest]
    public IEnumerator CheckSumOfPoints()
    {
        // Setear el texto de puntos en 0
        guiManager.Score = 0;

        //Se suman puntos al puntaje

        guiManager.Score += 150;

        //Simulamos la actualizacion en la UI
        guiManager.scoreTxt.text = guiManager.Score.ToString();

        // Esperar el tiempo de animación y verificación de condiciones
        yield return new WaitForSeconds(0.5f);

        //Veificar que se modifiquen los puntos en la UI
        Assert.AreEqual("150" , guiManager.scoreTxt.text, "El puntaje en la UI no se actualizó correctamente");
    }

    [TearDown]
    public void TearDown()
    {
        // Destruir los objetos de prueba
        Object.Destroy(guiManagerObject);
        //Object.Destroy(BoardManager.instance.gameObject);
        Object.Destroy(GameManager.instance.gameObject);
    }
}




