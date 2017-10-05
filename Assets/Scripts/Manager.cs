using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {
    //campos da UI (entradas)
    public InputField anguloField;
    public InputField distanciaField;
    public InputField prismaField;
    public Text saida;
    //variaveis logicas
    float angulo;
    float distancia;
    float tamanho;
    float prisma;
    Camera cam;
    Material mat;
    Texture tex;
    //referencias que vao receber as saídas: cabeça do teodolito vai receber a rotação no eixo X e o predio vai ser construído com o tamanho.
    public GameObject cabeca;
    public GameObject predio;
    Laser laser;
    GameObject teodolito;
    //metodo pra iniciar variaveis.
	void Start () {
        laser = cabeca.GetComponent<Laser>();
        teodolito = cabeca.transform.parent.root.gameObject;
        mat = predio.GetComponentInChildren<Renderer>().material;
        cam = Camera.main;
    }

    //pega as strings angulo e distancia e transforma em floats. 
    //Pega o angulo transforma em Radianos e acha a tangente Porque os metodos de funcoes trigonometricas da unity so trabalha com Radianos.
    //Depois multiplica pela distancia pra achar o tamanho do predio. 
    void AtualizaVariaveis(string angulo, string distancia,string prisma)
    {
        if (angulo != "")
        {            
            this.angulo = float.Parse(angulo);
            //a rotação do eixo x na unity é invertida, logo estou dando um tratamento ao angulo, se ele estiver no primeiro quadrante sera multiplicado por -1.
            if (this.angulo < 90 ) {
                this.angulo = -this.angulo;
            }
            
        }
        else {
            this.angulo = 0;
        }
        if (distancia != "")
        {
            this.distancia = float.Parse(distancia);
        }
        else
        {
            this.distancia = 0;
        }

        if (prisma != "")
        {
            this.prisma = float.Parse(prisma)-1   ;
        }
        else
        {
            this.prisma = -1;
        }

        //conversao de euler pra radianos.
        float teta = this.angulo * (Mathf.PI / 180);
        //pegando o valor absoluto da tangente de teta para trabalhar sempre no primeiro quadrante.
        tamanho = Mathf.Abs(Mathf.Tan(teta) * this.distancia)+(this.prisma+1);
        if (this.angulo == 90)
        {
            saida.text = "Predio infinito!";
        }
        else {
            saida.text = "Altura do predio = " + tamanho+ " m";
        }
        
    }

    void AtualizaCamera() {
        cam.orthographicSize = (distancia+tamanho+11)/2.4f;
        cam.transform.position = new Vector3(10.5f, tamanho/2, (-distancia)/2);
        
    }

    //repete comandos 1x/quadro
    void Update () {
        AtualizaVariaveis(anguloField.text,distanciaField.text,prismaField.text);
        cabeca.transform.rotation = Quaternion.Euler(angulo, 0 , 0);
        laser.hitPoint = new Vector3(0,tamanho, 0);
        predio.transform.localScale = new Vector3(10, tamanho, 10);
        teodolito.transform.position = new Vector3(0, prisma, -distancia);
        mat.SetTextureScale("_MainTex", new Vector2(10, tamanho ));
        AtualizaCamera();
    }
}
