using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradordeZumbis : MonoBehaviour
{
    public GameObject Zumbi;
    private float contadorTempo = 0;
    public float TempoGerarZumbi = 1;
    public LayerMask LayerZumbi;
    private float distanciaDeGeracao = 3;
    private float DistanciaDoJogadorParaGeracao = 20;
    private GameObject Jogador;
    private int quantidadeMaximaDeZumbisVivos = 2;
    private int quantidadeDeZumbisVivos;
    private float tempoProximoAumentoDeDificuldade = 15;
    private float contadorDeAumentarDificuldade = 0;
    private void Start()
    {
        Jogador = GameObject.FindWithTag("jogador");
        contadorDeAumentarDificuldade = tempoProximoAumentoDeDificuldade;
        for(int i = 0; i < quantidadeMaximaDeZumbisVivos; i++)
        {
            StartCoroutine(GerarNovoZumbi());
        }
    }
    void Update()
    {
        bool possoGerarZumbisPelaDistancia = Vector3.Distance(transform.position, Jogador.transform.position) > DistanciaDoJogadorParaGeracao;
        if (possoGerarZumbisPelaDistancia == true &&
            quantidadeDeZumbisVivos < quantidadeMaximaDeZumbisVivos)
        {
            contadorTempo += Time.deltaTime;

            if (contadorTempo >= TempoGerarZumbi)
            {
                StartCoroutine(GerarNovoZumbi());
                contadorTempo = 0;
            }

            if (Time.timeSinceLevelLoad > contadorDeAumentarDificuldade)
            {
                contadorDeAumentarDificuldade = Time.timeSinceLevelLoad + tempoProximoAumentoDeDificuldade;
                quantidadeMaximaDeZumbisVivos++;
            }
        }
    }
        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, distanciaDeGeracao);
        }
        IEnumerator GerarNovoZumbi()
        {
            Vector3 posicaoDeCriacao = AleatorizarPosicao();
            Collider[] colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);

            while (colisores.Length > 0)
            {
                posicaoDeCriacao = AleatorizarPosicao();
                colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);
                yield return null;
            }

            ControlaInimigo zumbi = Instantiate(Zumbi, posicaoDeCriacao, transform.rotation).GetComponent<ControlaInimigo>();
            zumbi.meuGerador = this;
            quantidadeDeZumbisVivos++;
        }
        Vector3 AleatorizarPosicao()
        {
            Vector3 posicao = Random.insideUnitSphere * distanciaDeGeracao;
            posicao += transform.position;
            posicao.y = 0;

            return posicao;
        }
        public void DiminuirQuantidadeZumbisVivos()
        {
            quantidadeDeZumbisVivos--;
        }
    
}
