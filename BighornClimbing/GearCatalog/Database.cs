using MySql.Data.MySqlClient;

namespace GearCatalog
{
    public class Database
    {
        private MySqlConnection Conn;

        public void Connect()
        {
            string connStr = "server=localhost;user=root;database=climbing_gear;port=3306;password=#iAmRoot";
            Conn = new MySqlConnection(connStr);
            Conn.Open();
        }

        public void InsertGear(Gear gear)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Conn;
            cmd.CommandText =
                "INSERT INTO gear(" +
                "name, category_id, description, brand, weight_grams, length_mm, width_mm, depth_mm, locking" +
                ") VALUES (" +
                "@Name, @Category_id, @Description, @Brand, @Weight_grams, @Length_mm, @Width_mm, @Depth_mm, @Locking)";
            cmd.Parameters.AddWithValue("@Name", gear.Name);
            cmd.Parameters.AddWithValue("@Category_id", gear.CategoryId);
            cmd.Parameters.AddWithValue("@Description", gear.Description);
            cmd.Parameters.AddWithValue("@Brand", gear.Brand);
            cmd.Parameters.AddWithValue("@Weight_grams", gear.WeightGrams);
            cmd.Parameters.AddWithValue("@Length_mm", gear.LengthMM);
            cmd.Parameters.AddWithValue("@Width_mm", gear.WidthMM);
            cmd.Parameters.AddWithValue("@Depth_mm", gear.DepthMM);
            cmd.Parameters.AddWithValue("@Locking", gear.Locking);

            cmd.ExecuteNonQuery();

        }
    }
}