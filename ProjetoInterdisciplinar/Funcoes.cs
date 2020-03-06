using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Windows.Media;
using ProjetoInterdisciplinar;

namespace ProjetoInterdisciplinar
{
    class Funcoes
    {
        public static int randomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public static void tocarSom(string som)
        {
            switch (som)
            {
                case "click":
                    som = "click.wav";
                    break;
                case "carta":
                    som = "carta.wav";
                    break;
                case "menu":
                    som = "menu.wav";
                    break;
                case "game":
                    som = "game.wav";
                    break;
            }

            var caminhoSons = Application.StartupPath + @"\sounds";
            MediaPlayer myPlayer = new MediaPlayer();
            myPlayer.Open(new System.Uri(caminhoSons.ToString() + "/" + som));
            myPlayer.Play();
        }

        public static string retornarNomePlayer(int playerid)
        {
            string retorno = null;

            Conexao conexao = new Conexao();
            conexao.conectar();

            SqlCommand cmd = new SqlCommand("sp_Sel_Player_ESP", conexao.conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pID_Player", playerid).Direction = ParameterDirection.Input;
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    retorno = dr["Nm_Player"].ToString();
                }
            }
            dr.Close();

            conexao.desconectar();

            return retorno;
        }

        public static void atualizarVDPlayer(int player, int ganhou)
        {
            Conexao conexao = new Conexao();
            conexao.conectar();

            SqlCommand cmd = new SqlCommand("sp_Upd_Player", conexao.conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pID_Player", player).Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@pGanhou", ganhou).Direction = ParameterDirection.Input;  
            cmd.ExecuteReader();

            conexao.desconectar();
        }

        public static void atualizarToolTipCartas(int player, PictureBox[] cartaTab, ToolTip[] tooltipCarta, int[] modoCarta)
        {
            Conexao conexao = new Conexao();
            conexao.conectar();

            for (int x = 0; x < 5; x++)
            {
                SqlCommand cmd = new SqlCommand("sp_Sel_Carta_ESP", conexao.conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pID_Player", player).Direction = ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@pID_Carta", cartaTab[x].Tag).Direction = ParameterDirection.Input;
                SqlDataReader dr = cmd.ExecuteReader();

                var caminhoCartas = Application.StartupPath + @"\images\cards";

                if (dr.HasRows)
                {
                    string nome = "";
                    string atk = "";
                    string def = "";

                    string textoTooltip = "";

                    while (dr.Read())
                    {
                        nome = dr["Nm_Carta"].ToString();
                        atk = dr["Vl_Atk"].ToString();
                        def = dr["Vl_Def"].ToString();

                        if (modoCarta[x] == 1)
                        {
                            textoTooltip = nome.Trim() + "\nATK : " + atk + "\nDEF : " + def + "\nMODO : ATAQUE";
                        }
                        else
                        {
                            textoTooltip = nome.Trim() + "\nATK : " + atk + "\nDEF : " + def + "\nMODO : DEFESA";
                        }

                        tooltipCarta[x].SetToolTip(cartaTab[x], textoTooltip);
                    }
                }
                dr.Close();
            }

            conexao.desconectar();
        }

        public static void listarCartas(List<Carta> cartas, ListBox lstCartas)
        {
            Conexao conexao = new Conexao();
            conexao.conectar();

            SqlCommand cmd = new SqlCommand("sp_Sel_Cartas", conexao.conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                string id = "";
                string nome = "";
                string atk = "";
                string def = "";

                while (dr.Read())
                {
                    id = dr["ID_Carta"].ToString();
                    nome = dr["Nm_Carta"].ToString();
                    atk = dr["Vl_Atk"].ToString();
                    def = dr["Vl_Def"].ToString();

                    cartas.Add(new Carta() { ID = id, NOME = nome, ATK = atk, DEF = def });
                }

                lstCartas.DataSource = cartas;
                lstCartas.ValueMember = "ID";
                lstCartas.DisplayMember = "Nome";

            }

            conexao.desconectar();
        }

        public static void popularDeck(int id_player)
        {
            BitArray carta_utilizada = new BitArray(41);

            Conexao conexao = new Conexao();
            conexao.conectar();

            SqlCommand cmd = new SqlCommand("SELECT ID_CartaDeck FROM TB_CartaDeck WHERE ID_Player = @ID", conexao.conexao);
            cmd.Parameters.AddWithValue("@ID", id_player);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                for (int x = 1; x <= 40; x++)
                {
                    int numero = randomNumber(1, 41);

                    while (carta_utilizada[numero] == true)
                    {
                        numero = randomNumber(1, 41);
                    }

                    SqlCommand cmd2 = new SqlCommand("sp_Upd_CartaDeck", conexao.conexao);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@pNr_PosDeck", x).Direction = ParameterDirection.Input;
                    cmd2.Parameters.AddWithValue("@pID_Carta", numero).Direction = ParameterDirection.Input;
                    cmd2.Parameters.AddWithValue("@pID_Player", id_player).Direction = ParameterDirection.Input;
                    SqlDataReader dr2 = cmd2.ExecuteReader(CommandBehavior.SingleRow);

                    carta_utilizada[numero] = true;

                    dr2.Close();
                }
            }
            else
            {
                for (int x = 1; x <= 40; x++)
                {
                    int numero = randomNumber(1, 41);

                    while (carta_utilizada[numero] == true)
                    {
                        numero = randomNumber(1, 41);
                    }

                    SqlCommand cmd3 = new SqlCommand("sp_Ins_CartaDeck", conexao.conexao);
                    cmd3.CommandType = CommandType.StoredProcedure;
                    cmd3.Parameters.AddWithValue("@pNr_PosDeck", x).Direction = ParameterDirection.Input;
                    cmd3.Parameters.AddWithValue("@pID_Carta", numero).Direction = ParameterDirection.Input;
                    cmd3.Parameters.AddWithValue("@pID_Player", id_player).Direction = ParameterDirection.Input;
                    SqlDataReader dr3 = cmd3.ExecuteReader(CommandBehavior.SingleRow);

                    carta_utilizada[numero] = true;

                    dr3.Close();
                }
            }

            dr.Close();

            conexao.desconectar();
        }

        public static void pegar5Cartas(int id_player, PictureBox[] cartaDeck, ToolTip[] tooltipDeck)
        {
            Conexao conexao = new Conexao();
            conexao.conectar();

            SqlCommand cmd = new SqlCommand("sp_Sel_5_Cartas", conexao.conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pID_Player", id_player).Direction = ParameterDirection.Input;
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                string id = "";
                string nome = "";
                string atk = "";
                string def = "";

                var caminhoCartas = Application.StartupPath + @"\images\cards";

                string textoTooltip = "";

                int cartasUsadas = 0;

                while (dr.Read())
                {
                    id = dr["ID_Carta"].ToString();
                    nome = dr["Nm_Carta"].ToString();
                    atk = dr["Vl_Atk"].ToString();
                    def = dr["Vl_Def"].ToString();
                    Funcoes.tocarSom("carta");

                    textoTooltip = nome.Trim() + "\nATK : " + atk + "\nDEF : " + def;

                    cartaDeck[cartasUsadas].Image = Image.FromFile(caminhoCartas.ToString() + "/" + id + ".jpg");
                    cartaDeck[cartasUsadas].Tag = "" + id;
                    tooltipDeck[cartasUsadas].IsBalloon = true;
                    tooltipDeck[cartasUsadas].SetToolTip(cartaDeck[cartasUsadas], textoTooltip);
                    cartaDeck[cartasUsadas].Visible = true;

                    cartasUsadas++;
                }

                dr.Close();

                conexao.desconectar();
            }
        }

        public static int lancarCarta(int id_player, PictureBox cartaDeck, ToolTip tooltipDeck, PictureBox[] cartaTabP, ToolTip[] tooltipTabP)
        {
            int retorno = -1;

            Conexao conexao = new Conexao();
            conexao.conectar();

            SqlCommand cmd = new SqlCommand("sp_Sel_Carta_ESP", conexao.conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pID_Player", id_player).Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@pID_Carta", cartaDeck.Tag).Direction = ParameterDirection.Input;
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                string id = "";
                string nome = "";
                string atk = "";
                string def = "";

                var caminhoCartas = Application.StartupPath + @"\images\cards";

                string textoTooltip = "";

                MessageBoxManager.Yes = "ATAQUE";
                MessageBoxManager.No = "DEFESA";

                var modo = MessageBox.Show("Escolha o modo da carta:", "CARTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                while (dr.Read())
                {
                    id = dr["ID_Carta"].ToString();
                    nome = dr["Nm_Carta"].ToString();
                    atk = dr["Vl_Atk"].ToString();
                    def = dr["Vl_Def"].ToString();
                    Funcoes.tocarSom("carta");

                    bool achou = false;

                    for (int x = 0; x < 5 && achou == false; x++)
                    {

                        bool result = cartaTabP[x].Tag.Equals("-1");

                        if (result == true)
                        {
                            if (modo == DialogResult.No)
                            {
                                retorno = 0;
                                textoTooltip = nome.Trim() + "\nATK : " + atk + "\nDEF : " + def + "\nMODO : DEFESA";
                                cartaTabP[x].BackColor = System.Drawing.Color.Blue;
                            }
                            else
                            {
                                retorno = 1;
                                textoTooltip = nome.Trim() + "\nATK : " + atk + "\nDEF : " + def + "\nMODO : ATAQUE";
                                cartaTabP[x].BackColor = System.Drawing.Color.Red;
                            }

                            cartaTabP[x].Visible = true;
                            cartaTabP[x].Image = Image.FromFile(caminhoCartas.ToString() + "/" + id + ".jpg");
                            cartaTabP[x].Tag = "" + id;
                            tooltipTabP[x].IsBalloon = true;
                            tooltipTabP[x].SetToolTip(cartaTabP[x], textoTooltip);

                            achou = true;
                        }
                    }
                }
                conexao.desconectar();
            }

            return retorno;
        }

        public static void atualizarCarta(int id_player, int posDeck, PictureBox cartaDeck, ToolTip tooltipDeck)
        {
            Conexao conexao = new Conexao();
            conexao.conectar();

            SqlCommand cmd = new SqlCommand("sp_Sel_Prox_Carta_Deck", conexao.conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pID_Player", id_player).Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@pNr_PosDeck", posDeck).Direction = ParameterDirection.Input;
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

                    textoTooltip = nome.Trim() + "\nATK : " + atk + "\nDEF : " + def;

                    cartaDeck.Image = Image.FromFile(caminhoCartas.ToString() + "/" + id + ".jpg");
                    cartaDeck.Tag = "" + id;
                    tooltipDeck.IsBalloon = true;
                    tooltipDeck.SetToolTip(cartaDeck, textoTooltip);
                }
            }

            dr.Close();

            conexao.desconectar();
        }

        public static int atacarCarta(int player1, int player2, PictureBox cartaTab1, PictureBox cartaTab2, int cartaAtacada, int modoCartaAtacada)
        {
            int retorno = 0;

            Conexao conexao = new Conexao();
            conexao.conectar();

            SqlCommand cmd = new SqlCommand("sp_Sel_Carta_ESP", conexao.conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pID_Player", player1).Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@pID_Carta", cartaTab1.Tag).Direction = ParameterDirection.Input;
            SqlDataReader dr = cmd.ExecuteReader();

            var caminhoCartas = Application.StartupPath + @"\images\cards";
            
            if (dr.HasRows)
            {
                string atk = "";
                string def = "";

                while (dr.Read())
                {
                    atk = dr["Vl_Atk"].ToString();
                    def = dr["Vl_Def"].ToString();
                }

                if (cartaTab2 != null && cartaAtacada != -1 && modoCartaAtacada != -1)
                {
                    SqlCommand cmd2 = new SqlCommand("sp_Sel_Carta_ESP", conexao.conexao);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@pID_Player", player2).Direction = ParameterDirection.Input;
                    cmd2.Parameters.AddWithValue("@pID_Carta", cartaTab2.Tag).Direction = ParameterDirection.Input;
                    SqlDataReader dr2 = cmd2.ExecuteReader();

                    if (dr2.HasRows)
                    {
                        string atk2 = "";
                        string def2 = "";

                        while (dr2.Read())
                        {
                            atk2 = dr2["Vl_Atk"].ToString();
                            def2 = dr2["Vl_Def"].ToString();
                        }

                        if (modoCartaAtacada == 1)
                        {
                            if (int.Parse(atk) > int.Parse(atk2))
                            {
                                MessageBox.Show("Ataque perfeito! Carta atacada destruída!", "ATAQUE");

                                cartaTab2.Image = Image.FromFile(caminhoCartas.ToString() + "/verso.png");
                                cartaTab2.Visible = false;
                                cartaTab2.Tag = "-1";
                            }
                            else if (int.Parse(atk) < int.Parse(atk2))
                            {
                                MessageBox.Show("Carta inferior!\nAtaque rebatido e carta destruída.", "ATAQUE");

                                cartaTab1.Image = Image.FromFile(caminhoCartas.ToString() + "/verso.png");
                                cartaTab1.Visible = false;
                                cartaTab1.Tag = "-1";
                            }
                            else
                            {
                                MessageBox.Show("Cartas equivalentes!\nAmbas as cartas foram destruídas.", "ATAQUE");

                                cartaTab1.Image = Image.FromFile(caminhoCartas.ToString() + "/verso.png");
                                cartaTab2.Image = Image.FromFile(caminhoCartas.ToString() + "/verso.png");
                                cartaTab1.Visible = false;
                                cartaTab2.Visible = false;
                                cartaTab1.Tag = "-1";
                                cartaTab2.Tag = "-1";
                            }

                            retorno = int.Parse(atk) - int.Parse(atk2);
                        }
                        else
                        {
                            if (int.Parse(atk) > int.Parse(def2))
                            {
                                cartaTab2.Image = Image.FromFile(caminhoCartas.ToString() + "/verso.png");
                                cartaTab2.Visible = false;
                                cartaTab2.Tag = "-1";

                                MessageBox.Show("Carta destruída!", "ATAQUE");
                            }
                            else if (int.Parse(atk) < int.Parse(def2))
                            {
                                MessageBox.Show("Ataque defendido!", "ATAQUE");
                            }
                            else
                            {
                                MessageBox.Show("Ataque defendido!", "ATAQUE");
                            }
                        }
                    }
                    dr2.Close();
                }
                else
                {
                    retorno = int.Parse(atk);
                }
            }
            dr.Close();

            conexao.desconectar();

            return retorno;
        }
    }
}

