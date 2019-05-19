using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01ConsultaCEP.Servico.Modelo;
using App01ConsultaCEP.Servico;

namespace App01ConsultaCEP
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Botao.Clicked += BuscarCEP;



        }

        private void BuscarCEP(object sender, EventArgs args)
        {
            string cep = CEP.Text.Trim();

            if (isValidCEP(cep))
            {

                try
                {

                Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);

                    if (end != null)
                    {
                        Resultado.Text = string.Format("Endereço: {3} - {2} - {0}, {1} ", end.localidade, end.uf, end.logradouro, end.bairro);
                    } 
                    else
                    {
                        DisplayAlert("Erro", "O endereço para o cep informado não foi encontrado, cep:"+cep, "OK");
                    }
                } catch(Exception e)
                {
                    DisplayAlert("Erro Crítico", e.Message, "OK");
                }

            }

        }

        private bool isValidCEP(string cep)
        {
            bool valid = true;

            int NovoCEP = 0;
            if (!int.TryParse(cep, out NovoCEP))
            {
                DisplayAlert("ERRO", "Cep inválido! O CEP deve ser composto apenas por números.", "OK");
                valid = false;
            }

            if (cep.Length != 8)
            {
                DisplayAlert("ERRO", "Cep inválido! O CEP conter 8 caracteres", "OK");

                valid = false;
            }

            return valid;
        }
    }
}
