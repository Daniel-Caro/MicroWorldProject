using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneControlScript : MonoBehaviour
{
    public int griRows = 2;
    public int griCols = 4;
    public float offSetX = 4f;
    public float offSetY = 5f;
    [SerializeField] private cartaScript originalCard;
    [SerializeField] private Sprite[] images;
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPos = originalCard.transform.position;
        int[] numbers = {0,0,1,1,2,2,3,3};
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
                card.transform.position = new Vector3(posX, posY,startPos.z);
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
}
