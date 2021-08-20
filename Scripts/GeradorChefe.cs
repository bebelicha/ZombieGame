using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefe : MonoBehaviour
{
    private float tempoParaProximaGeracao = 0;
    public float tempoEntreGeracoes = 60;
    public GameObject ChefePrefab;
    private ControlaInterface scriptControlaInterface;
    public Transform[] PosicoesPossiveisDeGeracao;
    private Transform jogador;
    private void Start()
    {
        tempoParaProximaGeracao = tempoEntreGeracoes;
        scriptControlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
        jogador = GameObject.FindWithTag("jogador").transform;
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad > tempoParaProximaGeracao)
        {
            Vector3 posicaoDeCriacao = CalcularPosicaoPossivelMaisDistanteDoJogador();
            Instantiate(ChefePrefab, posicaoDeCriacao, Quaternion.identity);
            scriptControlaInterface.AparecerTextoDoChefeCriado();
            tempoParaProximaGeracao = Time.timeSinceLevelLoad + tempoEntreGeracoes;
        }
    }
    Vector3 CalcularPosicaoPossivelMaisDistanteDoJogador()
    {
        Vector3 posicaoDeMaiorDistancia = Vector3.zero;
        float maiorDistancia = 0;
        foreach (Transform posicao in PosicoesPossiveisDeGeracao)
        {
            float distanciaPosicaoJogador = Vector3.Distance(posicao.position, jogador.position);
            if (distanciaPosicaoJogador > maiorDistancia)
            {
                maiorDistancia = distanciaPosicaoJogador;
                posicaoDeMaiorDistancia = posicao.position;
            }
        }
        return posicaoDeMaiorDistancia;
    }
}
