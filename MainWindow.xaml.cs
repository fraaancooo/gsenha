using Microsoft.Win32;
using System;
using System.Reflection;
using System.Windows;

namespace GSenha
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            Version Versao = Assembly.GetExecutingAssembly().GetName().Version;
            statusbaritemVersao.Content = String.Format("Versão {0}.{1}.{2} Revisão {3}", Versao.Major, Versao.Minor, Versao.Build, Versao.Revision);

        }

        private void ButtonGerar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Random random;
                String senhaGerada = String.Empty;
                String caracteres = String.Empty;
                String maiusculos = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                String minusculos = "abcdefghijklmnopqrstuvwxyz";
                String numeros = "1234567890";
                String especiais = "!@#$%&_.";

                if (checkboxMaiusculos.IsChecked == true)
                {
                    caracteres += maiusculos;
                }

                if (checkboxMinusculos.IsChecked == true)
                {
                    caracteres += minusculos;
                }

                if (checkboxNumeros.IsChecked == true)
                {
                    caracteres += numeros;
                }

                if (checkboxEspeciais.IsChecked == true)
                {
                    caracteres += especiais;
                }

                textboxSenhasGeradas.Text = String.Empty;

                for (int i = 0; i < Convert.ToInt32(textboxNumeroDeSenhas.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(textboxNumeroDeCaracteres.Text); j++)
                    {
                        random = new Random(DateTime.Now.Millisecond);
                        System.Threading.Thread.Sleep(1);
                        senhaGerada += caracteres[random.Next(0, caracteres.Length)].ToString();
                    }
                    textboxSenhasGeradas.Text += senhaGerada + Environment.NewLine;
                    senhaGerada = String.Empty;
                }

                this.buttonSalvar.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonSalvar_Click(object sender, RoutedEventArgs e)
        {
            FileDialog pasta = new SaveFileDialog();
            var resultado = pasta.ShowDialog();

            if (resultado == true)
            {
                try
                {
                    System.IO.File.WriteAllText(pasta.FileName.ToString() + ".txt", textboxSenhasGeradas.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                MessageBox.Show("Arquivo salvo!");
            }
        }
    }
}
