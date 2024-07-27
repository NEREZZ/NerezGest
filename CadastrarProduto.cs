using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vendas
{
    public partial class CadastrarProduto : Form
    {
        public CadastrarProduto()
        {
            InitializeComponent();
        }

        private void BotaoSalva_Click(object sender, EventArgs e)
        {
            // Referências diretas aos TextBox
            TextBox serialBox = this.serialBox;
            TextBox nomeBox = this.nomeBox;
            TextBox qtdBox = this.qtdBox;
            TextBox valorBox = this.valorBox;

            // Variáveis para armazenar os valores
            string serial = string.Empty;
            string nome = string.Empty;
            string qtd = string.Empty;
            string valor = string.Empty;

            // Verifica se todos os TextBox são válidos
            if (serialBox != null && nomeBox != null && qtdBox != null && valorBox != null)
            {
                //serial recebe a caixa de texto serialBox
                serial = serialBox.Text;
                // recebe a caixa de texto
                nome = nomeBox.Text;
                // se o nome do produto fort menor que 13 caracteres, ele adiciona espaço até dar 13
                if (nome.Length < 13)
                {
                    nome = nome.PadRight(13, ' ');
                }
                // recebe a caixa de texto
                qtd = qtdBox.Text;
                //recebe a caixa de texto3
                valor = valorBox.Text;
            }
            else
            {
                MessageBox.Show("Informações inválidas!");
                return; // Sai do método se algum TextBox for nulo
            }
            // se o nome do produto for mais que 13 caracteres
            while (nome.Length > 13)
            {
                MessageBox.Show("Produto não pode ter mais que 13 caracteres !");
                nome = null;
                CadastrarProduto cadastrarProduto = new CadastrarProduto();
                return;
            }

            // Valida e converte os valores
            if (long.TryParse(serial, out long serialLong) &&
                int.TryParse(qtd, out int qtdInt) &&
                double.TryParse(valor, out double valorDouble))
            {
                // tenta enviar os dados para escrita 
                try
                {
                    Estoque estoque = new Estoque();
                    estoque.EscreveArquivo(serialLong, nome, valorDouble, qtdInt);
                    MessageBox.Show("Produto cadastrado!");
                }
                // senao mostra mensagem
                catch (Exception ex)
                {
                    // Exibe uma mensagem de erro se a gravação falhar
                    MessageBox.Show("Erro ao salvar o produto: " + ex.Message);
                }
            }
            // se tudo for invalido, mostra mensagem
            else
            {
                MessageBox.Show("Um ou mais valores são inválidos!");
            }


        }


    }

        
}

