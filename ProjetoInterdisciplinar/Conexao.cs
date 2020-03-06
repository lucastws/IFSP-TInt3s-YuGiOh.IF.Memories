using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ProjetoInterdisciplinar
{
    class Conexao
    {
        public SqlConnection conexao = new SqlConnection();

        public void conectar()
        {
            // INSTÂNCIAS PARA PARA TESTES
            conexao.ConnectionString = @"Persist Security Info=False; Data Source=(LocalDB)\v11.0; AttachDbFilename=|DataDirectory|\bd_yim2.mdf; Integrated Security=True; MultipleActiveResultSets=true;";
            //conexao.ConnectionString = @"Persist Security Info=False; Data Source=(LocalDB)\v11.0; AttachDbFilename=C:\ProjetoInterdisciplinar\ProjetoInterdisciplinar\bd_yim2.mdf; Integrated Security=True; MultipleActiveResultSets=true;";
            //conexao.ConnectionString = @"Persist Security Info=False; Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\ProjetoInterdisciplinar\ProjetoInterdisciplinar\bd_yim.mdf; Integrated Security=True; MultipleActiveResultSets=true;";
            //conexao.ConnectionString = @"Persist Security Info=False; Server=TWS-PC\SQLEXPRESS; Database=bd_yim; Integrated Security=true; MultipleActiveResultSets=true;";

            // BANCO UTILIZADO
            //conexao.ConnectionString = @"Persist Security Info=False; Server=localhost; Database=bd_yim; Integrated Security=true; MultipleActiveResultSets=true;";
            conexao.Open();
        }
        public void desconectar()
        {
            conexao.Close();
        }
    }
}
