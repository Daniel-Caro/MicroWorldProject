using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading.Tasks;
public class SceneControlScript : MonoBehaviour
{
    public int griRows = 2;
    public int griCols = 4;
    public float offSetX = 1f;
    public float offSetY = 2f;
    [SerializeField] private cartaScript originalCardPirate;
    [SerializeField] private cartaScript originalCardPrincess;
    [SerializeField] private cartaScript originalCardFuture;
    [SerializeField] private cartaScript originalCard;
    [SerializeField] private Sprite[] images;
    public int score = 0;
    private GameObject audioMoneda;
    // Start is called before the first frame update
    void Start()
    {   
        audioMoneda = GameObject.Find("/SceneControl/audiosFondo/audioMoneda");
        if(Globals.style == Style.Princess){
            originalCard = originalCardPrincess;
            GameObject cartaPrincesa = GameObject.Find("/SceneControl/cartas/princess");
            cartaPrincesa.SetActive(true);
            GameObject fondoPrincesa = GameObject.Find("/SceneControl/fondos/princess");
            fondoPrincesa.SetActive(true);
            GameObject monedaPrincesa = GameObject.Find("/SceneControl/Canvas/DIAMANTE");
            monedaPrincesa.SetActive(true);
            GameObject audioFondoPrincesa = GameObject.Find("/SceneControl/audiosFondo/audioFondoPrincesa");
            audioFondoPrincesa.SetActive(true);
        } else if(Globals.style == Style.Pirate){
            originalCard =originalCardPirate;
            GameObject cartaPirata = GameObject.Find("/SceneControl/cartas/pirate");
            cartaPirata.SetActive(true);
            GameObject fondoPirata = GameObject.Find("/SceneControl/fondos/pirata");
            fondoPirata.SetActive(true);
            GameObject monedaPirate = GameObject.Find("/SceneControl/Canvas/PIRATACOIN");
            monedaPirate.SetActive(true);
            GameObject audioFondoPirata = GameObject.Find("/SceneControl/audiosFondo/audioFondoPrincesa");
            audioFondoPirata.SetActive(true);
        }
        else if(Globals.style == Style.Future){
            originalCard = originalCardFuture;
            GameObject cartaFuturo = GameObject.Find("/SceneControl/cartas/future");
            cartaFuturo.SetActive(true);
            GameObject fondoFuturo = GameObject.Find("/SceneControl/fondos/future");
            fondoFuturo.SetActive(true);
            GameObject monedaFuturo = GameObject.Find("/SceneControl/Canvas/CHIP");
            monedaFuturo.SetActive(true);
            GameObject audioFondoFuturo = GameObject.Find("/SceneControl/audiosFondo/audioFondoFuturo");
            audioFondoFuturo.SetActive(true);
        }
        Vector3 startPos = originalCard.transform.position;
        int[] numbers = new int[16];
        if(Globals.style == Style.Princess){
            numbers = new int[] {4,4,5,5,6,6,7,7,4,4,5,5,6,6,7,7};
        } else if(Globals.style == Style.Pirate){
            numbers = new int[]{0,0,1,1,2,2,3,3,0,0,1,1,2,2,3,3};
        }
        else if(Globals.style == Style.Future){
            numbers = new int[]{8,8,9,9,10,10,11,11,8,8,9,9,10,10,11,11};
        }
        
        numbers = ShuffeArray(numbers);
        for(int i=0; i < griCols; i++){
            for(int j=0; j< griRows; j++){
                cartaScript card;
                if(i==0 && j==0){
                    card = originalCard;
                }else{
                    card = Instantiate(originalCard) as cartaScript;
                }
                int index = j*griCols+i;
                int id = numbers[index];
                card.ChangeSprite(id, images[id]);
                float posX = (offSetX*i) + startPos.x;
                float posY = (offSetY*j) + startPos.y;
                card.transform.position = new Vector3(posX*0.5f, posY*0.5f,startPos.z);
            }
        }
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
    private int[] ShuffeArray(int[] numbers){
        int[] newArray = numbers.Clone() as int[];
        for(int i = 0; i < newArray.Length; i++){
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }
    private cartaScript _firstRevealed;
    private cartaScript _secondRevealed;
    private int _score = 0;
    [SerializeField] private TextMesh scoreLabel;
    public bool canReveal{
        get{
            return _firstRevealed == null || _secondRevealed == null;
        }
    }
    public async Task CardRevealed(cartaScript card){
        if(_firstRevealed == null){
            _firstRevealed = card;
        }else {
            _secondRevealed = card;
            StartCoroutine(CheckedMatch());
            await Task.Delay(1000);
            audioMoneda.SetActive(false);
        }
        IEnumerator CheckedMatch(){
            if (_firstRevealed.id == _secondRevealed.id){
                audioMoneda.SetActive(true);
                Globals.obtainedCoins++;
                scoreLabel.text = Globals.obtainedCoins.ToString();
                
                
            }
            else{
                yield return new WaitForSeconds(0.5f);
                _firstRevealed.Unreveal();
                _secondRevealed.Unreveal();
            }
            _firstRevealed = null;
            _secondRevealed = null;
        }
    }
}
