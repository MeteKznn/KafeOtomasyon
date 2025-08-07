using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KahveOtomasyonProjesi
{
    public class sqlbaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection(@"Data Source=DESKTOP-2C518IA\SQLEXPRESS;Initial Catalog=KahvehaneOtomasyon;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
