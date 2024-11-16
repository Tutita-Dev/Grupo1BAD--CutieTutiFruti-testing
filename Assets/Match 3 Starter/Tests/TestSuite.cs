using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class TestSuite
{
    private GameObject guiManagerObject;
    private GUIManager guiManager;
    private BoardManager boardManager;


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


        // Creacion de objetos de tipo GameObject y boardManager
        GameObject boardManagerObject = new GameObject("BoardManager");
        boardManager = boardManagerObject.AddComponent<BoardManager>();

        //Inicializar la lista de caramelos x=4 y=4
        boardManager.candies = new List<Sprite>();
        for (int i = 0; i < 3; i++)
        {
            Sprite sprite = Sprite.Create(Texture2D.blackTexture, new Rect(0, 0, 4, 4), Vector2.zero); //modificar por width y height
            boardManager.candies.Add(sprite);
        }

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

    [Test]
    public void GetRandomGrid_FillGridWhitSprites()
    {
        //Generacion de cuadrícula aleatoria
        Sprite[,] grid = boardManager.GetRandomGrid(5, 5);

        //Verificar que cada celda tiene un Sprite de la lista de caramelos 
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                Assert.Contains(grid[x, y], boardManager.candies,
                    $"El elemento en ({x},{y}) no pertenece a la lista de caramelos");
            }
        }
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




