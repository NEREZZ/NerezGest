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
using System.Runtime.Serialization.Formatters;



namespace Vendas
{
    public partial class Vendas : Form
    {
        private Timer autoSendTimer; // Timer para envio automático de dados
        private const int autoSendInterval = 200; // Intervalo de 200 milissegundos

        public Vendas()
        {
            InitializeComponent(); // Inicializa os componentes do formulário

            // Inicializa o Timer
            autoSendTimer = new Timer();
            autoSendTimer.Interval = autoSendInterval; // Define o intervalo do Timer
            autoSendTimer.Tick += AutoSendTimer_Tick; // Associa o evento Tick ao método AutoSendTimer_Tick
            this.Width = 608; // Define a largura do formulário
            this.Height = 395; // Define a altura do formulário
        }

        // Evento disparado ao clicar no botão Vendas
        private void BotaoVendas_Click(object sender, EventArgs e)
        {
            Vendas vendas = new Vendas(); // Cria uma nova instância do formulário Vendas
            vendas.Show(); // Mostra o formulário Vendas
            vendas.FormClosed += new FormClosedEventHandler(vendasFechou); // Associa o evento FormClosed ao método vendasFechou
        }

        // Evento disparado quando o formulário Vendas é fechado
        private void vendasFechou(object sender, EventArgs e)
        {
            this.Show(); // Mostra o formulário atual
        }

        // Evento disparado ao clicar no botão Estoque
        private void BotaoEstoque_Click(object sender, EventArgs e)
        {
            Estoque estoque = new Estoque(); // Cria uma nova instância do formulário Estoque
            estoque.Show(); // Mostra o formulário Estoque
            this.Hide(); // Esconde o formulário atual
            estoque.FormClosed += new FormClosedEventHandler(estoqueFechado); // Associa o evento FormClosed ao método estoqueFechado
        }

        // Evento disparado quando o formulário Estoque é fechado
        private void estoqueFechado(object sender, EventArgs e)
        {
            this.Show(); // Mostra o formulário atual
        }

        // Evento disparado ao clicar no botão Relatório de Vendas
        private void BotaoRelatorioVendas_Click(object sender, EventArgs e)
        {
            RelatorioVendas relatorioVendas = new RelatorioVendas(); // Cria uma nova instância do formulário Relatório de Vendas
            relatorioVendas.Show(); // Mostra o formulário Relatório de Vendas
            this.Hide(); // Esconde o formulário atual
            relatorioVendas.FormClosed += new FormClosedEventHandler(relatorioFechado); // Associa o evento FormClosed ao método relatorioFechado
        }

        // Evento disparado quando o formulário Relatório de Vendas é fechado
        private void relatorioFechado(object sender, EventArgs e)
        {
            this.Show(); // Mostra o formulário atual
        }

        // Evento disparado ao clicar no botão Remover Produto
        private void RemoverProduto_Click_1(object sender, EventArgs e)
        {
            RemoverProduto remover = new RemoverProduto(); // Cria uma nova instância do formulário Remover Produto
            remover.Show(); // Mostra o formulário Remover Produto
            this.Hide(); // Esconde o formulário atual
            remover.FormClosed += new FormClosedEventHandler(removeFechado); // Associa o evento FormClosed ao método removeFechado
        }

        // Evento disparado quando o formulário Remover Produto é fechado
        private void removeFechado(object sender, EventArgs e)
        {
            this.Show(); // Mostra o formulário atual
        }

        // Evento disparado ao clicar no botão Cadastrar Produto
        private void botaoCadastra_Click(object sender, EventArgs e)
        {
            CadastrarProduto cadastra = new CadastrarProduto(); // Cria uma nova instância do formulário Cadastrar Produto
            cadastra.Show(); // Mostra o formulário Cadastrar Produto
            this.Hide(); // Esconde o formulário atual
            cadastra.FormClosed += new FormClosedEventHandler(cadastras); // Associa o evento FormClosed ao método cadastras
        }

        // Evento disparado quando o formulário Cadastrar Produto é fechado
        private void cadastras(object sender, FormClosedEventArgs e)
        {
            this.Show(); // Mostra o formulário atual
        }

        // Evento disparado quando o texto na caixa de texto boxProd é alterado
        private void boxProd_TextChanged(object sender, EventArgs e)
        {
            autoSendTimer.Stop(); // Para o Timer
            autoSendTimer.Start(); // Reinicia o Timer
        }

        // Evento disparado quando o Timer atinge o intervalo definido
        private void AutoSendTimer_Tick(object sender, EventArgs e)
        {
            autoSendTimer.Stop(); // Para o Timer

            SendData(boxProd.Text); // Envia os dados
        }

        // Método para enviar dados
        private void SendData(string data)
        {
            VerificaArquivo(long.Parse(data)); // Chama o método VerificaArquivo passando os dados convertidos para long
        }

        // Método para verificar o arquivo de estoque
        public void VerificaArquivo(long prod)
        {
            string arq = @"C:\Users\pedro\OneDrive\Área de Trabalho\PROJETOS\Vendas\Vendas\estoque.txt";

            if (File.Exists(arq)) // Verifica se o arquivo existe
            {
                var linhas = File.ReadAllLines(arq); // Lê todas as linhas do arquivo
                string[][] texto = new string[linhas.Length][]; // Inicializa a matriz de strings
                double soma = 0; // Variável para somar os valores
                bool ProdutoEncontrado = false; // Flag para verificar se o produto foi encontrado

                // Processa cada linha
                for (int i = 0; i < linhas.Length; i++)
                {
                    string linha = linhas[i];
                    texto[i] = linha.Split('|').Select(s => s.Trim()).ToArray(); // Remove espaços em branco e divide a linha

                    if (prod.ToString().Equals(texto[i][0])) // Verifica se o produto corresponde
                    {
                        ProdutoEncontrado = true; // Marca o produto como encontrado
                        if (double.TryParse(texto[i][2], out double valor)) // tenta converter o dado pra double e adiciona ele em valor
                        {
                            soma += valor; // deveria somar
                        }
                        else
                        {
                            MessageBox.Show($"Erro ao converter o valor: {texto[i][2]}"); // Mostra mensagem de erro
                        }
                    }
                }

                if (!ProdutoEncontrado) // Se o produto não foi encontrado
                {
                    MessageBox.Show("Produto não encontrado ou não existe!"); // Mostra mensagem de produto não encontrado
                }
                else
                {
                    boxTotal.Text = soma.ToString("F2"); // Mostra a soma na caixa de texto formatada com duas casas decimais
                }
            }
            else // Se o arquivo não existe
            {
                MessageBox.Show("Arquivo não encontrado"); // Mostra mensagem de arquivo não encontrado
            }
        }
    }
}
