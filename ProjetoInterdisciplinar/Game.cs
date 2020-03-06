using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ProjetoInterdisciplinar
{
    public partial class Game : Form
    {
        public Game()
        {
            InitializeComponent();

            carregarRecursos();
        }

        private int Player_hp = 8000;
        private int BOT_hp = 8000;

        private int Player_cartasNoTab = 0;
        private int BOT_cartasNoTab = 0;

        private int totalCartasUsadas = 0;
        private int BOT_totalCartasUsadas = 0;

        private int[] modoCartaTab = new int[5];
        private int[] BOT_modoCarta = new int[5];

        private BitArray BOT_cartaDisponivel = new BitArray(40);

        private bool cartasUtilizadasP = false;

        private int cartaSelecionada;

        private int acao = -1; // 0 = ESCOLHER CARTA NOVA DO DECK / 1 = SELECIONAR CARTA PARA ATACAR O INIMIGO / 2 = SELECIONAR CARTA DO INIMIGO / 3 = SUBSTITUIR CARTA DO TABULEIRO

        private int jogada = 0;

        private bool jogoAcabou = false;

        private PictureBox[] cartaDeck = new PictureBox[5];
        private PictureBox[] cartaTabP = new PictureBox[5];
        private PictureBox[] cartaTabB = new PictureBox[5];

        private ToolTip[] tooltipDeck = new ToolTip[5];
        private ToolTip[] tooltipTabP = new ToolTip[5];
        private ToolTip[] tooltipTabB = new ToolTip[5];

        private void carregarRecursos()
        {
            var caminhoBG = Application.StartupPath + @"\images\background";
            this.BackgroundImage = Image.FromFile(caminhoBG.ToString() + "/2.jpg");

            btnFinalizarTurno.Visible = false;

            gpbTabuleiroB.MouseClick += gpbTabuleiroB_Click;

            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            cartaDeck[0] = pctCarta_Deck_1;
            cartaDeck[1] = pctCarta_Deck_2;
            cartaDeck[2] = pctCarta_Deck_3;
            cartaDeck[3] = pctCarta_Deck_4;
            cartaDeck[4] = pctCarta_Deck_5;

            cartaTabP[0] = pctCarta_TabP_1;
            cartaTabP[1] = pctCarta_TabP_2;
            cartaTabP[2] = pctCarta_TabP_3;
            cartaTabP[3] = pctCarta_TabP_4;
            cartaTabP[4] = pctCarta_TabP_5;

            cartaTabB[0] = pctCarta_TabB_1;
            cartaTabB[1] = pctCarta_TabB_2;
            cartaTabB[2] = pctCarta_TabB_3;
            cartaTabB[3] = pctCarta_TabB_4;
            cartaTabB[4] = pctCarta_TabB_5;

            tooltipDeck[0] = tltCarta_Deck_1;
            tooltipDeck[1] = tltCarta_Deck_2;
            tooltipDeck[2] = tltCarta_Deck_3;
            tooltipDeck[3] = tltCarta_Deck_4;
            tooltipDeck[4] = tltCarta_Deck_5;

            tooltipTabP[0] = tltCarta_TabP_1;
            tooltipTabP[1] = tltCarta_TabP_2;
            tooltipTabP[2] = tltCarta_TabP_3;
            tooltipTabP[3] = tltCarta_TabP_4;
            tooltipTabP[4] = tltCarta_TabP_5;

            tooltipTabB[0] = tltCarta_TabB_1;
            tooltipTabB[1] = tltCarta_TabB_2;
            tooltipTabB[2] = tltCarta_TabB_3;
            tooltipTabB[3] = tltCarta_TabB_4;
            tooltipTabB[4] = tltCarta_TabB_5;

            gpbDeck.Size = new Size(1020, 310);
            gpbTabuleiroP.Size = new Size(690, 210);
            gpbTabuleiroB.Size = new Size(690, 210);

            for (int x = 0; x < 5; x++)
            {
                cartaDeck[x].Size = new Size(200, 280);
                cartaTabP[x].Size = new Size(130, 180);
                cartaTabB[x].Size = new Size(130, 180);

                cartaDeck[x].Tag = "" + -1;
                cartaTabP[x].Tag = "" + -1;
                cartaTabB[x].Tag = "" + -1;

                cartaDeck[x].Visible = false;
                cartaTabP[x].Visible = false;
                cartaTabB[x].Visible = false;

                cartaDeck[x].SizeMode = PictureBoxSizeMode.Zoom;
                cartaTabB[x].SizeMode = PictureBoxSizeMode.Zoom;
                cartaTabP[x].SizeMode = PictureBoxSizeMode.Zoom;

                cartaTabB[x].BackColor = Color.Blue;
                cartaTabP[x].BackColor = Color.Blue;
            }

            lblNick.Text = Funcoes.retornarNomePlayer(YGO_IM.playerid);

            btnFinalizarTurno.Font = YGO_IM.minhaFonte;
            btnQuitar.Font = YGO_IM.minhaFonte;

            Funcoes.popularDeck(0);
            Funcoes.popularDeck(YGO_IM.playerid);

            Funcoes.pegar5Cartas(YGO_IM.playerid, cartaDeck, tooltipDeck);
            totalCartasUsadas += 5;

            atualizarLabels();

            acao = 0;

            Funcoes.tocarSom("game");
        }

        private bool verificarExistenciaCartasTab(PictureBox[] cartasTab)
        {
            for (int x = 0; x < 5; x++) if (cartasTab[x].Tag.ToString() != "-1") return true;

            return false;
        }

        private void atualizarCartasNoTab()
        {
            BOT_cartasNoTab = 0;
            Player_cartasNoTab = 0;

            cartasUtilizadasP = true;

            for (int x = 0; x < 5; x++)
            {
                if (cartaTabP[x].Tag.ToString() != "-1")
                {
                    if (cartaTabP[x].BackColor != Color.Gray) cartasUtilizadasP = false;

                    Player_cartasNoTab++;
                }
                if (cartaTabB[x].Tag.ToString() != "-1")
                {
                    BOT_cartasNoTab++;
                }
            }
        }

        private void atualizarLabels()
        {
            lblHpBOT.Text = "" + BOT_hp;
            lblHpPlayer.Text = "" + Player_hp;

            lblCartasRestantesP.Text = "" + (40 - totalCartasUsadas);
            lblCartasRestantesB.Text = "" + (40 - (BOT_totalCartasUsadas + 5));
        }

        private bool checarCondicoes()
        {
            if (jogoAcabou != true)
            {
                if (BOT_hp <= 0 || (BOT_totalCartasUsadas + 5) > 40)
                {
                    jogoAcabou = true;

                    Funcoes.atualizarVDPlayer(YGO_IM.playerid, 1);
                    Funcoes.atualizarVDPlayer(0, 0);

                    MessageBox.Show("Você ganhou!", "GAME");

                    var frmMenu = new YGO_IM();
                    frmMenu.Show();
                    this.Close();

                    return true;
                }
                else if (Player_hp <= 0 || totalCartasUsadas > 40)
                {
                    jogoAcabou = true;

                    Funcoes.atualizarVDPlayer(YGO_IM.playerid, 0);

                    MessageBox.Show("Você perdeu!", "GAME");
                    Funcoes.atualizarVDPlayer(0, 1);

                    var frmMenu = new YGO_IM();
                    frmMenu.Show();
                    this.Close();

                    return true;
                }
            }
            return false;
        }

        private void BOT_executarJogada(PictureBox[] cartaTabB, ToolTip[] tooltipTabB, PictureBox[] cartaTabP)
        {
            Conexao conexao = new Conexao();
            conexao.conectar();

            SqlCommand cmd = new SqlCommand("sp_Sel_Prox_Carta_Deck", conexao.conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pID_Player", 0).Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@pNr_PosDeck", ++BOT_totalCartasUsadas).Direction = ParameterDirection.Input;
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                string id = "";
                string nome = "";
                string atk = "";
                string def = "";

                var caminhoCartas = Application.StartupPath + @"\images\cards";

                string textoTooltip = "";

                while (dr.Read())
                {
                    id = dr["ID_Carta"].ToString();
                    nome = dr["Nm_Carta"].ToString();
                    atk = dr["Vl_Atk"].ToString();
                    def = dr["Vl_Def"].ToString();
                    Funcoes.tocarSom("carta");

                    bool achou1 = false;

                    for (int x = 0; x < 5 && achou1 == false; x++)
                    {
                        bool result = cartaTabB[x].Tag.Equals("-1");

                        if (result == true)
                        {
                            bool achou2 = false;

                            BOT_modoCarta[x] = 1;
                            textoTooltip = nome.Trim() + "\nATK : " + atk + "\nDEF : " + def + "\nMODO : ATAQUE";
                            cartaTabB[x].BackColor = Color.Red;

                            for (int y = 0; y < 5 && achou2 == false; y++)
                            {
                                SqlCommand cmd2 = new SqlCommand("sp_Sel_Carta_ESP", conexao.conexao);
                                cmd2.CommandType = CommandType.StoredProcedure;
                                cmd2.Parameters.AddWithValue("@pID_Player", YGO_IM.playerid).Direction = ParameterDirection.Input;
                                cmd2.Parameters.AddWithValue("@pID_Carta", cartaTabP[y].Tag).Direction = ParameterDirection.Input;
                                SqlDataReader dr2 = cmd2.ExecuteReader();

                                if (dr2.HasRows)
                                {
                                    string atk2 = "";
                                    string def2 = "";

                                    while (dr2.Read())
                                    {
                                        atk2 = dr2["Vl_Atk"].ToString();
                                        def2 = dr2["Vl_Def"].ToString();

                                        Funcoes.tocarSom("carta");

                                        if (cartaTabP[y].Tag.ToString() != "-1")
                                        {
                                            if (modoCartaTab[y] == 1)
                                            {
                                                if (int.Parse(atk2) >= int.Parse(atk))
                                                {
                                                    BOT_modoCarta[x] = 0;
                                                    textoTooltip = nome.Trim() + "\nATK : " + atk + "\nDEF : " + def + "\nMODO : DEFESA";

                                                    cartaTabB[x].BackColor = Color.Blue;

                                                    achou2 = true;
                                                }
                                            }
                                            else
                                            {
                                                if (int.Parse(def2) >= int.Parse(atk))
                                                {
                                                    BOT_modoCarta[x] = 0;
                                                    textoTooltip = nome.Trim() + "\nATK : " + atk + "\nDEF : " + def + "\nMODO : DEFESA";

                                                    cartaTabB[x].BackColor = Color.Blue;

                                                    achou2 = true;
                                                }
                                            }
                                        }
                                    }
                                }

                                dr2.Close();
                            }

                            cartaTabB[x].Visible = true;
                            cartaTabB[x].Image = Image.FromFile(caminhoCartas.ToString() + "/" + id + ".jpg");
                            cartaTabB[x].Tag = "" + id;
                            tooltipTabB[x].IsBalloon = true;
                            tooltipTabB[x].SetToolTip(cartaTabB[x], textoTooltip);

                            achou1 = true;
                        }
                    }

                    if (achou1 == false)
                    {
                        bool achou3 = false;

                        for (int z = 0; z < 5 && achou3 == false; z++)
                        {
                            SqlCommand cmd3 = new SqlCommand("sp_Sel_Carta_ESP", conexao.conexao);
                            cmd3.CommandType = CommandType.StoredProcedure;
                            cmd3.Parameters.AddWithValue("@pID_Player", 0).Direction = ParameterDirection.Input;
                            cmd3.Parameters.AddWithValue("@pID_Carta", cartaTabB[z].Tag).Direction = ParameterDirection.Input;
                            SqlDataReader dr3 = cmd3.ExecuteReader();

                            if (dr3.HasRows)
                            {
                                string atk3 = "";
                                string def3 = "";

                                while (dr3.Read())
                                {
                                    atk3 = dr3["Vl_Atk"].ToString();
                                    def3 = dr3["Vl_Def"].ToString();
                                }

                                if (int.Parse(atk3) > int.Parse(atk))
                                {
                                    BOT_modoCarta[z] = 1;
                                    textoTooltip = nome.Trim() + "\nATK : " + atk + "\nDEF : " + def + "\nMODO : ATAQUE";

                                    cartaTabB[z].BackColor = Color.Red;

                                    cartaTabB[z].Visible = true;
                                    cartaTabB[z].Image = Image.FromFile(caminhoCartas.ToString() + "/" + id + ".jpg");
                                    cartaTabB[z].Tag = "" + id;
                                    tooltipTabB[z].IsBalloon = true;
                                    tooltipTabB[z].SetToolTip(cartaTabB[z], textoTooltip);

                                    achou3 = true;
                                }
                                else if (int.Parse(def3) > int.Parse(def))
                                {
                                    BOT_modoCarta[z] = 0;
                                    textoTooltip = nome.Trim() + "\nATK : " + atk + "\nDEF : " + def + "\nMODO : DEFESA";

                                    cartaTabB[z].BackColor = Color.Blue;

                                    cartaTabB[z].Visible = true;
                                    cartaTabB[z].Image = Image.FromFile(caminhoCartas.ToString() + "/" + id + ".jpg");
                                    cartaTabB[z].Tag = "" + id;
                                    tooltipTabB[z].IsBalloon = true;
                                    tooltipTabB[z].SetToolTip(cartaTabB[z], textoTooltip);

                                    achou3 = true;
                                }
                            }
                            dr3.Close();
                        }

                        if (achou3 == false)
                        {
                            if (int.Parse(atk) >= int.Parse(def))
                            {
                                BOT_modoCarta[4] = 1;
                                textoTooltip = nome.Trim() + "\nATK : " + atk + "\nDEF : " + def + "\nMODO : ATAQUE";

                                cartaTabB[4].BackColor = Color.Red;

                                cartaTabB[4].Visible = true;
                                cartaTabB[4].Image = Image.FromFile(caminhoCartas.ToString() + "/" + id + ".jpg");
                                cartaTabB[4].Tag = "" + id;
                                tooltipTabB[4].IsBalloon = true;
                                tooltipTabB[4].SetToolTip(cartaTabB[4], textoTooltip);
                            }
                        }
                    }
                }
            }

            dr.Close();

            for (int y = 0; y < 5; y++)
            {
                SqlCommand cmd4 = new SqlCommand("sp_Sel_Carta_ESP", conexao.conexao);
                cmd4.CommandType = CommandType.StoredProcedure;
                cmd4.Parameters.AddWithValue("@pID_Player", 0).Direction = ParameterDirection.Input;
                cmd4.Parameters.AddWithValue("@pID_Carta", cartaTabB[y].Tag).Direction = ParameterDirection.Input;
                SqlDataReader dr4 = cmd4.ExecuteReader();

                if (dr4.HasRows)
                {
                    string atk = "";
                    string def = "";

                    while (dr4.Read())
                    {
                        atk = dr4["Vl_Atk"].ToString();
                        def = dr4["Vl_Def"].ToString();
                    }

                    if (verificarExistenciaCartasTab(cartaTabP) == true)
                    {
                        for (int x = 0; x < 5; x++)
                        {
                            SqlCommand cmd5 = new SqlCommand("sp_Sel_Carta_ESP", conexao.conexao);
                            cmd5.CommandType = CommandType.StoredProcedure;
                            cmd5.Parameters.AddWithValue("@pID_Player", YGO_IM.playerid).Direction = ParameterDirection.Input;
                            cmd5.Parameters.AddWithValue("@pID_Carta", cartaTabP[x].Tag).Direction = ParameterDirection.Input;
                            SqlDataReader dr5 = cmd5.ExecuteReader();

                            if (dr5.HasRows)
                            {
                                string atk2 = "";
                                string def2 = "";

                                while (dr5.Read())
                                {
                                    atk2 = dr5["Vl_Atk"].ToString();
                                    def2 = dr5["Vl_Def"].ToString();
                                }

                                if (int.Parse(atk) > int.Parse(atk2) && cartaTabB[y].BackColor != Color.Gray )
                                {
                                    MessageBox.Show("Você foi atacado pelo BOT!", "ATAQUE");
                                    int ataque = Funcoes.atacarCarta(0, YGO_IM.playerid, cartaTabB[y], cartaTabP[x], x, modoCartaTab[x]);
                                    if (ataque > 0)
                                    {
                                        MessageBox.Show(ataque + " pontos de vida infringidos no jogador!", "ATAQUE");

                                        Player_hp -= ataque;
                                    }
                                    else if (ataque < 0)
                                    {
                                        MessageBox.Show(ataque + " pontos de vida rebatidos no BOT!", "ATAQUE");

                                        BOT_hp += ataque;
                                    }
                                    atualizarLabels();

                                    cartaTabB[y].BackColor = Color.Gray;

                                    if (checarCondicoes() == true) return;
                        
                                    if (BOT_modoCarta[y] == 0) BOT_modoCarta[y] = 1;
                                    Funcoes.atualizarToolTipCartas(0, cartaTabB, tooltipTabB, BOT_modoCarta);               
                                }
                            }

                            dr5.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Você foi atacado pelo BOT!", "ATAQUE");
                        int ataque = Funcoes.atacarCarta(0, YGO_IM.playerid, cartaTabB[y], null, -1, -1);
                        MessageBox.Show(ataque + " pontos de vida infringidos no jogador!", "ATAQUE");
                        Player_hp -= ataque;
                        atualizarLabels();

                        cartaTabB[y].BackColor = Color.Gray;

                        if (checarCondicoes() == true) return;
   
                        if (BOT_modoCarta[y] == 0) BOT_modoCarta[y] = 1;
                        Funcoes.atualizarToolTipCartas(0, cartaTabB, tooltipTabB, BOT_modoCarta);

                    }
                }
                dr4.Close();
            }

            for (int x = 0; x < 5; x++)
            {
                if (cartaTabB[x].Tag.ToString() != "-1")
                {
                    if (BOT_modoCarta[x] == 0)
                    {

                        cartaTabB[x].BackColor = Color.Blue;
                    }
                    else
                    {
                        cartaTabB[x].BackColor = Color.Red;
                    }
                }
            }

            conexao.desconectar();
        }

        private void carta_deck_Click(object sender, EventArgs e)
        {
            if (acao == 0)
            {
                atualizarLabels();

                if (checarCondicoes() == true) return;
                else
                {
                    int tlt = 0;

                    bool tabFull = true;

                    for (int x = 0; x < 5; x++)
                    {
                        if (cartaDeck[x] == (PictureBox)sender)
                        {
                            tlt = x;
                        }

                        if (cartaTabP[x].Tag.ToString() == "-1")
                        {
                            tabFull = false;
                        }
                    }

                    if (tabFull == true)
                    {
                        MessageBox.Show("Tabuleiro cheio! Escolha uma carta para ser substituida!", "GAME");

                        gpbDeck.Visible = false;

                        cartaSelecionada = tlt;

                        acao = 3;
                    }
                    else
                    {
                        bool achou = false;
                        int aux = -1;

                        for (int x = 0; x < 5 && achou == false; x++)
                        {

                            bool result = cartaTabP[x].Tag.Equals("-1");

                            if (result == true)
                            {
                                aux = x;
                                achou = true;
                            }
                        }

                        modoCartaTab[aux] = Funcoes.lancarCarta(YGO_IM.playerid, (PictureBox)sender, tooltipDeck[tlt], cartaTabP, tooltipTabP);
                        totalCartasUsadas++;
                        Funcoes.atualizarCarta(YGO_IM.playerid, totalCartasUsadas, (PictureBox)sender, tooltipDeck[tlt]);
                        atualizarCartasNoTab();

                        if (jogada == 0)
                        {
                            MessageBox.Show("VEZ DO BOT!", "GAME");
                            BOT_executarJogada(cartaTabB, tooltipTabB, cartaTabP);
                            BOT_cartasNoTab++;

                            if (checarCondicoes() == true) return;
                            else
                            {
                                MessageBox.Show("SUA VEZ!", "GAME");

                                gpbDeck.Visible = true;

                                jogada++;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Selecione uma carta para atacar ou passe a vez!", "GAME");

                            acao = 1;

                            gpbDeck.Visible = false;

                            btnFinalizarTurno.Visible = true;
                        }
                    }

                    atualizarLabels();
                }
            }
        }

        private void pctCarta_TabP_Click(object sender, EventArgs e)
        {
            PictureBox aux = (PictureBox)sender;

            if (acao == 1 && aux.BackColor != Color.Gray)
            {
                MessageBoxManager.Yes = "Atacar";
                MessageBoxManager.No = "Mudar modo";

                var escolha = MessageBox.Show("O que deseja fazer com esta carta?", "CARTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (escolha == DialogResult.Yes)
                {
                    if (aux.BackColor == Color.Blue)
                    {
                        MessageBoxManager.Yes = "Sim";
                        MessageBoxManager.No = "Não";

                        var certeza = MessageBox.Show("Esta carta está em modo defesa! Atacar com ela mudará o modo da mesma.\nDeseja continuar?", "CARTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (certeza == DialogResult.Yes)
                        {
                            for (int x = 0; x < 5; x++)
                            {
                                if (cartaTabP[x] == aux)
                                {
                                    modoCartaTab[x] = 1;
                                    Funcoes.atualizarToolTipCartas(YGO_IM.playerid, cartaTabP, tooltipTabP, modoCartaTab);

                                    cartaSelecionada = x;

                                    break;
                                }
                            }

                            acao = 2;
                        }
                    }
                    else
                    {
                        for (int x = 0; x < 5; x++)
                        {
                            if (cartaTabP[x] == aux)
                            {
                                cartaSelecionada = x;

                                break;
                            }
                        }

                        acao = 2;
                    }
                }
                else
                {
                    for (int x = 0; x < 5; x++)
                    {
                        if (cartaTabP[x] == aux)
                        {
                            if (modoCartaTab[x] == 1)
                            {
                                cartaTabP[x].BackColor = Color.Blue;
                                modoCartaTab[x] = 0;
                            }
                            else
                            {
                                cartaTabP[x].BackColor = Color.Red;
                                modoCartaTab[x] = 1;
                            }

                            Funcoes.atualizarToolTipCartas(YGO_IM.playerid, cartaTabP, tooltipTabP, modoCartaTab);

                            break;
                        }
                    }
                }
            }
            else if (acao == 3)
            {
                aux.Tag = "-1";

                btnFinalizarTurno.Visible = true;

                modoCartaTab[cartaSelecionada] = Funcoes.lancarCarta(YGO_IM.playerid, cartaDeck[cartaSelecionada], tooltipDeck[cartaSelecionada], cartaTabP, tooltipTabP);
                totalCartasUsadas++;
                Funcoes.atualizarCarta(YGO_IM.playerid, totalCartasUsadas, cartaDeck[cartaSelecionada], tooltipDeck[cartaSelecionada]);
                atualizarCartasNoTab();

                MessageBox.Show("Selecione uma carta para atacar!", "ATAQUE");

                acao = 1;

                gpbDeck.Visible = false;
            }
        }

        private void pctCarta_TabB_Click(object sender, EventArgs e)
        {
            if (acao == 2)
            {
                int cartaAtacada = 0;

                for (int x = 0; x < 5; x++)
                {
                    if (cartaTabB[x] == (PictureBox)sender)
                    {
                        cartaAtacada = x;

                        break;
                    }
                }

                MessageBox.Show("Você atacou o BOT!", "ATAQUE");
                int ataque = Funcoes.atacarCarta(YGO_IM.playerid, 0, cartaTabP[cartaSelecionada], (PictureBox)sender, cartaAtacada, BOT_modoCarta[cartaAtacada]);
                if (ataque > 0)
                {
                    MessageBox.Show(ataque + " pontos de vida infringidos no BOT!", "ATAQUE");

                    BOT_hp -= ataque;
                }
                else if(ataque < 0)
                {
                    MessageBox.Show(ataque + " pontos de vida rebatidos no jogador!", "ATAQUE");

                    Player_hp += ataque;
                }

                atualizarLabels();
                if (checarCondicoes() == true) return;
                else
                {
                    cartaTabP[cartaSelecionada].BackColor = Color.Gray;

                    atualizarCartasNoTab();

                    if (cartasUtilizadasP == true || Player_cartasNoTab == 0)
                    {
                        for (int x = 0; x < 5; x++)
                        {
                            if (cartaTabP[x].Tag.ToString() != "-1")
                            {
                                if (modoCartaTab[x] == 0) cartaTabP[x].BackColor = Color.Blue;
                                else cartaTabP[x].BackColor = Color.Red;
                            }
                        }

                        btnFinalizarTurno.Visible = false;

                        MessageBox.Show("VEZ DO BOT!", "GAME");
                        BOT_executarJogada(cartaTabB, tooltipTabB, cartaTabP);
                        BOT_cartasNoTab++;

                        if (checarCondicoes() == true) return;
                        else
                        {
                            MessageBox.Show("SUA VEZ!", "GAME");

                            gpbDeck.Visible = true;

                            jogada++;

                            acao = 0;
                        }
                    }
                    else
                    {
                        acao = 1;
                    }

                    atualizarCartasNoTab();
                }
            }
        }

        private void gpbTabuleiroB_Click(object sender, EventArgs e)
        {
            if (acao == 2)
            {
                if (!verificarExistenciaCartasTab(cartaTabB))
                {
                    MessageBox.Show("Você atacou o BOT diretamente!", "ATAQUE");
                    int ataque = Funcoes.atacarCarta(YGO_IM.playerid, 0, cartaTabP[cartaSelecionada], null, -1, -1);
                    MessageBox.Show(ataque + " pontos de vida infringidos no BOT!", "ATAQUE");
                    BOT_hp -= ataque;
                    atualizarLabels();
                    if (checarCondicoes() == true) return;
                    else
                    {

                        cartaTabP[cartaSelecionada].BackColor = Color.Gray;

                        atualizarCartasNoTab();

                        if (cartasUtilizadasP == true || Player_cartasNoTab == 0)
                        {
                            Funcoes.atualizarToolTipCartas(YGO_IM.playerid, cartaTabP, tooltipTabP, modoCartaTab);

                            for (int x = 0; x < 5; x++)
                            {
                                if (cartaTabP[x].Tag.ToString() != "-1")
                                {
                                    if (modoCartaTab[x] == 0)
                                    {
  
                                        cartaTabP[x].BackColor = Color.Blue;
                                    }
                                    else
                                    {
                                        cartaTabP[x].BackColor = Color.Red;
                                    }
                                }
                            }

                            btnFinalizarTurno.Visible = false;

                            MessageBox.Show("VEZ DO BOT!", "GAME");
                            BOT_executarJogada(cartaTabB, tooltipTabB, cartaTabP);
                            BOT_cartasNoTab++;
                            if (checarCondicoes() == true) return;
                            else
                            {
                                MessageBox.Show("SUA VEZ!", "GAME");

                                gpbDeck.Visible = true;

                                jogada++;

                                acao = 0;
                            }
                        }
                        else
                        {
                            acao = 1;
                        }

                        atualizarCartasNoTab();
                    }
                }
            }
        }

        private void btnFinalizarTurno_Click(object sender, EventArgs e)
        {
            if (acao == 1)
            {
                btnFinalizarTurno.Visible = false;

                for (int x = 0; x < 5; x++)
                {
                    if (cartaTabP[x].Tag.ToString() != "-1")
                    {
                        if (modoCartaTab[x] == 0)
                        {
                            cartaTabP[x].BackColor = Color.Blue;
                        }
                        else
                        {
                            cartaTabP[x].BackColor = Color.Red;
                        }
                    }
                }

                MessageBox.Show("VEZ DO BOT!", "GAME");
                BOT_executarJogada(cartaTabB, tooltipTabB, cartaTabP);
                BOT_cartasNoTab++;
                if (checarCondicoes() == true) return;
                else
                {
                    MessageBox.Show("SUA VEZ!", "GAME");

                    gpbDeck.Visible = true;

                    jogada++;

                    acao = 0;
                }
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            MessageBoxManager.Yes = "Sim";
            MessageBoxManager.No = "Não";
               
            var escolha = MessageBox.Show("TEM CERTEZA QUE DESEJA ABANDONAR?\n\nAbandono resultará em mais uma derrota!", "GAME", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (escolha == DialogResult.Yes)
            {
                Funcoes.atualizarVDPlayer(YGO_IM.playerid, 0);

                var frmMenu = new YGO_IM(YGO_IM.playerid);
                frmMenu.Show();

                this.Close();
            }
        }
    }
}