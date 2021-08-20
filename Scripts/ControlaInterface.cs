using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour
{
    private  ControlaJogador scriptControlaJogador;
    public Slider SliderVidaJogador;
    public GameObject PainelDeGameOver;
    public GameObject PainelTituloJogo;
    public Text TextoTempoDeSobrevivencia;
    public Text TextoPontuacaoMaxima;
    private float tempoPontuacaoSalvo;
    private int quantidadeDeZumbisMortos;
    public Text TextoQuantidadeDeZumbisMortos;
    public Text TextoChefeAparece;

    
    public void Start()
    {
        
        scriptControlaJogador = GameObject.FindWithTag("jogador")
        .GetComponent<ControlaJogador>();
        SliderVidaJogador.maxValue = scriptControlaJogador.statusJogador.Vida;
        AtualizarSliderVidaJogador();
        Time.timeScale = 1;
        tempoPontuacaoSalvo = PlayerPrefs.GetFloat("PontuacaoMaxima");
    }
    public void AtualizarSliderVidaJogador (){
            SliderVidaJogador.value = scriptControlaJogador.statusJogador.Vida;
        }
    public void AtualizarQuantidadeDeZumbisMortos()
    {
        quantidadeDeZumbisMortos++;
        TextoQuantidadeDeZumbisMortos.text = string.Format("x {0}", quantidadeDeZumbisMortos);
    }
    public void GameOver ()
    {
        PainelDeGameOver.SetActive(true);
        Time.timeScale = 0;
        int minutos = (int)(Time.timeSinceLevelLoad / 60);
        int segundos = (int)(Time.timeSinceLevelLoad % 60);
        // % resto
        TextoTempoDeSobrevivencia.text = "You survived for " + minutos + " minutes and " + segundos + " seconds!";
        AjustarPontuacaoMaxima(minutos, segundos);
    }
    void AjustarPontuacaoMaxima(int min, int seg)
    {

        if (Time.timeSinceLevelLoad > tempoPontuacaoSalvo)
        {
            tempoPontuacaoSalvo = Time.timeSinceLevelLoad;
            TextoPontuacaoMaxima.text = string.Format("Your record is {0} minutes and {1} seconds", min, seg);
            PlayerPrefs.SetFloat("PontuacaoMaxima", tempoPontuacaoSalvo);
        }
        if (TextoPontuacaoMaxima.text == "")
        {
            min = (int)tempoPontuacaoSalvo / 60;
            seg = (int)tempoPontuacaoSalvo % 60;
            TextoPontuacaoMaxima.text = string.Format("Your record is {0} minutes and {1} seconds", min, seg);
        }
    }

    public void Reiniciar ()
    {
        SceneManager.LoadScene("game");
    }
    public void AparecerTextoDoChefeCriado()
    {
        StartCoroutine(DesaparecerTexto(2, TextoChefeAparece));
    }
    IEnumerator DesaparecerTexto(float tempoDeSumico, Text textoParaSumir)
    {
        textoParaSumir.gameObject.SetActive(true);
        Color corTexto = textoParaSumir.color;
        corTexto.a = 1;
        textoParaSumir.color = corTexto;
        yield return new WaitForSeconds(1);
        float contador = 0;
        while (textoParaSumir.color.a > 0)
        {
            contador += (Time.deltaTime / tempoDeSumico);
            corTexto.a = Mathf.Lerp(1, 0, contador);
            textoParaSumir.color = corTexto;
            if (textoParaSumir.color.a <= 0)
            {
                textoParaSumir.gameObject.SetActive(false);
            }
            yield return null;
        }
    }
}
