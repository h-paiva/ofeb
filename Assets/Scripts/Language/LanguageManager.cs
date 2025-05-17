using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{
    public Dropdown dropdownIdioma; // Dropdown Legacy
    public List<TextUI> textosUI;   // Lista de textos a traduzir

    private Dictionary<string, Dictionary<string, string>> traducoes = new Dictionary<string, Dictionary<string, string>>();
    private string idiomaAtual = "Português";

    void Start()
    {
        // Traduções
        traducoes["PORTUGUES"] = new Dictionary<string, string>
        {
            { "novo jogo", "Novo Jogo" },
            { "ajuste", "Ajustes" },
            { "sair", "Sair" },
            { "volume", "Volume" },
            { "idioma", "Idioma" },
        };

        traducoes["INGLES"] = new Dictionary<string, string>
        {
            { "novo jogo", "New Game" },
            { "ajuste", "Settings" },
            { "sair", "Exit" },
            { "volume", "Volume" },
            { "idioma", "Language" },
        };

        traducoes["ITALIANO"] = new Dictionary<string, string>
        {
            { "novo jogo", "Nuovo Gioco" },
            { "ajuste", "Impostazioni" },
            { "sair", "Uscita" },
            { "volume", "Volume" },
            { "idioma", "Lingua" },
        };

        // Escuta o dropdown
        dropdownIdioma.onValueChanged.AddListener(delegate {
            TrocarIdioma(dropdownIdioma.options[dropdownIdioma.value].text);
        });

        TrocarIdioma(idiomaAtual); // Aplica o idioma padrão
    }

    public void TrocarIdioma(string novoIdioma)
    {
        if (!traducoes.ContainsKey(novoIdioma)) return;

        idiomaAtual = novoIdioma;

        foreach (TextUI item in textosUI)
        {
            if (traducoes[idiomaAtual].ContainsKey(item.chave))
            {
                item.text.text = traducoes[idiomaAtual][item.chave];
            }
        }
    }
}
