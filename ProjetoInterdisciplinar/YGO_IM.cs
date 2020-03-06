using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ProjetoInterdisciplinar
{
    public partial class YGO_IM : Form
    {
        public YGO_IM(int sessao = -1)
        {
            InitializeComponent();

            carregarRecursos();

            playerid = sessao;
        }

        public static int playerid = -1;

        public List<Carta> Cartas = new List<Carta>();

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);
        private PrivateFontCollection fonts = new PrivateFontCollection();
        public static Font minhaFonte;

        private int opcao_form = -1;

        void carregarRecursos()
        {
            var caminhoBG = Application.StartupPath + @"\images\background";
            this.BackgroundImage = Image.FromFile(caminhoBG.ToString() + "/1.jpg");

            if (playerid != -1)
            {
                btnJogar.Enabled = true;
                btnLogout.Visible = true;

                btnLogar.Visible = false;
                btnCadastrar.Visible = false;
            }
            else
            {
                MessageBoxManager.OK = "Continuar";
                MessageBoxManager.Yes = "ATAQUE";
                MessageBoxManager.No = "DEFESA";
                MessageBoxManager.Register();

                btnJogar.Enabled = false;
                btnLogout.Visible = false;

                btnLogar.Visible = true;
                btnCadastrar.Visible = true;
            }

            txtUsuario.Visible = false;
            txtPassword.Visible = false;

            dgvRanking.Visible = false;
            dgvRanking.DataSource = null;
  
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            byte[] fontData = Properties.Resources.medieval;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.medieval.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.medieval.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);
            minhaFonte = new Font(fonts.Families[0], 10.25F, FontStyle.Bold);

            lstCartas.Font = minhaFonte;

            btnJogar.Font = minhaFonte;
            btnSair.Font = minhaFonte;
            btnCartas.Font = minhaFonte;
            btnRanking.Font = minhaFonte;
            btnAjuda.Font = minhaFonte;
            btnLogar.Font = minhaFonte;
            btnCadastrar.Font = minhaFonte;
            btnSair.Font = minhaFonte;
            btnSobre.Font = minhaFonte;
            btnEnviar.Font = minhaFonte;

            lblSobre.Visible = false;
            lblSobre.Font = minhaFonte;
            lblSobre.Text = "Yu-Gi-Oh! IF Memories" +
                            "\nVersão 0.2 Beta" +
                            "\n" +
                            "\nEste programa tem como objetivo materializar um projeto proposto de" +
                            "\nacordo com os requisitos impostos pelas disciplinas escaladas no segundo" +
                            "\nsemestre de Análise e Desenvolvimento de Sistemas do IFSP - Campus Capivari." +
                            "\n" +
                            "\nDesenvolvido por:" +
                            "\nGuilherme Vilas Boas" + 
                            "\nHigor Gustavo" + 
                            "\nLucas Henrique Miranda" + 
                            "\nMatheus de Barros";

            lblAjuda.Visible = false;
            lblAjuda.Font = minhaFonte;
            lblAjuda.Text = "Yu-Gi-Oh! IF Memories" +
                            "\nComo Jogar" +
                            "\n" +
                            "\nYu-Gi-Oh! IF Memories é um Card Game que utiliza-se de pontuações de" +
                            "\nataque e defesa de cartas únicas para a simulação de uma batalha entre" +
                            "\njogador e bot." +
                            "\n" +
                            "\nTanto o BOT quanto o jogador possuem uma vida: HP. Inicialmente com 8000" +
                            "\nse essa vida chega a 0 o jogador/BOT perde." +
                            "\n" +
                            "\nAs cartas possuem dois MODOS: ATAQUE e DEFESA. Representados, respectivamente" +
                            "\npelas cores VERMELHO e AZUL." +
                            "\nOs modos das cartas nos dizem qual pontuação será considerada na jogada." +
                            "\nPor exemplo: Uma carta no tabuleiro do BOT possui 1000 pontos de ATAQUE e" +
                            "\n1500 de DEFESA e a mesma está em MODO DE DEFESA (AZUL). Portanto," +
                            "\nassim que o jogador atacá-lo, o ATAQUE de sua carta deverá ser maior que a" +
                            "\nDEFESA da carta do BOT!" +
                            "\n" +
                            "\nSe uma carta em modo de ATAQUE ataca outra carta também no modo de ATAQUE," +
                            "\na carta com maior ataque destruirá a outra inflingindo a DIFERENÇA no HP" +
                            "\nde quem teve a carta destruída. Caso ambas possuam valor EQUIVALENTE, ambas" +
                            "\nserão destruídas. Atacar uma carta no modo de DEFESA não atingira o" +
                            "\nBOT/jogador, somente destruirá a carta." +
                            "\n" +
                            "\nOs decks possuem um total de 40 cartas, inicializando a partida com 5 na mão" +
                            "\ncada um. Se as cartas esgotarem, o jogador perde.";

            Funcoes.tocarSom("menu");
        }

        void montarRanking()
        {
            Conexao conexao = new Conexao();
            conexao.conectar();

            SqlCommand cmd = new SqlCommand("sp_Sel_Players", conexao.conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            dgvRanking.DataSource = ds.Tables[0];

            dgvRanking.AutoResizeColumns();

            conexao.desconectar();
        }

        private void dgvRanking_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex == this.dgvRanking.NewRowIndex)
                return;

            if (e.ColumnIndex == this.dgvRanking.Columns["POSICAO"].Index)
            {
                e.Value = e.RowIndex + 1;
            }
        }

        void mostrarSobre(int opcao)
        {
            if (opcao == 1)
            {
                lblSobre.Visible = true;

                btnJogar.Visible = false;
                btnVoltar.Visible = true;
            }
            else
            {
                lblSobre.Visible = false;
                
                btnJogar.Visible = true;
                btnVoltar.Visible = true;
            }
        }

        void mostrarAjuda(int opcao)
        {
            if (opcao == 1)
            {
                lblAjuda.Visible = true;

                btnJogar.Visible = false;
                btnVoltar.Visible = true;
            }
            else
            {
                lblAjuda.Visible = false;

                btnJogar.Visible = true;
                btnVoltar.Visible = false;
            }
        }

        void mostrarForm(int opcao)
        {
            if (opcao == 1)
            {
                txtUsuario.Text = "";
                txtPassword.Text = "";

                txtUsuario.Visible = true;
                txtPassword.Visible = true;
                btnEnviar.Visible = true;

                btnJogar.Visible = false;
                btnVoltar.Visible = true;
            }
            else
            {
                txtUsuario.Text = "";
                txtPassword.Text = "";

                txtUsuario.Visible = false;
                txtPassword.Visible = false;
                btnEnviar.Visible = false;

                btnJogar.Visible = true;
                btnVoltar.Visible = false;
            }
        }

        void mostrarCartas(int opcao)
        {

            if (opcao == 1)
            {
                pctCarta.Visible = true;
                pctVerso.Visible = true;
                lstCartas.Visible = true;

                btnJogar.Visible = false;
                btnVoltar.Visible = true;
               
            } 
            else
            {
                pctCarta.Visible = false;
                pctVerso.Visible = false;
                lstCartas.Visible = false;

                btnJogar.Visible = true;
                btnVoltar.Visible = false;      
            }
        }

        void mostrarRanking(int opcao)
        {
            if (opcao == 1)
            {
                montarRanking();

                dgvRanking.Visible = true;

                btnJogar.Visible = false;
                btnVoltar.Visible = true;
            }
            else
            {
                dgvRanking.Visible = false;

                btnJogar.Visible = true;
                btnVoltar.Visible = false;
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            opcao_form = 0;

            Funcoes.tocarSom("click");

            mostrarSobre(0);
            mostrarAjuda(0);
            mostrarCartas(0);
            mostrarRanking(0);
            mostrarForm(1);
        }

        private void btnLogar_Click(object sender, EventArgs e)
        {
            opcao_form = 1;

            Funcoes.tocarSom("click");

            mostrarSobre(0);
            mostrarAjuda(0);
            mostrarCartas(0);
            mostrarRanking(0);
            mostrarForm(1);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Funcoes.tocarSom("click");

            playerid = -1;

            btnJogar.Enabled = false;
            btnLogout.Visible = false;
            btnLogar.Visible = true;
            btnCadastrar.Visible = true;

            MessageBox.Show("Logout realizado com sucesso!", "LOGIN");
        }

        private void btnRanking_Click(object sender, EventArgs e)
        {
            Funcoes.tocarSom("click");

            mostrarSobre(0);
            mostrarAjuda(0);
            mostrarForm(0);
            mostrarCartas(0);
            mostrarRanking(1);
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Funcoes.tocarSom("click");

            mostrarSobre(0);
            mostrarAjuda(0);
            mostrarForm(0);
            mostrarCartas(0);
            mostrarRanking(0);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Funcoes.tocarSom("click");

            Application.Exit();
        }

        private void btnJogar_Click(object sender, EventArgs e)
        {
            Funcoes.tocarSom("click");

            Game frmJogo = new Game();
            frmJogo.Show();
            this.Hide();
        }

        private void btnCartas_Click(object sender, EventArgs e)
        {
            Funcoes.tocarSom("click");

            Funcoes.listarCartas(Cartas, lstCartas);

            mostrarSobre(0);
            mostrarForm(0);
            mostrarRanking(0);
            mostrarAjuda(0);
            mostrarCartas(1);
        }

        private void btnAjuda_Click(object sender, EventArgs e)
        {
            Funcoes.tocarSom("click");

            mostrarSobre(0);
            mostrarForm(0);
            mostrarCartas(0);
            mostrarRanking(0);
            mostrarAjuda(1);
        }

        private void btnSobre_Click(object sender, EventArgs e)
        {
            Funcoes.tocarSom("click");

            mostrarAjuda(0);
            mostrarForm(0);
            mostrarRanking(0);
            mostrarCartas(0);
            mostrarSobre(1);
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            Funcoes.tocarSom("click");

            Conexao conexao = new Conexao();
            conexao.conectar();

            SqlCommand cmd = new SqlCommand("sp_Bsc_Player", conexao.conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pNm_Player", txtUsuario.Text).Direction = ParameterDirection.Input;
            SqlDataReader dr = cmd.ExecuteReader();

            if (opcao_form == 0)
            {
                if (dr.HasRows)
                {
                    MessageBox.Show("Já existe um usuário cadastrado com esse nick!", "ERRO");
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("sp_Ins_Player", conexao.conexao);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@pNm_Player", txtUsuario.Text).Direction = ParameterDirection.Input;
                    cmd2.Parameters.AddWithValue("@pDs_Senha", txtPassword.Text).Direction = ParameterDirection.Input;
                    cmd2.Parameters.AddWithValue("@pNr_Vitorias", 0).Direction = ParameterDirection.Input;
                    cmd2.Parameters.AddWithValue("@pNr_Derrotas", 0).Direction = ParameterDirection.Input;

                    cmd2.ExecuteReader();

                    MessageBox.Show("Usuário cadastrado com sucesso! Logue com o mesmo para jogar!", "LOGIN");

                    mostrarForm(0);
                }
            }
            else
            {
                if (dr.HasRows)
                {
                    string id = "";
                    string nome = "";
                    string senha = "";
                    string vitorias = "";
                    string derrotas = "";

                    while (dr.Read())
                    {
                        id = dr["ID_Player"].ToString();
                        nome = dr["Nm_Player"].ToString();
                        senha = dr["Ds_Senha"].ToString();
                        vitorias = dr["Nr_Vitorias"].ToString();
                        derrotas = dr["Nr_Derrotas"].ToString();
                    }

                    if (txtPassword.Text == senha)
                    {
                        playerid = int.Parse(id);

                        btnJogar.Enabled = true;
                        btnLogout.Visible = true;

                        btnLogar.Visible = false;
                        btnCadastrar.Visible = false;

                        MessageBox.Show("Logado com sucesso!", "LOGIN");

                        mostrarForm(0);
                    }
                    else
                    {
                        MessageBox.Show("Senha incorreta!", "LOGIN");
                    }
                }
                else
                {
                    MessageBox.Show("Usuário inexistente!", "ERRO");
                }
                dr.Close();
            }

            conexao.desconectar();
        }

        private void lstCartas_SelectedIndexChanged(object sender, EventArgs e)
        {
            string item = lstCartas.SelectedValue.ToString();

            var caminhoCartas = Application.StartupPath + @"\images\cards";
            string cartaAtual = lstCartas.SelectedValue.ToString();
            try
            {
                Funcoes.tocarSom("carta");
                pctCarta.Image = Image.FromFile(caminhoCartas.ToString() + "/" + item + ".jpg");
                pctCarta.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch
            {

            }
        }

        private void YGO_IM_Load(object sender, EventArgs e)
        {

        }
    }
}
