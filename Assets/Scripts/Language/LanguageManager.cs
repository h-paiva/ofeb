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
        //PORTUGUES - PT-BR
        traducoes["PORTUGUES"] = new Dictionary<string, string>
        {
            //LETRAS DO MENU
            { "novo jogo", "<color=#feae34>Novo Jogo" },
            { "ajuste", "<color=#feae34>Ajustes" },
            { "sair", "<color=#feae34>Sair</color>" },
            //LETRAS DAS OPÇÕES
            { "volume", "<color=#feae34>VOLUME</color>" },
            { "idioma", "<color=#feae34>IDIOMA</color>" },
            { "sairAjuste", "SAIR" },
        };

        //INGLES 
        traducoes["INGLES"] = new Dictionary<string, string>
        {
            { "novo jogo", "<color=#feae34>New Game</color>" },
            { "ajuste", "<color=#feae34>Settings</color>" },
            { "sair", "<color=#feae34>Exit</color>" },
            { "volume", "<color=#feae34>VOLUME</color>" },
            { "idioma", "<color=#feae34>Language</color>" },
            { "sairAjuste", "Exit" },
        };

        /*
        //ITALIANO
        traducoes["ITALIANO"] = new Dictionary<string, string>
        {
            { "novo jogo", "<color=#feae34>Nuovo Gioco</color>" },
            { "ajuste", "<color=#feae34>Impostazioni</color>" },
            { "sair", "<color=#feae34>Uscita</color>" },
            { "volume", "<color=#feae34>VOLUME</color>" },
            { "idioma", "<color=#feae34>Lingua</color>" },
            { "sairAjuste", "Uscita" },
        };
        */

        // Escuta o dropdown
        dropdownIdioma.onValueChanged.AddListener(delegate
        {
            TrocarIdioma(dropdownIdioma.options[dropdownIdioma.value].text);
        });

        // SALVA O ULTIMO IDIOMA QUE O PLAYER DEIXOU
        if (PlayerPrefs.HasKey("idiomaEscolhido"))
        {
            idiomaAtual = PlayerPrefs.GetString("idiomaEscolhido");
            // Seleciona o valor certo no Dropdown
            int index = dropdownIdioma.options.FindIndex(option => option.text == idiomaAtual);
            if (index != -1)
                dropdownIdioma.value = index;
        }
        TrocarIdioma(idiomaAtual);
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

        PlayerPrefs.SetString("idiomaEscolhido", novoIdioma);
        PlayerPrefs.Save();
    }
}
