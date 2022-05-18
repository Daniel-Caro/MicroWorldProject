using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;
public class Frogger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite deadSpritePirate;
    public Sprite idleSpritePirate;
    public Sprite deadSpritePrincess;
    public Sprite idleSpritePrincess;
    public Sprite deadSpriteFuture;
    public Sprite idleSpriteFuture;
    public string type;
    public static bool haMuerto = false;
    public GameObject gameOverText;
    public GameObject secondChanceText;
    private bool coroutineCalled = false;
    private Button botonArriba;
    private Button botonIzquierda;
    private Button botonDerecha;
    private Button botonAbajo;
    private bool haMovido = false;
    // Start is called before the first frame update
    private void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }
    void Start()
    {
        botonArriba = GameObject.Find("/Canvas/Botones/Arriba").GetComponent<Button>();
        botonIzquierda = GameObject.Find("/Canvas/Botones/Izquierda").GetComponent<Button>();
        botonDerecha = GameObject.Find("/Canvas/Botones/Derecha").GetComponent<Button>();
        botonAbajo = GameObject.Find("/Canvas/Botones/Abajo").GetComponent<Button>();
        
        if(Globals.style == Style.Princess){
            spriteRenderer.sprite = idleSpritePrincess;
        }
        else if(Globals.style == Style.Pirate){
            spriteRenderer.sprite = idleSpritePirate;
        }
        else if(Globals.style == Style.Future){
            spriteRenderer.sprite = idleSpriteFuture;
        }

            botonArriba.onClick.AddListener(() => {
            Vector3 direction = new Vector3(0,0.19f,0);
            transform.rotation = Quaternion.Euler(0f,0f,0f);

            Move(direction);
             });
            botonAbajo.onClick.AddListener(()=> {
                Vector3 direction = new Vector3(0,-0.19f,0);
                transform.rotation = Quaternion.Euler(0f,0f,180f);

                Move(direction);
            });
            botonIzquierda.onClick.AddListener(()=> {
                Vector3 direction = new Vector3(-0.19f,0,0);
                transform.rotation = Quaternion.Euler(0f,0f,90f);

                Move(direction);
            });
            botonDerecha.onClick.AddListener(()=> {
                Vector3 direction = new Vector3(0.19f,0,0);
                transform.rotation = Quaternion.Euler(0f,0f,-90f);
                Move(direction);
            });
        
    
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
            Vector3 direction = new Vector3(0,0.19f,0);
            transform.rotation = Quaternion.Euler(0f,0f,0f);
            Move(direction);
        }
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) ){
            Vector3 direction = new Vector3(0,-0.19f,0);
            transform.rotation = Quaternion.Euler(0f,0f,180f);
            Move(direction);
        }
        else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) ){
            Vector3 direction = new Vector3(-0.19f,0,0);
            transform.rotation = Quaternion.Euler(0f,0f,90f);
            Move(direction);
        }
        else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) ){
            Vector3 direction = new Vector3(0.19f,0,0);
            transform.rotation = Quaternion.Euler(0f,0f,-90f);
            Move(direction);
        }
        


    }
    private void Move(Vector3 direction){
        Vector3 destination = transform.position + direction;
        Collider2D barrier = Physics2D.OverlapBox(destination, Vector2.zero, 0f,LayerMask.GetMask("Barrier"));
        Collider2D obstacle = Physics2D.OverlapBox(destination, Vector2.zero, 0f,LayerMask.GetMask("Obstacle"));
        if(barrier != null){
            return;
        }else{
            transform.position = destination;
        }
        if(obstacle != null){
            transform.position = destination;
            Death();
        }else{
            transform.position = destination;
        }

    }
    private void Death(){
        if(Globals.style == Style.Princess){
            spriteRenderer.sprite = deadSpritePrincess;
            GameObject image = GameObject.Find("/Canvas/Image");
            image.SetActive(false);
        }
        else if(Globals.style == Style.Pirate){
            spriteRenderer.sprite = deadSpritePirate;
            GameObject image = GameObject.Find("/Canvas/Image");
            image.SetActive(false);
        }
        else if(Globals.style == Style.Future){
            spriteRenderer.sprite = deadSpriteFuture;
            GameObject image = GameObject.Find("/Canvas/Image");
            image.SetActive(false);
        }
        haMuerto = true;
         if (Globals.restartGameBoost)
            {
                Globals.restartGameBoost = false;
                haMuerto = false;
                StartCoroutine(secondChance());
            }
            else
            {
                GameObject.Find("/Canvas/Botones/Abajo").SetActive(false);
                GameObject.Find("/Canvas/Botones/Arriba").SetActive(false);
                GameObject.Find("/Canvas/Botones/Izquierda").SetActive(false);
                GameObject.Find("/Canvas/Botones/Derecha").SetActive(false);
                Debug.Log("Monedas obtenidas: " + Globals.obtainedCoins);
                if (Globals.doubleCoinsBoost)
                {
                    Globals.gameResources["Coins"].currentR += Globals.obtainedCoins * 2;
                    Globals.doubleCoinsBoost = false;
                }
                else Globals.gameResources["Coins"].currentR += Globals.obtainedCoins;
                Globals.obtainedCoins = 0;
                gameOverText.SetActive(true);
                if (!coroutineCalled)
                {
                    StartCoroutine(changeScene());
                    Debug.Log("after calling corot");
                    coroutineCalled = true;
                    haMuerto = false;
                }
            }
        transform.rotation = Quaternion.identity;
        enabled = false;
    }
    private void Respawn(){

        enabled = true;

    }
    private void OnTriggerEnter2D(Collider2D other){
        if(enabled && other.gameObject.layer == LayerMask.NameToLayer("Obstacle")){
            Death();
            TimerFrogger.timeLeft = 0f;
        }
    }
    IEnumerator changeScene() {
        Debug.Log("Empieza la corrutina");
        yield return new WaitForSeconds(3f);
        Debug.Log("Termina el tiempo");
        Scene mainScene = SceneManager.GetSceneByName("SampleScene");
        SceneManager.SetActiveScene(mainScene);
        mainScene.GetRootGameObjects().First().gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync("froggerScene");
    }

    IEnumerator secondChance() {
        Debug.Log("Empieza la corrutina");
        secondChanceText.SetActive(true);
        yield return new WaitForSeconds(2f);
        secondChanceText.SetActive(false);
        SceneManager.UnloadSceneAsync("froggerScene");
        SceneManager.LoadScene("froggerScene", LoadSceneMode.Additive);
        Debug.Log("Termina el tiempo");
    }
    
}
