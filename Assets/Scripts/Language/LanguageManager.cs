using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{
    public Dropdown dropdownIdioma; // Dropdown Legacy
    public List<TextUI> textosUI;   // Lista de textos a traduzir

    private Dictionary<string, Dictionary<string, string>> traducoes = new Dictionary<string, Dictionary<string, string>>();
    private string idiomaAtual = "Portugues (Brasil)";

    void Start()
    {
        // Traduções
        //PORTUGUES - PT-BR
        traducoes["Portugues (Brasil)"] = new Dictionary<string, string>
        {
            //LETRAS DO MENU
            { "jogar", "jogar" },
            { "opcoes", "opcoes" },
            { "sair", "sair" },
            //LETRAS DAS OPÇÕES
            { "volumeOpcoes", "volume" },
            { "idiomaOpcoes", "idioma" },
            { "voltarOpcoes", "voltar" },
            { "sairOpcoes", "sair" },
            { "inicioOpcoes", "inicio" },
        };

        //INGLES 
        traducoes["Ingles"] = new Dictionary<string, string>
        {
            //LETRAS DO MENU
            { "jogar", "play" },
            { "opcoes", "option" },
            { "sair", "quit" },
            //LETRAS DAS OPÇÕES
            { "volumeOpcoes", "volume" },
            { "idiomaOpcoes", "language" },
            { "voltarOpcoes", "back" },
            { "sairOpcoes", "quit" },
            { "inicioOpcoes", "menu" },
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
