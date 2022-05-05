using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialScript : MonoBehaviour
{
    public Sprite[] pirateSprites;
    public Sprite[] princessSprites;
    public Sprite[] futureSprites;
    private Sprite[] usedSprites;
    private string[] pirateDialog = {
        "…",
        "Arrrgggh grumete! ¡Por todos los mares! ¡Por fin respiras!",
        "Te he guiado moviendo corrientes por popa y proa hasta llegar a Isla del Mono… Pero pensaba que no tendrías vida.",
        "Soy el gran Capitán Than. Y te preguntarás por qué tengo la cara tan pálida… Te lo voy a contar, me has caido bien, pequeña sardina.",
        "Hace dos días, mi tripulación y yo estábamos navegando en busca de una pieza, cuando de repente, una tormenta empezó a azotar incesablemente.",
        "Nuestro barco no pudo soportar el ímpetu de las olas y empezó a tener daños en el casco. El viento partió la vela mayor y acabamos naufragando.",
        "Cuando me quise dar cuenta, estábamos en la maldita Isla del Mono.",
        "Isla del Mono está maldita. Cada vez que alguien muere en sus orillas, se convierte en un alma errante, condenado por el resto de sus días a permanecer aquí, sin poder interactuar con nadie.",
        "Es curioso que puedas verme… Pero no hay tiempo para más misterios.",
        "Necesito que dirijas a mi tripulación para que puedan seguir viviendo y proliferando en esta isla.",
        "No sabes lo difícil que resulta encarrillar a una tripulación tan sumamente conflictiva.",
        "Te ayudaré desde las sombras… e intentaré invocar a Juan Gorrión, el mítico pirata. Él ya ayudó hace tiempo a un joven como tú que parecía de otro lugar…",
        "¿Qué me dices? ¿Aceptas?", //Panel
        "¡Arrrrrgh marinero! ¡Icen las velas!"
    };

    private string[] futureDialog = {
        "*PEE* *PO* *PEE* *PO* *PEEPO*",
        "Diagnóstico correcto! El sujeto humano ha despertado.",
        "Mi nombre es PEETO. Programa Especializado en Enseñanza de Tareas Organizativas. Estoy aquí para guiarte en tu tarea. ¿No sabes cual es?",
        "Te encuentras en el año 3487, unos años después de la Gran Guerra humano-robot. Tras la guerra, los robots pensamos que sería sencillo construir una sociedad sin humanos, pero lo cierto es que todo apuntó al caos, nada fue como esperábamos.",
        "Nuestros gobernantes TRIFAS y KO, llegaron a la conclusión de que necesitábamos a los humanos, la raza creadora para poder entender como el corazón humano podía construir civilizaciones sin llegar a la extinción. Es por esto que ansiabamos tu llegada.",
        "Como humano solo tendrás que tomar decisiones y dirigir nuestra villa. Nosotros analizaremos los datos de tu actividad y comenzaremos a crear modelos más refinados de metahumanos, para finalmente, crear una sociedad de metahumanos en simbiosis con los robots.",
        "A cambio de tu ayuda, te devolveremos a casa. Podrás salir de tu teléfono móvil y acabar esta historia. ¿Aceptas?", //Panel
        "Efectivamente, lo esperábamos. Un programador de este videojuego solo ha permitido que se pueda pulsar el botón Sí como decisión. Recuerda, lo sabemos todo de ti. Disfruta de la aventura."
    };

    private string[] princessDialog = {
        "...",
        "Hola, ¿se encuentra bien? ¡Acaba de caer del cielo!",
        "No sé qué clase de hechicero es, pero no importa, necesito su ayuda urgentemente.",
        "Mi nombre es Heleen Leclerc, la princesa de Bracktenbury. Desde que han fallecido mis padres , que en paz descansen, he tenido muchos problemas con el trono.",
        "No he parado de recibir pretendientes que quieren mi mano para acceder al poder. Incluso mi prometido aprovechó el momento de duelo para intentar acelerar el proceso matrimonial… Estoy harta.",
        "Es por esto que necesito su ayuda para poder desconectar del reino sin tener que sacrificar el legado de mi familia. Tu misión es sencilla, solo tendrías que hacer las funciones de monarca desde las sombras hasta mi vuelta del retiro espiritual de Bigbury.",
        "Si lo consigue, hablaré con el gran mago Bertín y le llevaremos de vuelta a casa.",
        "¿Acepta el trato?", //Panel
        "Como esperaba. No olvide que soy la nueva Reina y, en dos segundos, puede tener su cabeza rodando por el suelo jeje. Qué amable es."
    };

    private string[] usedDialog;
    private int dialogIndex = 0;
    private TextMeshProUGUI dialogText;
    private GameObject character;
    public GameObject panelDecisionSioSi;

    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("Character");
        dialogText = GameObject.Find("TutorialText").gameObject.GetComponent<TextMeshProUGUI>();
        switch (Globals.style)
        {
            case Style.Future:
                usedSprites = futureSprites;
                usedDialog = futureDialog;
                break;
            case Style.Princess:
                usedSprites = princessSprites;
                usedDialog = princessDialog;
                break;
            case Style.Pirate:
                usedSprites = pirateSprites;
                usedDialog = pirateDialog;
                character.GetComponent<Image>().sprite = usedSprites[1];
                break;
        }
        dialogText.text = usedDialog[dialogIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Globals.tutorialStep <= 10)
            {
                switch (Globals.style)
                {
                    case Style.Future:
                        break;
                    case Style.Princess:
                        break;
                    case Style.Pirate:
                        if (dialogIndex == 12){
                            panelDecisionSioSi.SetActive(true);
                            character.GetComponent<Image>().sprite = usedSprites[0];
                            GameObject botonSi = panelDecisionSioSi.transform.Find("BotonSi").gameObject;
                            Button compoBotonSi = botonSi.GetComponent<Button>();
                            GameObject botonNo = panelDecisionSioSi.transform.Find("BotonNo").gameObject;
                            Button compoBotonNo = botonNo.GetComponent<Button>();
                            compoBotonSi.onClick.AddListener(() => {
                                    dialogIndex++;
                                    dialogText.text = usedDialog[dialogIndex];
                                    panelDecisionSioSi.SetActive(false);
                            }); 
                        }
                        else
                        {
                            if (dialogIndex == 1 || dialogIndex == 9 || dialogIndex == 11 || dialogIndex == 13) character.GetComponent<Image>().sprite = usedSprites[2];
                            if (dialogIndex == 4 || dialogIndex == 5 || dialogIndex == 6 || dialogIndex == 7 || dialogIndex == 10) character.GetComponent<Image>().sprite = usedSprites[1];
                            if (dialogIndex == 2 || dialogIndex == 3 || dialogIndex == 8) character.GetComponent<Image>().sprite = usedSprites[0];
                            dialogIndex++;
                            dialogText.text = usedDialog[dialogIndex];
                        }
                        break;
                }
            }
        }
    }
}
