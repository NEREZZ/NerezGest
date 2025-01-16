using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vendas
{
    public partial class Estoque : Form
    {
        // Construtor da classe Estoque
        public Estoque()
        {
            InitializeComponent();
            Vendas vendas = new Vendas(); // Cria uma instância da classe Vendas
            this.Width = vendas.Width; // Define a largura do formulário Estoque igual à largura do formulário Vendas
            this.Height = vendas.Height; // Define a altura do formulário Estoque igual à altura do formulário Vendas
        }

        // Método para escrever no arquivo de estoque
        public void EscreveArquivo(long serial, String nome, double valor, int quantidade)
        {
            string arq = @"C:\Users\pedro\OneDrive\Área de Trabalho\PROJETOS\Vendas\Vendas\estoque.txt";
            using (StreamWriter escritor = new StreamWriter(arq, true)) // Abre o arquivo para escrita, permitindo adicionar texto ao final do arquivo
            {
                // Escreve uma linha no arquivo com os dados fornecidos
                escritor.WriteLine($" {serial} | {nome} | {valor:F2} | 0{quantidade} |");
            }
        }

        // Método para ler do arquivo de estoque
        public void LeArquivo()
        {
            string arq = @"C:\Users\pedro\OneDrive\Área de Trabalho\PROJETOS\Vendas\Vendas\estoque.txt";

            if (File.Exists(arq)) // Verifica se o arquivo existe
            {
                // Lê todas as linhas do arquivo
                var linhas = File.ReadAllLines(arq);

                // Inicializa uma matriz de strings com o tamanho das linhas lidas
                string[][] texto = new string[linhas.Length][];

                // Processa cada linha do arquivo
                for (int i = 0; i < linhas.Length; i++)
                {
                    string linha = linhas[i];

                    // Divide a linha em partes separadas pelo caractere '|'
                    texto[i] = linha.Split('|');
                    // Adiciona cada parte da linha a diferentes listas de exibição
                    serialList.Items.Add(texto[i][0]);
                    prodList.Items.Add(texto[i][1]);
                    valorList.Items.Add(texto[i][2]);
                    quantList.Items.Add(texto[i][3]);
                }
            }
            else // Caso o arquivo não exista
            {
                MessageBox.Show("Arquivo não encontrado ou estoque vazio!"); // Exibe uma mensagem de erro
            }
        }

        // Método vazio chamado swap (não faz nada atualmente)
        public void swap(String texto)
        {
        }

        // Método chamado quando o formulário Estoque é carregado
        private void Estoque_Load(object sender, EventArgs e)
        {
            LeArquivo(); // Chama o método LeArquivo para ler o estoque quando o formulário for carregado
        }
    }
}
